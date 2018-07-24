/*
 * Darktic water bowl
 * sends messages like "0100" via serial to unity
 * Ashley Colley U. Lapland 17.1.2018
 */

// for capacitive sensor
#include <Wire.h> // this sets up pins A4 and A5 as i2c pins for the cap sensor library
#include "Adafruit_MPR121.h"
Adafruit_MPR121 cap = Adafruit_MPR121();

// for the capacitive sensor
float capZero0, capZero1, capZero2, capZero3, capZero4 = 0;
float cap0, cap1, cap2, cap3, cap4 = 0;

unsigned long prevMil0, prevMil1, prevMil2, prevMil3;  // used for time delay

/* START: you can change thhses bits */
unsigned long offDelay = 2000; // after hand is lifted befor eswitches off this is in milliseconds eg 2000 = 2 seconds

float t0=4; // added threshold per area
float t1=4;
float t2=4;
float t3 =4;
/* END: you can change theses bits */

String outString = "0000";
String old_outString = "";

void setup() {
  Wire.begin();
  Serial.begin(115200); // initialize Serial communication 
  delay(2000);
  Serial.println("Started");

  // set up the capacitive senor
  // Serial.println("Adafruit MPR121 Capacitive Touch sensor");
  // Default address is 0x5A, if tied to 3.3V its 0x5B, If tied to SDA its 0x5C and if SCL then 0x5D
  if (!cap.begin(0x5A)) {
      Serial.println("MPR121 not found, check wiring?");
      while (1);
    }
    else {
      Serial.println("MPR121 found!");
    }
zeroCapSensor();
} // end of set up

void zeroCapSensor (){
  Serial.println("Zeroing cap sensors");
  for (int i = 1;i<1000;i++){
    cap0 = cap.filteredData(0); // change to an array later
    cap1 = cap.filteredData(1);
    cap2 = cap.filteredData(2);
    cap3 = cap.filteredData(3);
    
    capZero0 = capZero0* 0.9 + cap0*0.1; // crude smoothing
    capZero1 = capZero1* 0.9 + cap1*0.1;
    capZero2 = capZero2* 0.9 + cap2*0.1;
    capZero3 = capZero3* 0.9 + cap3*0.1;
  }
}

void loop() {

// read the caps
    cap0 = cap0 * 0.9 + 0.1 *(capZero0 -cap.filteredData(0)); // change to an array later
    cap1 = cap1 * 0.9 + 0.1 *(capZero1 -cap.filteredData(1));
    cap2 = cap2 * 0.9 + 0.1 *(capZero2 -cap.filteredData(2));
    cap3 = cap3 * 0.9 + 0.1 *(capZero3 -cap.filteredData(3));

// how to stop showing at start up
    if (millis() > 10000){ // after 10 sec start up

        if ( cap0>t0) { 
          outString.setCharAt(0, 49); // i.e. outstring = "1xxx"
          prevMil0 = millis();
          }
          else {
            if (millis()-prevMil0 > offDelay) outString.setCharAt(0, 48); // i.e. outstring = "0xxx"
            }
         
          
        if ( cap1>t1) { // AC added separate thresholds t0, t1 and t2
          outString.setCharAt(1, 49); // i.e. outstring = "x1xx"
          prevMil1 = millis();
          }
          else {
            if (millis()-prevMil1 > offDelay) outString.setCharAt(1, 48); // i.e. outstring = "x0xx"
            }
          
        if ( cap2>t2) { // AC added separate thresholds t0, t1 and t2
          outString.setCharAt(2, 49);
          prevMil2 = millis();
          }
          else {
            if (millis()-prevMil2 > offDelay) outString.setCharAt(2, 48);
            }

         if ( cap3>t3) { // AC added separate thresholds t0, t1 and t2
          outString.setCharAt(3, 49);
          prevMil3 = millis();
          }
          else {
            if (millis()-prevMil3 > offDelay) outString.setCharAt(3, 48);
            }

         if (outString != old_outString){
          Serial.println(outString);
          old_outString = outString;
         } // if 
          
    } // if millis

} // end of loop




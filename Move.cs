/*
 * Darktic water bowl
 *  Ashley Colley University of Lapland 17.1.2018
 * 
 * Recieves serial messages like "0100" via serial 
 *   ->fades game objects in when 1 is recieved and out when 0 is recieved
 *
 * 1) Make an empty game object and attach this script
 * 2) Create other game objects such as plane, cube etc and set their tag to be "layer1"
 * 3) Repeat 2 for layer2-layer4
 * 
 */

using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class Move : MonoBehaviour {
	// *** NOTE YOU HAVE TO SET THE RIGHT SERIAL PORT EACH TIME ***
	//SerialPort sp = new SerialPort("COM3", 9600); // windows example
	SerialPort sp = new SerialPort("/dev/cu.usbserial-14310", 115200);  // Mac example
	string inputString = "";
	char[] inputChars;
	GameObject layer0, layer1, layer2, layer3;
	Color color0, color1, color2, color3;
	Color curColor; // used in the fading
	float alphaNow, alphaDiff; // used in fading

	float fixedStep = 0.05f;
	float closeEnough = 0.1f;

	void Start () {
		sp.Open ();
		sp.ReadTimeout = 1;
		print ("Started");
		layer0 = GameObject.FindWithTag("layer0");
		layer1 = GameObject.FindWithTag("layer1");
		layer2 = GameObject.FindWithTag("layer2");
		layer3 = GameObject.FindWithTag("layer3");
	}

	void Update () 
	{
		try{
			inputString= sp.ReadLine();
			// for each sensor
				print(inputString);
			}
				catch(System.Exception){
			}
		if (inputString.Length == 4 ) {

			char input0 = inputString [0];
			float targetOpacity0 = float.Parse ("" + input0);
			curColor = layer0.GetComponent<Renderer> ().material.color;
			//print (curColor);
			if (curColor.a > targetOpacity0 - closeEnough) {
				curColor.a = curColor.a - fixedStep;
				layer0.GetComponent<Renderer> ().material.SetColor ("_Color", curColor);
			}
			if (curColor.a < targetOpacity0 + closeEnough) {
				curColor.a = curColor.a + fixedStep;
				layer0.GetComponent<Renderer> ().material.SetColor ("_Color", curColor);
			}
				
			char input1 = inputString [1];
			//print (input1);
			float targetOpacity1 = float.Parse ("" + input1);
			curColor = layer1.GetComponent<Renderer> ().material.color;
			if (curColor.a > targetOpacity1 - closeEnough) {
				curColor.a = curColor.a - fixedStep;
				layer1.GetComponent<Renderer> ().material.SetColor ("_Color", curColor);
			}
			if (curColor.a < targetOpacity1 + closeEnough) {
				curColor.a = curColor.a + fixedStep;
				layer1.GetComponent<Renderer> ().material.SetColor ("_Color", curColor);
			}
				
			char input2 = inputString [2];
			//print (input2);
			float targetOpacity2 = float.Parse ("" + input2);
			curColor = layer2.GetComponent<Renderer> ().material.color;
			if (curColor.a > targetOpacity2 - closeEnough) {
				curColor.a = curColor.a - fixedStep;
				layer2.GetComponent<Renderer> ().material.SetColor ("_Color", curColor);
			}
			if (curColor.a < targetOpacity2 + closeEnough) {
				curColor.a = curColor.a + fixedStep;
				layer2.GetComponent<Renderer> ().material.SetColor ("_Color", curColor);
			}

			char input3 = inputString [3];
			//print (input3);
			float targetOpacity3 = float.Parse ("" + input3);
			curColor = layer3.GetComponent<Renderer> ().material.color;
			if (curColor.a > targetOpacity3 - closeEnough) {
				curColor.a = curColor.a - fixedStep;
				layer3.GetComponent<Renderer> ().material.SetColor ("_Color", curColor);
			}
			if (curColor.a < targetOpacity3 + closeEnough) {
				curColor.a = curColor.a + fixedStep;
				layer3.GetComponent<Renderer> ().material.SetColor ("_Color", curColor);
			}
				
		}// end if

	}

}

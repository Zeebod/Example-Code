/*
***By Default your text will be Arial, this is the default text for
Unity. It is advised that you create two varialbes a boolean and a new
object of the Rect class. This can be named anything that you would like

*/
using UnityEngine;
using System;
using System.Collections;

public class MyTestFile : MonoBehaviour {

#region	Rectangles
	Rect myRect = new Rect (0, 0, Screen.width/2, Screen.height/2);
	Rect b_Rect = new Rect (Screen.width/2, Screen.height/2 , 100, 50);
	Rect c_Rect = new Rect (Screen.width / 2, Screen.height / 2 + 100, 100, 50);
	Rect n_Rect = new Rect (Screen.width / 2, Screen.height / 2 + 200, 100, 50);
	Rect bkgd_Rect = new Rect(0, 0, Screen.height*2, Screen.width*2);
#endregion

#region Globals
	bool showText = true;
	string gameTitle = "GSF_2K";
	//private Texture2D backTexture; //used for loading a 2d background
	GUIStyle myGuiStyle;
	public Font myF;
#endregion


	//Example of loading a texture file
	void Start(){
		//Working on
		//backTexture = (Texture2D)Resources.Load ("Textures/spaceBattle", typeof(Texture2D)); 
		myF = (Font)Resources.Load ("Fonts/STENCIL", typeof(Font));
	}

	void OnGUI(){
		//Creating a style for a label
		myGuiStyle = new GUIStyle (GUI.skin.label); //used for our text label
		myGuiStyle.fontSize = 120;

		//load and setting our font
		myGuiStyle.font = myF;

		//changing the color of our font
		myGuiStyle.normal.textColor = Color.cyan;


		if (showText) {
			GUI.Label(myRect, gameTitle, myGuiStyle);
			GUI.Button(b_Rect, "New Game"); //this is a generic button
			GUI.Button(c_Rect, "Continue");

			//GUI.DrawTexture(bkgd_Rect, backTexture); working on this


			//This is an example of making buttons conditional statements.
			//When using a button like this it means that the UnityEngine
			//based upon the GUI class and the Button object are using what 
			//is known as an EventHandler. This EventHandler has a Listener 
			//attached that is awaiting input from the user. This input will
			//most likely be a mouse click and it knows that it should display
			//to the debug log that this button is functioning.
			if(GUI.Button(n_Rect, "Exit")){
				Debug.Log("I am the exit button, and I work");
			}

		}
	}
	
}

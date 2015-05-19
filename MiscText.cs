using UnityEngine;
using System.Collections;

public class MiscText : MonoBehaviour {

	#region Rectangles
	//New object of Rectangle class
	//This label begins at the top left corner of the screen
	Rect myNewRect = new Rect(0, 0, Screen.width, Screen.height);
	Rect myNewRect1 = new Rect(100, 100, Screen.width, Screen.height);
	#endregion

	#region String variables
	string s1 = "New Game Screen";
	#endregion

	#region My Font object
	public Font myFont;
	#endregion

	#region GUIStyle object
	GUIStyle myGS;
	#endregion
	// Use this for initialization
	void Start () {
		myFont = (Font)Resources.Load ("Fonts/BAUHS93", typeof(Font));
	}
	
	void OnGUI(){
		myGS = new GUIStyle (GUI.skin.label);
		myGS.fontSize = 48;
		myGS.normal.textColor = Color.green;
		myGS.font = myFont;
		//GUI.Label (myNewRect, "New Game Screen");
		GUI.Label (myNewRect, s1, myGS);
	}
}

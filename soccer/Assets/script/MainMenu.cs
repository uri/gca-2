
using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	
	private int buttonWidth = 140;
	private int buttonHeight = 60;
	private string buttonString = "Start";
	
	public Texture background;

	public int Continue;
	public int NextLevel;
	
	
	void OnGUI() {
		
		if (Continue == 0) {
			buttonString = "Start";
		} else {
			buttonString = "Continue";	
		}
		
		GUI.DrawTexture(new Rect(0,0,Screen.width, Screen.height), background);
		
		
		bool pressed = false;
		
		if (Continue == 0)
			pressed = GUI.Button(new Rect(Screen.width / 2 - buttonWidth /2, Screen.height / 2, buttonWidth, buttonHeight), buttonString);
		else
			pressed = GUI.Button(new Rect(Screen.width / 2 - buttonWidth /2, Screen.height / 2, buttonWidth, buttonHeight), buttonString);	
			
		if (pressed) {
			Application.LoadLevel(NextLevel);
		}
	}
}

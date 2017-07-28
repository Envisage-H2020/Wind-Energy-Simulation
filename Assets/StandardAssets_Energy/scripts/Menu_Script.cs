using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using goedle_sdk;

public class Menu_Script : MonoBehaviour {
	
	private string playerName;
	private string playerSurname;
	private string playerSchoolName;


	public void onClick_LoadScene(string sceneName){

		// Marc use string sceneName to see when the EducationalScene is called. Only from SceneSelector can an EducationalScene be called.
		// sceneName is a wordpress slug containing numbers, letters lower case, or hyphens, e.g. athens
		SceneManager.LoadScene(sceneName);
	}

	public void onClick_LoadCredsScene(){
		// Marc: event when credits button is pressed
		SceneManager.LoadScene("S_Credits");
	}

	public void onClick_LoadSceneSelectorScene(){
		// Marc: Event when "Play" button is pressed that leads to SceneSelector scene
		SceneManager.LoadScene("S_SceneSelector");
	}

	public void onClick_LoadMainMenuScene(){
		// Marc: Event when "Back" button is pressed from SceneSelector, Reward scene, Help, Settings, Credits, Settings, Login
		SceneManager.LoadScene("S_MainMenu");
	}

	public void onClick_LoadSettingsScene(){
		// Marc: Event when "Settings" button is pressed from main menu
		SceneManager.LoadScene("S_Settings");
	}
	
	public void onClick_LoadLoginScene(){
		// Marc: Event when "Login" button is pressed from main menu
		SceneManager.LoadScene("S_Login");
	}


	public void onClick_LoadHelpScene(){
		// Marc: Event when "Help" button is pressed from main menu
		SceneManager.LoadScene("S_Help");
	}
	

	public void onClick_ExitGame(){
		// Marc: Exit button event (only for desktop versions). It does not exist for web version. It is only when the user closes the tab of the webbrowser that the game exits.
		Application.Quit ();
	}


	/*//////////////////////
	 Inputfield functions 
	//////////////////////*/

	public void NameEntered(string text){
		playerName = text;
		GoedleAnalytics.identify ("first_name", playerName);
		//TODO: "http" call can be entered here to retrieve the value.
	}
	public void SurnameEntered(string text){
		playerSurname = text;
		GoedleAnalytics.identify ("last_name", playerSurname);
		//TODO: "http" call can be entered here to retrieve the value.
	}
	public void SchoolNameEntered(string text){
		playerSchoolName = text;
		GoedleAnalytics.track ("group", "school", playerSchoolName);
		//TODO: "http" call can be entered here to retrieve the value.
	}

	public void InputFieldsFilled(){
		if(playerName != null && playerSurname != null && playerSchoolName != null){

		}
		else {

		}
	}




}
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using goedle_sdk;
using System;
using System.Text;


// A script that is only used in the inputScene to retrieve the personal information of the user.
public class PlayerPersonalInfo : MonoBehaviour {

	private string playerName;
	private string playerSurname;
	private string playerSchoolName;
	public LevelManager levelMng;
	public Text msgText; // text that informs the user to enter personal info, if there are left empty.


	void Start(){
		msgText.enabled = false;
	}


	/*//////////////////////
	 Inputfield functions 
	//////////////////////*/

	public void NameEntered(string text){
		playerName = text;

		//TODO: "http" call can be entered here to retrieve the value.
	}
	public void SurnameEntered(string text){
		playerSurname = text;
		//TODO: "http" call can be entered here to retrieve the value.
	}
	public void SchoolNameEntered(string text){
		playerSchoolName = text;
		//TODO: "http" call can be entered here to retrieve the value.
	}

	public void InputFieldsFilled(){
		if(playerName != null && playerSurname != null && playerSchoolName != null){
			string user_id = (playerName + playerSurname + playerSchoolName).ToLower().Trim();
			using (MD5 md5 = MD5.Create())
			{
				byte[] hash = md5.ComputeHash(Encoding.Default.GetBytes(user_id));
				Guid user_id_hash = new Guid(hash);
				GoedleAnalytics.setUserId (user_id_hash.ToString("D"));
				GoedleAnalytics.trackTraits ("first_name", playerName);
				GoedleAnalytics.trackTraits ("last_name", playerSurname);
				GoedleAnalytics.track ("group", "school", playerSchoolName);
			}
			levelMng.LoadNextLevel();
		}
		else {
			msgText.enabled = true;
		}
	}
}

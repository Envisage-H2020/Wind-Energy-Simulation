﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using goedle_sdk;

public class TurbineInputManager : MonoBehaviour {
    
	public Canvas PopUpCanvas;
	private Text popUpText;
	private Image backgroundImage;
	private TurbineController turbineController;
	private bool displayPopUpText = false;
	private Vector2 mousePos;
	private Vector2 canvasPos;


	[Header ("Materials")]
	[SerializeField] private Material dafaultMat;
	[SerializeField] private Material highlightedMat;
	[SerializeField] private Material transparentMat;

    void Start(){
		PopUpCanvas.enabled = false;
		PopUpCanvas.gameObject.SetActive (true);
		turbineController = GetComponent<TurbineController>();
		InitializePopUpText();
	}

	void Update(){
		//used for highlighting the turbine when is damaged.
		if(turbineController.isDamaged)
			HighlightTurbine(turbineController.isDamaged);
	}

	//when user clicks the turbine.
	void OnMouseDown () {
		//clicks while turbine is rotating.
		if (turbineController.isConstructed) {
			if (turbineController.isRotating && !turbineController.isDamaged) {
				turbineController.DisableTurbine ();
				GoedleAnalytics.track ("disable.turbine");
			} else if (!turbineController.isRotating && !turbineController.isDamaged) {
				turbineController.EnableTurbine ("onTurbine");
				GoedleAnalytics.track ("enable.turbine");
			} else if (turbineController.isDamaged) {
				turbineController.repairTurbine ();
				GoedleAnalytics.track ("repair.turbine");
			}
		}
	}

	
	//when mouse is hovered over a wind turbine the turbine is highlighted.
	void HighlightTurbine(bool isDamaged){
		gameObject.transform.Find("Turbine_Fan").GetComponent<Renderer>().material = displayPopUpText ? highlightedMat : (isDamaged ? transparentMat : dafaultMat);
		gameObject.transform.Find("Turbine_Main").GetComponent<Renderer>().material = displayPopUpText ? highlightedMat : (isDamaged ? transparentMat : dafaultMat);
	}

	//when mouse is over a wind turbine without being clicked.
	void OnMouseEnter(){
		PlaceCanvasToMouse();
		displayPopUpText = true;
		DisplayPopUpText();
		HighlightTurbine(turbineController.isDamaged);
	}

	//when mouse is not over a wind turbine (specifically on the collider of the turbine).
	void OnMouseExit(){
		displayPopUpText = false;
		DisplayPopUpText();
		HighlightTurbine(turbineController.isDamaged);
	}


	void InitializePopUpText(){
		popUpText = PopUpCanvas.GetComponentInChildren<Image>().GetComponentInChildren<Text>();
		backgroundImage = PopUpCanvas.GetComponentInChildren<Image>();
		popUpText.enabled = false;
		backgroundImage.enabled = false;
	}

	void DisplayPopUpText(){
		if(displayPopUpText == true){
			if(turbineController.isRotating){
				popUpText.color = Color.red;
				popUpText.text = "Turn off";
			}
			else if(!turbineController.isRotating && !turbineController.isDamaged){
				popUpText.color = Color.blue;
				popUpText.text = "Turn on";
			}
			else {
				popUpText.text = "repair";
				popUpText.color = Color.black;
			}
			popUpText.enabled = true;
			backgroundImage.enabled = true;
		}
		else{
			popUpText.enabled = false;
			backgroundImage.enabled = false;
		} 
	}

	// used to move the text next to the selected turbine.
	void PlaceCanvasToMouse(){
		mousePos = Input.mousePosition;
		canvasPos.x = mousePos.x;
		canvasPos.y = mousePos.y+20;
		PopUpCanvas.GetComponentInChildren<Image>().transform.position = canvasPos;
	}
}
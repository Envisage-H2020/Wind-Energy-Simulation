﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using goedle_sdk;

public class TurbineQuadSelector : MonoBehaviour {

	public Vector3 rotation = Vector3.zero;
	private TurbineInputManager turbineInputManager;
	private TurbineController turbineController;
	private Simulation simulation;


	private GameObject infoQuad;
	private GameObject infoQuadText;
	private Camera camera;

	void Awake(){
		InvokeRepeating("updateInfoText", 0,5);
	}

	void Start () {
		turbineInputManager = transform.GetComponentInParent<TurbineInputManager>();
		turbineController = transform.GetComponentInParent<TurbineController>();

		if ( GameObject.FindGameObjectsWithTag("terrain").Length > 0) // in the case there is no terrain be safe
			simulation  = GameObject.FindGameObjectsWithTag("terrain")[0].GetComponent<Simulation>();

		infoQuad = transform.parent.Find("InfoQuad").gameObject;
		infoQuadText = infoQuad.transform.Find("InfoQuadText").gameObject;

		camera = GameObject.Find ("camera").GetComponent<Camera> ();

		updateInfoText ();
	}

	void Update () {
		infoQuad.transform.LookAt (camera.transform.position);
	}


	void updateInfoText(){
		infoQuadText.GetComponent<TextMesh>().text =   
				"	<b>Class " + turbineController.turbineClass + "</b>\n" +
				"<color=blue>\n<b>Output:</b>" + turbineController.turbineEnergyOutput + " (" +
				turbineController.turbineCurrEnergyOutput + ")" +
				" MW\n<b>Cost   : </b>$ " + turbineController.turbineCost +
				"\n<b>Rotor  : </b>" + turbineController.turbineRotorSize + " m\n" + 
				"<b>Wind   : </b>" +  turbineController.turbineWindClass  + "m/s\n</color>";
	}

	void OnMouseOver(){
		
		transform.Rotate (rotation * Time.deltaTime);	

		infoQuad.GetComponent<MeshRenderer>().enabled = true;
		infoQuadText.GetComponent<MeshRenderer>().enabled = true;

		if(Input.GetMouseButtonDown(0)){
			GoedleAnalytics.track ("add.turbine");

			simulation.totalIncome -= turbineController.turbineCost;

			turbineInputManager.PopUpCanvas.enabled = true;
			turbineController.EnableTurbine ("QuadonClick");

			infoQuad.GetComponent<MeshRenderer> ().enabled = false;
			infoQuadText.GetComponent<MeshRenderer>().enabled = false;

			// Inactivate the Quad
			//gameObject.SetActive (false);
			gameObject.GetComponent<MeshCollider> ().enabled = false;
			gameObject.GetComponent<MeshRenderer> ().enabled = false;

			// Enable the Visuals of the turbine
			foreach (Transform childtr in transform.parent.gameObject.transform) { 

				if (childtr.gameObject.tag == "producer_mesh") 
					foreach (Transform tr2 in childtr)
						if (tr2.gameObject.name == "Turbine_Fan" || tr2.gameObject.name == "Turbine_Main") // new prefab
								tr2.gameObject.GetComponent<Renderer> ().enabled = true;

			}

			// Destroy Nearby turbine spots
			float range = 3 * turbineController.turbineRotorSize;
			GameObject [] producersArray = GameObject.FindGameObjectsWithTag ("producer");

      		foreach (GameObject go in producersArray){
				float distance = Mathf.Sqrt( (transform.position - go.transform.position).sqrMagnitude );
				if(distance < range && go != transform.parent.gameObject)
						go.SetActive(false);
			}


		}
	}


	void OnMouseExit(){
		infoQuad.GetComponent<MeshRenderer> ().enabled = false;
		infoQuadText.GetComponent<MeshRenderer>().enabled = false;
	}
}

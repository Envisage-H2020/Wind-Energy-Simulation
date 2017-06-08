using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using goedle_sdk;

public class TurbineQuadSelector : MonoBehaviour {

	public Vector3 rotation = Vector3.zero;
	private TurbineInputManager turbineInputManager;
	private TurbineController turbineController;

	void Start () {
		turbineInputManager = transform.GetComponentInParent<TurbineInputManager>();
		turbineController = transform.GetComponentInParent<TurbineController>();
	}

	void Update () {}

	void OnMouseOver(){
		
		transform.Rotate (rotation * Time.deltaTime);	

		if(Input.GetMouseButtonDown(0)){
			GoedleAnalytics.track ("add.turbine");

			turbineInputManager.PopUpCanvas.enabled = true;
			turbineController.EnableTurbine ("QuadonClick");

			// Inactivate the Quad
			gameObject.SetActive (false);

			// Enable the Visuals of the turbine
			foreach (Transform child in transform.parent.gameObject.transform) 
				if (child.gameObject.name == "Turbine_Fan" || child.gameObject.name == "Turbine_Main")
						child.gameObject.GetComponent<Renderer> ().enabled = true;
		}
	}
}

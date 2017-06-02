using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using goedle_sdk;

public class TurbineQuadSelector : MonoBehaviour {

	public Vector3 rotation = Vector3.zero;
	private SpawnManager turbineSpawner;

	// Use this for initialization
	void Start () {
		turbineSpawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<SpawnManager>();
	}
	
	// Update is called once per frame
	void Update () {
	}


	void OnMouseOver(){
		
		transform.Rotate (rotation * Time.deltaTime);	

		if(Input.GetMouseButtonDown(0)){


			GoedleAnalytics.track ("add.turbine");

			turbineSpawner.numberOfTurbines ++;
			turbineSpawner.numberOfTurbinesOperating++;

			// Inactivate the Quad
			gameObject.SetActive (false);

			// Enable the Visuals of the turbine
			foreach (Transform child in transform.parent.gameObject.transform) 
				if (child.gameObject.name == "Turbine")
					foreach (Transform child2 in child.gameObject.transform) 
						if (child2.gameObject.name == "Turbine_Fan" || child2.gameObject.name == "Turbine_Main")
							child2.gameObject.GetComponent<Renderer> ().enabled = true;





			

		}
	}
}

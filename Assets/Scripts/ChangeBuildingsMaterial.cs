using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBuildingsMaterial : MonoBehaviour {

    private Simulation simulation;
	private string prev_usage = " ";

	void Start() {
		simulation = GameObject.Find("simulator").GetComponent<Simulation>();
	}

	void Update(){
     	//made to call the method only when a change has occured and not every frame (optimization purposes)
		if(!string.Equals(simulation.powerUsage,prev_usage) ){

			if (string.Equals (simulation.powerUsage, "Under power"))
				gameObject.GetComponent<Renderer> ().material.color = Color.red;
			else if(string.Equals(simulation.powerUsage,"Correct power"))
				gameObject.GetComponent<Renderer> ().material.color = Color.white;
			else 
				gameObject.GetComponent<Renderer> ().material.color = Color.blue;

			prev_usage = simulation.powerUsage;
		}
	}


}

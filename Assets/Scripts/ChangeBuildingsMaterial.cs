using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBuildingsMaterial : MonoBehaviour {

    private Simulation simulator;
	private string prev_usage = " ";

	void Start() {
		simulator = GameObject.FindGameObjectWithTag("Simulator").GetComponent<Simulation>();
	}

	void Update(){
     	//made to call the method only when a change has occured and not every frame (optimization purposes)
		if(!string.Equals(simulator.powerUsage,prev_usage) ){

			if (string.Equals (simulator.powerUsage, "Under power"))
				gameObject.GetComponent<Renderer> ().material.color = Color.red;
			else if(string.Equals(simulator.powerUsage,"Correct power"))
				gameObject.GetComponent<Renderer> ().material.color = Color.white;
			else 
				gameObject.GetComponent<Renderer> ().material.color = Color.blue;

			prev_usage = simulator.powerUsage;
		}
	}


}

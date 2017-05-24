﻿using UnityEngine;
using UnityEngine.SceneManagement;
using goedle_sdk;

public class PlayerStatistics : MonoBehaviour {

	private Simulation simulator;
	public static int underPowerSec;
    public static int correctPowerSec;
    public static int overPowerSec;
    public bool endSimulation = false;
  
    // Use this for initialization
    void Start () {
		endSimulation = false;
		if(!SceneManager.GetActiveScene().name.Equals("S_Reward")){ // if not the last scene
			simulator = GameObject.FindGameObjectWithTag("Simulator").GetComponent<Simulation>();
			InitializeCountValues();
			InvokeRepeating("CalculatePowerUsageStatistics", 0.0f, 1.0f);
		}
	}

	void Update(){
		checkEndSimulation();
	}

	void InitializeCountValues(){
		underPowerSec = 0;
		correctPowerSec = 0;
		overPowerSec = 0;
	}


	/*
	Stops the simulation and loads the end scene in the game. This can be achieved either by clicking on the exit button,
	or after 24 minutes have passed.
	*/
	public void checkEndSimulation(){
		if(!SceneManager.GetActiveScene().name.Equals("S_Reward")){  // if not the last scene
			if(simulator.minutesCount >= 24 || endSimulation == true){
				GoedleAnalytics.track ("end.simulation");
				SceneManager.LoadScene("S_Reward" ); // loads last scene
				Resources.UnloadUnusedAssets(); //removes unused assets to free memory
			}
		}
	}

	public void ExitButtonPressed(){
		endSimulation = true;
	}
	

	/*
	It holds to static variables the seconds that the player has spent in each power output scenario respectively.
	These values are later used in the end scene to calculate and display the usage of the wind farm, concerning the time spent in each scenario. 
	*/
 	void CalculatePowerUsageStatistics(){
		if( !SceneManager.GetActiveScene().name.Equals("S_Reward") ){
			if(string.Equals(simulator.powerUsage,"Under power")){
				underPowerSec++;
			}
			else if(string.Equals(simulator.powerUsage,"Correct power")){
				correctPowerSec++;
			}
			else {
				overPowerSec++;
			}
		}
	}

	void GetPlayerInfo(){
	}
}

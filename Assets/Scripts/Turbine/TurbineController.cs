using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurbineController : MonoBehaviour {
	
	//dependacies of other scripts
	private TurbineAnimCtrl turbineAnim;
	private TurbineDamage turbineDmg;
	private TurbineInputManager inputManager;
	private TurbineRepair repair;
	private Simulation simulation;

	private bool lowWindDisabled = false; //shows if turbine should stop rotating when wind under 4 m/s.
	private bool scriptsEnabled = true;

    // Use this for initialization
    void Start () {
		inputManager = GetComponent<TurbineInputManager>();
		turbineAnim = GetComponentInChildren<TurbineAnimCtrl>();
		turbineDmg = GetComponent<TurbineDamage>();
		repair = GetComponent<TurbineRepair>();
		simulation  = GameObject.Find("simulator").GetComponent<Simulation>();
	}

	void Update(){
		if (simulation.gamePaused == true) {
			pauseTurbineStateIntention (false);		
		}else{
			if(scriptsEnabled == false) 
				pauseTurbineStateIntention (true);		
			
			//sets the speed of the rotation based on the wind rotation
			turbineAnim.SetRotationSpeed(simulation.currentWindSpeed);


			//used for disables rotation when wind is low
			if(simulation.currentWindSpeed < 3 && IsRotating() == true){
				DisableOnWindLow();
			}
			if(simulation.currentWindSpeed > 3 && IsRotating() == false && lowWindDisabled == true){
				EnableOnWindHigh();
			}
		}
	}


	/* 
	make turbines stop rotating when wind is below 4 m/s.
	*/
	public void DisableOnWindLow(){
			DisableTurbine();
			lowWindDisabled = true;
	}


	/* 
	make turbines rotate again when wind is not below
	the low speed.
	*/
	public void EnableOnWindHigh(){
			EnableTurbine();
			lowWindDisabled = false;
	}


	public void setDamage( bool isDamaged){
		turbineDmg.isDamaged = isDamaged;
	}

	public void repairTurbine(){
		//decreases total income
		if(simulation.income >= 1){
			simulation.income--;
			repair.turbineRepair();
			simulation.damagedTurbines--;
		}
	}


	public void setRepair(bool repairBool){
		repair.isRepaired = repairBool;
	}

	public void DisableTurbine(){
		//used to display the numbers for the output values next to the minimap
		StartCoroutine(simulation.calculateSubstractedPower());

		turbineAnim.DisableRotation();
		simulation.numberOfTurbinesOperating--;	
	}

	public void EnableTurbine(){
		//used to display the numbers for the output values next to the minimap
		StartCoroutine(simulation.calculateAddedPower());

		turbineAnim.EnableRotation();
		simulation.numberOfTurbinesOperating++;
	}


	//disables all scripts if game is paused
	public void pauseTurbineStateIntention(bool intentTurbine){
		
			//disable animation
		turbineAnim.enabled = intentTurbine;
		turbineAnim.GetComponent<Animator>().enabled = intentTurbine;

		turbineDmg.enabled = intentTurbine;
		repair.enabled = intentTurbine;
		inputManager.enabled = intentTurbine;

			//used in the update function to minimize the times it calls the function
		scriptsEnabled = intentTurbine;
	}


	/*--------- Auxiliary Gets ------------*/
	public bool IsRotating(){
		return turbineAnim.isRotating; 	
	}

	public bool IsDamaged(){
		return turbineDmg.isDamaged;
	}

	public bool isRepaired(){
		return repair.isRepaired;
	}


	public int getNumberOfTurbinesOperating(){
		return simulation.numberOfTurbinesOperating;	
	}

	public int getNumberOfTurbines(){
		return simulation.numberOfTurbines;	
	}

}

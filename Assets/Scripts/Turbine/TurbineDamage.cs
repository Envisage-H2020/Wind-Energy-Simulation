using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 In this class we use some basic probality maths to randomly damage a wind turbine.
 The main class is CalculateDamagePropability(), that produces a float " damagePropability " with 
 different values in each iteration.
 */
public class TurbineDamage : MonoBehaviour {
	
	private Simulation simulation;
	public bool isDamaged = false;
	private TurbineController turbineController;
	private float propabilityMultiplier;
	private float turbineUsage;
    private int damageStartTime;
	private float rate; // te rate that the method to damage the turbine will be called

     void Start(){			
         damageStartTime = 0; // 4
         float startCall = Random.Range(0.0f,90.0f);
         float rate = Random.Range(120.0f,300.0f);
         simulation = GameObject.Find("simulator").GetComponent<Simulation>();
         turbineController = GetComponent<TurbineController>();
		 //Calls the method for the first time in "startCall" with a repeat rate of the "rate" value.
         InvokeRepeating("CalculateDamagePropability",startCall,rate);		
     }
	
	/* 
	=====================================
		Calculate the propability that a
		turbine can get damaged
 	=====================================
	*/
	void CalculateDamagePropability(){

		if(simulation.minutesCount >= damageStartTime  &&  turbineController.IsRotating() == true && turbineController.IsDamaged() == false
			&& simulation.damagedTurbines <= 4){

			propabilityMultiplier = Random.Range(0.0f,1.0f);
			turbineUsage = 0.0f;
			if( string.Compare(simulation.powerUsage,"Over power") == 0 ){
				turbineUsage = 1.3f;
			}
			else if(string.Compare(simulation.powerUsage,"Correct power") == 0 ){
				turbineUsage = 1.0f;
			}
			else {
				turbineUsage = 0.0f;
			}

			float damagePropability = turbineUsage * propabilityMultiplier;

			damagePropability = 1;

			if(damagePropability > 0.85){
				damageTurbine();
			} 
		}
	}

	/*
	damages the turbine and stops it's operation
	*/
	void damageTurbine(){
			turbineController.DisableTurbine();
			isDamaged = true;
			turbineController.setRepair(false);
		    simulation.damagedTurbines++;
	}
}

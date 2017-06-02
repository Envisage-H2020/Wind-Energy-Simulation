using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurbineAnimCtrl : MonoBehaviour {
	Animator animator;
	public bool isRotating = true;

    void Start () {
		animator = GetComponent<Animator>();
		animator.SetBool("Rotate",true);
	}

	public void DisableRotation(){  
		animator.SetBool("Rotate",false);
		isRotating = false;
	}

	public void EnableRotation(){
		animator.SetBool("Rotate",true);
		isRotating = true;
	}

	public void SetRotationSpeed(int windspeed){
		animator.SetFloat("speedMultiplier", (float) (windspeed) * (float) GameObject.Find("simulator").GetComponent<Simulation>().simulationSpeed / 20 );
	}
}

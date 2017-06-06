using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesController : MonoBehaviour {
	
	private TurbineController turbineController;
	//private Transform turbinePosition;
	public bool isEmiting = false;
	//private GameObject obj;
	
    // Use this for initialization
    void Start () {
	//	turbinePosition = GetComponent<Transform>();
		turbineController = GetComponent<TurbineController>();
	}

	void Update(){

		if(turbineController.IsDamaged() == true && isEmiting == false){
			EmitParticle();
		}
		else if(turbineController.isRepaired() == true && isEmiting == true){
			StopParticle();
		}
	}
	
	public void EmitParticle(){


		foreach (Transform child in gameObject.transform)
			if (child.gameObject.name == "Turbine_Smoke") 
				child.gameObject.SetActive (true);



//		obj = ObjectPooler.current.GetPooledObject();
//		if(obj == null)  return;
//
//		//obj.transform.position = new Vector3 (turbinePosition.position.x, turbinePosition.position.y + 30,turbinePosition.position.z);
//		obj.SetActive(true);

		isEmiting = true;
	}


	public void StopParticle(){

		foreach (Transform child in gameObject.transform)
			if (child.gameObject.name == "Turbine_Smoke") 
				child.gameObject.SetActive (false);

//		obj.SetActive(false);
		isEmiting = false;
	}
}

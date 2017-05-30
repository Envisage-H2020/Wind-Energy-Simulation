using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurbineSelector : MonoBehaviour {

	public Vector3 rotation = Vector3.zero;




	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}




	void OnMouseOver(){
		
			transform.Rotate (rotation * Time.deltaTime);	

		if(Input.GetMouseButtonDown(0)){
			Debug.Log ("PRESS QUAD");
			GameObject.Find (transform.parent.gameObject.name + "_Fan").GetComponent<Renderer> ().enabled = true;
			GameObject.Find (transform.parent.gameObject.name + "_Main").GetComponent<Renderer> ().enabled = true;
			GameObject.Find (transform.parent.gameObject.name + "_quad").SetActive(false);
		}
	}

}

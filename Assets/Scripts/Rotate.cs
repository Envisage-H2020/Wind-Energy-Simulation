using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {
	public Vector3 Rotation = Vector3.zero;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (Rotation * Time.deltaTime);
	}
}

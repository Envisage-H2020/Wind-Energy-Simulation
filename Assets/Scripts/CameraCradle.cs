using UnityEngine;
using System.Collections;

public class CameraCradle : MonoBehaviour {

	public float Speed = 20;
	public float Height = 80;

	// Use this for initialization
	void Start () {
		foreach (var p in RtsManager.Current.Players) {
			if (p.IsAi)
				continue;

			var pos = p.Location.position;
			pos.y = Height;
			transform.position = pos;
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (
			Input.GetAxis ("Horizontal") * Speed * Time.deltaTime,
			Input.GetAxis ("Vertical") * Speed * Time.deltaTime,
			0);
	}
}

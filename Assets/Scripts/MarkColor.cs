using UnityEngine;
using System.Collections;

public class MarkColor : MonoBehaviour {

	public MeshRenderer[] Renderers;

	// Use this for initialization
	void Start () {
		var color = GetComponent<Player> ().Info.AccentColor;
		foreach (var r in Renderers) {
			r.material.color = color;
		}
	}
}

using UnityEngine;
using System.Collections.Generic;

public class AiSupport : MonoBehaviour {

	public List<GameObject> Drones = new List<GameObject>();
	public List<GameObject> CommandBases = new List<GameObject>();

	public PlayerSetupDefinition Player = null;

	public static AiSupport GetSupport (GameObject go)
	{
		return go.GetComponent<AiSupport>();
	}

	public void Refresh()
	{
		Drones.Clear ();
		CommandBases.Clear ();
		foreach (var u in Player.ActiveUnits) {
			if (u.name.Contains("Drone Unit")) Drones.Add (u);
			if (u.name.Contains("Command Base")) CommandBases.Add(u);
		}
	}
}

using UnityEngine;
using System.Collections;

public class CreateDronesAi : AiBehavior {

	public int DronesPerBase = 5;
	public float Cost = 25;
	private AiSupport support;

	public override float GetWeight ()
	{
		if (support == null)
			support = AiSupport.GetSupport (gameObject);

		if (support.Player.Credits < Cost)
			return 0;

		var drones = support.Drones.Count;
		var bases = support.CommandBases.Count;

		if (bases == 0)
			return 0;

		if (drones >= bases * DronesPerBase) return 0;

		return 1;

	}

	public override void Execute ()
	{
		Debug.Log (support.Player.Name + " is creating a drone.");

		var bases = support.CommandBases;
		var index = Random.Range (0, bases.Count);
		var commandBase = bases [index];
		commandBase.GetComponent<CreateUnitAction> ().GetClickAction () ();
	}
}

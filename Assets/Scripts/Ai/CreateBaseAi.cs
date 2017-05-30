using UnityEngine;
using System.Collections;

public class CreateBaseAi : AiBehavior {

	private AiSupport support = null;

	public float Cost = 200;

	public int UnitsPerBase = 5;

	public float RangeFromDrone = 30;

	public int TriesPerDrone = 3;

	public GameObject BasePrefab;

	public override float GetWeight ()
	{
		if (support == null)
			support = AiSupport.GetSupport (gameObject);

		if (support.Player.Credits < Cost || support.Drones.Count == 0)
			return 0;
	
		if (support.CommandBases.Count * UnitsPerBase <= support.Drones.Count)
			return 1;

		return 0;
	}

	public override void Execute ()
	{
		Debug.Log ("Creating Base");

		var go = GameObject.Instantiate (BasePrefab);
		go.AddComponent<Player> ().Info = support.Player;

		foreach (var drone in support.Drones) {
			for (int i = 0; i < TriesPerDrone; i++) {
				var pos = drone.transform.position;
				pos += Random.insideUnitSphere * RangeFromDrone;
				pos.y = Terrain.activeTerrain.SampleHeight (pos);

				go.transform.position = pos;

				if (RtsManager.Current.IsGameObjectSafeToPlace (go)) {
					support.Player.Credits -= Cost;
					return;
				}
			}
		}

		Destroy (go);
	}
}

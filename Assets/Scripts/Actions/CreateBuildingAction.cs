using UnityEngine;
using System.Collections;

public class CreateBuildingAction : ActionBehavior {

	public float Cost = 0;
	public GameObject BuildingPrefab;
	public float MaxBuildDistance = 30;


	public GameObject GhostBuildingPrefab;
	private GameObject active = null;

	public override System.Action GetClickAction ()
	{
		return delegate() {
			var player = GetComponent<Player>().Info;
			if (player.Credits < Cost)
			{
				Debug.Log("Not enough, this costs " + Cost);
				return;
			}

			var go = GameObject.Instantiate(GhostBuildingPrefab);
			var finder = go.AddComponent<FindbuildingSite>();
			finder.BuildingPrefab = BuildingPrefab;
			finder.MaxBuildDistance = MaxBuildDistance;
			finder.Info = player;
			finder.Source = transform;
			finder.Cost = Cost;
			active = go;
		};
	}

	void Update()
	{
		if (active == null)
			return;

		if (Input.GetKeyDown (KeyCode.Escape))
			GameObject.Destroy (active);
	}

	void OnDestroy()
	{
		if (active == null)
			return;

		Destroy (active);
	}


}

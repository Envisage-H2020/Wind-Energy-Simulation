using UnityEngine;
using System.Collections;

public class FindbuildingSite : MonoBehaviour {

	public float Cost = 200;
	public float MaxBuildDistance = 30;
	public GameObject BuildingPrefab;
	public PlayerSetupDefinition Info;
	public Transform Source;

	Renderer rend;
	Color Red = new Color (1, 0, 0, 0.5f);
	Color Green = new Color (0, 1, 0, 0.5f);

	void Start()
	{
		MouseManager.Current.enabled = false;
		rend = GetComponent<Renderer> ();
	}

	// Update is called once per frame
	void Update () {
		var tempTarget = RtsManager.Current.ScreenPointToMapPosition (Input.mousePosition);
		if (tempTarget.HasValue == false)
			return;

		transform.position = tempTarget.Value;

		if (Vector3.Distance (transform.position, Source.position) > MaxBuildDistance) {
			rend.material.color = Red;
			return;
		}

		if (RtsManager.Current.IsGameObjectSafeToPlace (gameObject)) {
			rend.material.color = Green;
			if (Input.GetMouseButtonDown (0)) {
				var go = GameObject.Instantiate (BuildingPrefab);
				go.AddComponent<ActionSelect> ();
				go.transform.position = transform.position;
				Info.Credits -= Cost;
				go.AddComponent<Player> ().Info = Info;
				Destroy (this.gameObject);
			}
		} else {
			rend.material.color = Red;
		}
	}

	void OnDestroy()
	{
		MouseManager.Current.enabled = true;
	}

}

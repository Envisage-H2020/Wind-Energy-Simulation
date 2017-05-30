using UnityEngine;
using System.Collections;

public class CreateUnitAction : ActionBehavior {

	public GameObject Prefab;
	public float Cost = 0;
	private PlayerSetupDefinition player;

	// Use this for initialization
	void Start () {
		player = GetComponent<Player> ().Info;
	}
	
	public override System.Action GetClickAction ()
	{
		return delegate() {
			if (player.Credits < Cost) {
				Debug.Log ("Cannot Create, It costs " + Cost);
				return;
			}

			var go = (GameObject)GameObject.Instantiate (
				         Prefab,
				         transform.position,
				         Quaternion.identity);
			go.AddComponent<Player> ().Info = player;
			go.AddComponent<RightClickNavigation> ();
			go.AddComponent<ActionSelect> ();
			player.Credits -= Cost;
		};
	}
}

using UnityEngine;
using System.Collections;

public class AttackInRange : MonoBehaviour {

	public GameObject ImpactVisual;
	public float FindTargetDelay = 1;
	public float AttackRange = 20;
	public float AttackFrequency = 0.25f;
	public float AttackDamage = 1;

	private ShowUnitInfo target;
	private PlayerSetupDefinition player;
	private float findTargetCounter = 0;
	private float attackCounter = 0;

	// Use this for initialization
	void Start () {
		player = GetComponent<Player> ().Info;
	}

	void FindTarget()
	{
		if (target != null) {
			if (Vector3.Distance (transform.position, target.transform.position) > AttackRange) {
				return;
			}
		}

		foreach (var p in RtsManager.Current.Players) {
			if (p == player)
				continue;

			foreach (var unit in p.ActiveUnits) {
				if (Vector3.Distance (unit.transform.position, transform.position) < AttackRange) {
					target = unit.GetComponent<ShowUnitInfo> ();
					return;
				}
			}
		}
		target = null;
	}

	void Attack()
	{
		if (target == null)
			return;
		target.CurrentHealth -= AttackDamage;
		GameObject.Instantiate (ImpactVisual, target.transform.position, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
		findTargetCounter += Time.deltaTime;
		if (findTargetCounter > FindTargetDelay) {
			FindTarget ();
			findTargetCounter = 0;
		}

		attackCounter += Time.deltaTime;
		if (attackCounter > AttackFrequency) {
			Attack ();
			attackCounter = 0;
		}
	}
}

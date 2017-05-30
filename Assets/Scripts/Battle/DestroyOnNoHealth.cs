using UnityEngine;
using System.Collections;

public class DestroyOnNoHealth : MonoBehaviour {

	public GameObject ExplosionPrefab;
	private ShowUnitInfo info;

	// Use this for initialization
	void Start () {
		info = GetComponent<ShowUnitInfo> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (info.CurrentHealth <= 0) {
			Destroy (this.gameObject);
			GameObject.Instantiate (ExplosionPrefab, transform.position, Quaternion.identity);
		}
	}
}

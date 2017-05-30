using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MapBlip : MonoBehaviour {

	private GameObject blip;

	public GameObject Blip { get { return blip; } }

	// Use this for initialization
	void Start () {
		blip = GameObject.Instantiate (Map.Current.BlipPrefab);
		blip.transform.parent = Map.Current.transform;
		var color = GetComponent<Player> ().Info.AccentColor;
		blip.GetComponent<Image> ().color = color;
	}
	
	// Update is called once per frame
	void Update () {
		blip.transform.position = Map.Current.WorldPositionToMap (transform.position);
	}

	void OnDestroy()
	{
		GameObject.Destroy (blip);
	}
}

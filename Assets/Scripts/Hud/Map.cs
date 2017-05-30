using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Map : MonoBehaviour {

	public RectTransform ViewPort;
	public Transform Corner1, Corner2;
	public GameObject BlipPrefab;
	public static Map Current;

	private Vector2 terrainSize; 

	private RectTransform mapRect;

	public Map()
	{
		Current = this;
	}

	// Use this for initialization
	void Start () {
		terrainSize = new Vector2 (
			Corner2.position.x - Corner1.position.x,
			Corner2.position.z - Corner1.position.z);

		mapRect = GetComponent<RectTransform> ();
	}

	public Vector2 WorldPositionToMap(Vector3 point)
	{
		var pos = point - Corner1.position;
		var mapPos = new Vector2 (
			point.x / terrainSize.x * mapRect.rect.width,
			point.z / terrainSize.y * mapRect.rect.height);
		return mapPos;
	}
	
	// Update is called once per frame
	void Update () {
		ViewPort.position = WorldPositionToMap (Camera.main.transform.position);
	}
}

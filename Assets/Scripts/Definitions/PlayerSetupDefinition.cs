using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PlayerSetupDefinition  {

	public string Name;

	public Transform Location;

	public Color AccentColor;

	public List<GameObject> StartingUnits = new List<GameObject>();

	private List<GameObject> activeUnits = new List<GameObject> ();

	public List<GameObject> ActiveUnits { get { return activeUnits; } }

	public bool IsAi;

	public float Credits;
}

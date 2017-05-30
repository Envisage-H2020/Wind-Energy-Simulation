using UnityEngine;
using System.Collections.Generic;

public class AiController : MonoBehaviour {

	public string PlayerName;
	public float Confusion = 0.3f;
	public float Frequency = 1;

	private PlayerSetupDefinition player;
	private float waited = 0;
	private List<AiBehavior> Ais = new List<AiBehavior>();

	public PlayerSetupDefinition Player { get { return player; } }

	// Use this for initialization
	void Start () {
		foreach (var ai in GetComponents<AiBehavior>()) {
			Ais.Add (ai);
		}
		foreach (var p in RtsManager.Current.Players) {
			if (p.Name == PlayerName) player = p;
		}
		gameObject.AddComponent<AiSupport> ().Player = player;
	}
	
	// Update is called once per frame
	void Update () {
		waited += Time.deltaTime;
		if (waited < Frequency)
			return;

		string aiLog = "";
		float bestAiValue = float.MinValue;
		AiBehavior bestAi = null;
		AiSupport.GetSupport (gameObject).Refresh ();
		foreach (var ai in Ais) {
			ai.TimePassed += waited;
			var aiValue = ai.GetWeight() * ai.WeightMultiplier + Random.Range(0, Confusion);
			aiLog += ai.GetType ().Name + ": " + aiValue + "\n";
			if (aiValue > bestAiValue)
			{
				bestAiValue = aiValue;
				bestAi = ai;
			}
		}

		Debug.Log (aiLog);
		bestAi.Execute ();
		waited = 0;
	}
}

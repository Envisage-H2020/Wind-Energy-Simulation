using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class ActionsManager : MonoBehaviour {

	public static ActionsManager Current;

	public Button[] Buttons;

	private List<Action> actionCalls = new List<Action>();

	public ActionsManager()
	{
		Current = this;
	}

	public void ClearButtons()
	{
		foreach (var b in Buttons)
			b.gameObject.SetActive (false);

		actionCalls.Clear ();
	}

	public void AddButton(Sprite pic, Action onClick)
	{
		int index = actionCalls.Count;
		Buttons [index].gameObject.SetActive (true);
		Buttons [index].GetComponent<Image> ().sprite = pic;
		actionCalls.Add (onClick);
	}

	public void OnButtonClick (int index)
	{
		actionCalls [index] ();
	}


	// Use this for initialization
	void Start () {
		for (int i = 0; i < Buttons.Length; i++) {
			var index = i;
			Buttons[index].onClick.AddListener(delegate() {
				OnButtonClick (index);
			});
		}

		ClearButtons ();
	}
}

using UnityEngine;
using System.Collections;

public class ActionSelect : Interaction {

	public override void Deselect ()
	{
		ActionsManager.Current.ClearButtons ();
	}

	public override void Select ()
	{
		ActionsManager.Current.ClearButtons ();
		foreach (var ab in GetComponents<ActionBehavior>()) {
			ActionsManager.Current.AddButton(
				ab.ButtonPic, 
				ab.GetClickAction());
		}
	}
}

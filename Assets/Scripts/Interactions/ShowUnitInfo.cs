using UnityEngine;
using System.Collections;

public class ShowUnitInfo : Interaction {

	public string Name;
	public float MaxHealth, CurrentHealth;
	public Sprite ProfilePic;

	bool show = false;

	public override void Select ()
	{
		show = true;
	}

	void Update ()
	{
		if (!show)
			return;
		InfoManager.Current.SetPic (ProfilePic);
		InfoManager.Current.SetLines (
			Name, 
			CurrentHealth + "/" + MaxHealth,
			"Owner: " + GetComponent<Player> ().Info.Name);
	}

	public override void Deselect ()
	{
		InfoManager.Current.ClearPic ();
		InfoManager.Current.ClearLines ();
		show = false;
	}
}

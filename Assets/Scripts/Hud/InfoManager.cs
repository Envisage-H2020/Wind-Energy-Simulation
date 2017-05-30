using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InfoManager : MonoBehaviour {

	public static InfoManager Current;

	public Image ProfilePic;
	public Text Line1, Line2, Line3;

	public InfoManager()
	{
		Current = this;
	}

	public void SetLines(string line1, string line2, string line3)
	{
		Line1.text = line1;
		Line2.text = line2;
		Line3.text = line3;
	}

	public void ClearLines()
	{
		SetLines ("", "", "");
	}

	public void SetPic(Sprite pic)
	{
		ProfilePic.sprite = pic;
		ProfilePic.color = Color.white;
	}

	public void ClearPic()
	{
		ProfilePic.color = Color.clear;
	}

	// Use this for initialization
	void Start () {
		ClearLines ();
		ClearPic ();
	}
}

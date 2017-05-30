using UnityEngine;
using System;
using System.Collections;

public abstract class ActionBehavior : MonoBehaviour {

	public abstract Action GetClickAction();

	public Sprite ButtonPic;
}

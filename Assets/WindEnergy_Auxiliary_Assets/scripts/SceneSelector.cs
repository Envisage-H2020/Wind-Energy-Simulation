using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SceneSelector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	RectTransform rt;
	Button bt_scene;
	Text txt_bt_scene_title, txt_bt_scene_short_descr;
	Shadow sh;

	// Use this for initialization
	void Start () {
		rt = gameObject.GetComponent<RectTransform>();
		sh = gameObject.GetComponent<Shadow> ();
			
		bt_scene 		         = gameObject.transform.Find ("bt_scene").GetComponent<Button>();
		txt_bt_scene_title       = gameObject.transform.Find ("bt_scene").gameObject.transform.Find ("txt_bt_scene_title").GetComponent<Text>();
		txt_bt_scene_short_descr = gameObject.transform.Find ("bt_scene").gameObject.transform.Find ("txt_bt_scene_short_descr").GetComponent<Text>();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		rt.localScale = new Vector3 (1.1f, 1.1f, 1); 
		bt_scene.GetComponent<Image> ().color = Color.white;
		txt_bt_scene_title.color = Color.black;
		txt_bt_scene_short_descr.color = Color.black;
		sh.effectColor = new Color32(127/255,127/255,127/255,127);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		rt.localScale = new Vector3 (1, 1, 1); 
		bt_scene.GetComponent<Image> ().color = new Color32(0,0,0,127);
		txt_bt_scene_title.color = Color.white;
		txt_bt_scene_short_descr.color = Color.white;
		sh.effectColor = new Color32(255/255,255/255,255/255,0);
	}
}

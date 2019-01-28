using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour {

	public Transform menu;
	public Transform controls;

	public void Controls () {
		menu.gameObject.SetActive(false);
		controls.gameObject.SetActive(true);
	}

	public void Retour () {
		menu.gameObject.SetActive(true);
		controls.gameObject.SetActive(false);
	}
}

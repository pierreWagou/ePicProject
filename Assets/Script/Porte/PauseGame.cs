using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PauseGame : MonoBehaviour {
	public Transform canvas;
	public Transform Player;

	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)) {
			if(canvas.gameObject.activeInHierarchy == false) {
				canvas.gameObject.SetActive(true);
				Time.timeScale = 0;
			}
			else {
				canvas.gameObject.SetActive(false);
				Time.timeScale = 1;
			}
		}
	}

	public void Pause() {
		if(canvas.gameObject.activeInHierarchy == false) {
			canvas.gameObject.SetActive(true);
			Time.timeScale = 0;
		}
		else {
			canvas.gameObject.SetActive(false);
			Time.timeScale = 1;
		}
	}
}

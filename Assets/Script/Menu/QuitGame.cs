using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitGame : MonoBehaviour {

	public void End (bool Quit) {
		if(Quit) {
			Time.timeScale = 1;
			SceneManager.LoadScene("StartMenu");
		}
	}
}

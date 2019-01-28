using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	public void RetourMenu () {
		SceneManager.LoadScene("StartMenu");
	}

	public void Credits () {
		SceneManager.LoadScene("Credits");
	}

	public void Play () {
		SceneManager.LoadScene("Pic");
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour {

	public void Begin () {
		PlayerPrefs.SetFloat("xPic", -10);
		PlayerPrefs.SetFloat("yPic", 4);
		PlayerPrefs.SetFloat("zPic", 7);
		PlayerPrefs.SetFloat("xMDE", -3);
		PlayerPrefs.SetFloat("yMDE", 0);
		PlayerPrefs.SetFloat("zMDE", -5);
		PlayerPrefs.SetFloat("xWC", 2);
		PlayerPrefs.SetFloat("yWC", 1);
		PlayerPrefs.SetFloat("zWC", 3);
		PlayerPrefs.SetString("content", string.Empty);
		PlayerPrefs.SetInt("Fut de Delirium", 0);
		PlayerPrefs.SetInt("Fut de Barbar", 0);
		PlayerPrefs.SetInt("Fut de Cuvee", 0);
		PlayerPrefs.SetInt("Fut de Bok", 0);
		PlayerPrefs.SetInt("Fut de ValDieu", 0);
		PlayerPrefs.SetInt("Clé de la réserve", 0);
		PlayerPrefs.SetInt("fouille", 0);
		PlayerPrefs.SetInt("question", 0);
		PlayerPrefs.SetInt("porte", 0);
		PlayerPrefs.SetInt("babypong", 0);
		PlayerPrefs.SetInt("recompense", 0);
		SceneManager.LoadScene ("Intro");
	}
}

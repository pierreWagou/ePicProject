using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

	public Transform text;
	private GameObject _player;
	private int d, ba, c, bo, v;

	void Start () {
		d = PlayerPrefs.GetInt("Fut de Delirium");
		ba = PlayerPrefs.GetInt("Fut de Barbar");
		c = PlayerPrefs.GetInt("Fut de Cuvee");
		bo = PlayerPrefs.GetInt("Fut de Bok");
		v = PlayerPrefs.GetInt("Fut de ValDieu");
		_player = GameObject.FindGameObjectWithTag("Player");
	}

	void Update() {
		float distance = Vector3.Distance(this.gameObject.transform.position, _player.transform.position);
		if(distance<=2)
			text.gameObject.SetActive(true);
		else
			text.gameObject.SetActive(false);
		if(Input.GetKeyDown(KeyCode.E))
			if(d==1 && ba==1 && c==1 && bo==1 && v==1)
		 		SceneManager.LoadScene("End");
			else
				Debug.Log("oupsi");
	}
}

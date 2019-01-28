using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleportation : MonoBehaviour {

	public Transform Player;
	private GameObject _player;
	public Transform boite;
	public Transform text;


	void Start () {
		_player = GameObject.FindGameObjectWithTag("Player");
	}

	void Update () {
		float distance = Vector3.Distance(this.gameObject.transform.position, _player.transform.position);
		if(distance<=2)
			text.gameObject.SetActive(true);
		else {
			text.gameObject.SetActive(false);
			boite.gameObject.SetActive(false);
		}
		if(Input.GetKeyDown(KeyCode.E) && distance<=2) {
			text.gameObject.SetActive(false);
			boite.gameObject.SetActive(true);
		}
		if(Input.GetKeyDown(KeyCode.T) && distance<=2)
				SceneManager.LoadScene("End");
	}
}

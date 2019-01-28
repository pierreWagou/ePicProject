using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PorteFermee : MonoBehaviour {

	public Transform Player;
	public Inventory inventory;
	public Transform text;
	private GameObject _player;

	void Start () {
		_player = GameObject.FindGameObjectWithTag("Player");
		Player.position = new Vector3(PlayerPrefs.GetFloat("xWC"), PlayerPrefs.GetFloat("yWC"), PlayerPrefs.GetFloat("zWC"));
		inventory.LoadInventory();
	}

	void Update() {
		float distance = Vector3.Distance(this.gameObject.transform.position, _player.transform.position);
		if(distance<=1)
			text.gameObject.SetActive(true);
		else
			text.gameObject.SetActive(false);
		if(Input.GetKeyDown(KeyCode.E) && distance<=1) {
			PlayerPrefs.SetFloat("xWC", Player.position.x);
			PlayerPrefs.SetFloat("yWC", Player.position.y);
			PlayerPrefs.SetFloat("zWC", Player.position.z);
			inventory.SaveInventory();
			SceneManager.LoadScene ("Toilet");
		}

	}
}

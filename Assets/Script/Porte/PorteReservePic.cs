using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PorteReservePic : MonoBehaviour {

	public Inventory inventory;
	public Transform text;
	private GameObject _player;

	void Start () {
		_player = GameObject.FindGameObjectWithTag("Player");
		inventory.LoadInventory();
	}

	void Update() {
		float distance = Vector3.Distance(this.gameObject.transform.position, _player.transform.position);
		if(distance<=1)
			text.gameObject.SetActive(true);
		else
			text.gameObject.SetActive(false);

		if(Input.GetKeyDown(KeyCode.E) && distance<=1) {
			inventory.SaveInventory();
			SceneManager.LoadScene ("Pic");
		}
	}
}

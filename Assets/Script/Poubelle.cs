using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poubelle : MonoBehaviour {

	public Inventory inventory;
	public Item item;
	private GameObject _player;

	void start () {
		_player = GameObject.FindGameObjectWithTag("Player");
	}

	void Update () {
		float distance = Vector3.Distance(this.gameObject.transform.position, _player.transform.position);
		if(Input.GetKeyDown(KeyCode.E) && distance<=2) {
			inventory.AddItem(item);
			PlayerPrefs.SetInt("Fut de Bok", 1);
		}
	}
}

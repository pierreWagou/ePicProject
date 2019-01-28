using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public Inventory inventory;

	// private void OnTriggerEnter(Collider other) {
	// 	if(other.tag=="Item") {
	// 		inventory.AddItem(other.GetComponent<Item>());
	// 		Destroy(other.gameObject);
	// 	}
	// }

	private void OnCollisionEnter(Collision collision) {
		if(collision.gameObject.tag=="Item") {
			inventory.AddItem(collision.gameObject.GetComponent<Item>());
			Destroy(collision.gameObject);
		}
	}
}

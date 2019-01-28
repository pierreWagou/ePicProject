using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fouille : MonoBehaviour {

	public Inventory inventory;
	private GameObject _player;
	public Transform text;
	public Transform boite;
	public Transform fouille1;
	public Transform fouille2;
	public Transform fouille3;
	private int i;
	public Item item;

	void Start () {
		_player = GameObject.FindGameObjectWithTag("Player");
		i=PlayerPrefs.GetInt("fouille");
	}

	void Update () {
		float distance = Vector3.Distance(this.gameObject.transform.position, _player.transform.position);
		if(distance<=2 && i<=3)
			text.gameObject.SetActive(true);
		else {
			text.gameObject.SetActive(false);
			boite.gameObject.SetActive(false);
		}
		if(Input.GetKeyDown(KeyCode.E) && distance<=2) {
			i++;
			text.gameObject.SetActive(false);
			boite.gameObject.SetActive(true);
			switch(i) {
				case 1:
					fouille1.gameObject.SetActive(true);
					text.gameObject.SetActive(true);
					break;
				case 2:
					fouille1.gameObject.SetActive(false);
					fouille2.gameObject.SetActive(true);
					text.gameObject.SetActive(true);
					break;
				case 3:
					fouille2.gameObject.SetActive(false);
					fouille3.gameObject.SetActive(true);
					text.gameObject.SetActive(true);
					inventory.AddItem(item);
					PlayerPrefs.SetInt("Fut de Cuvee", 1);
					break;
			}
			text.gameObject.SetActive(true);
			PlayerPrefs.SetInt("fouille",i);
		}
	}
}

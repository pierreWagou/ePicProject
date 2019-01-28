using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JouerBabyPong : MonoBehaviour {

	public Transform Player;
	public Inventory inventory;
	public Item item;
	public Transform dialogue;
	public Transform gagne;
	public Transform perdu;
	public Transform recompense;
	public Transform text;
	private GameObject _player;
	private int i, j;

	void Start () {
		_player = GameObject.FindGameObjectWithTag("Player");
		Player.position = new Vector3(PlayerPrefs.GetFloat("xPic"), PlayerPrefs.GetFloat("yPic"), PlayerPrefs.GetFloat("zPic"));
		i = PlayerPrefs.GetInt("babypong");
		j = PlayerPrefs.GetInt("recompense");
		inventory.LoadInventory();
	}

	void Update() {
		float distance = Vector3.Distance(this.gameObject.transform.position, _player.transform.position);
		int k = PlayerPrefs.GetInt("Fut de Bok");
		if(distance<=2 && k==0) {
			text.gameObject.SetActive(true);
			if(i==1) {
				dialogue.gameObject.SetActive(true);
				if(j==1) {
					recompense.gameObject.SetActive(true);
					inventory.AddItem(item);
					PlayerPrefs.SetInt("Fut de Bok", 1);
					j = 2;
					PlayerPrefs.SetInt("recompense", 2);
				}
				if(j==0)
					gagne.gameObject.SetActive(true);
				}
			if(i==2) {
				dialogue.gameObject.SetActive(true);
				perdu.gameObject.SetActive(true);
			}
		}
		if(distance>2) {
			text.gameObject.SetActive(false);
			dialogue.gameObject.SetActive(false);
			gagne.gameObject.SetActive(false);
			perdu.gameObject.SetActive(false);
			recompense.gameObject.SetActive(false);
			i = 0;
		}
		if(Input.GetKeyDown(KeyCode.E) && distance<=2 && k==0) {
			gagne.gameObject.SetActive(false);
			perdu.gameObject.SetActive(false);
			recompense.gameObject.SetActive(false);
			PlayerPrefs.SetFloat("xPic", Player.position.x);
			PlayerPrefs.SetFloat("yPic", Player.position.y);
			PlayerPrefs.SetFloat("zPic", Player.position.z);
			inventory.SaveInventory();
			SceneManager.LoadScene ("BabyPong");
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question : MonoBehaviour {

	public Inventory inventory;
	public Item item;
	private GameObject _player;
	public Transform boite;
	public Transform text;
	public Transform dialogue;
	public Transform gagne;
	public Transform perdu;
	public Transform button;
	private int i;


	void Start () {
		_player = GameObject.FindGameObjectWithTag("Player");
		i = PlayerPrefs.GetInt("question");
	}

	void Update () {
		float distance = Vector3.Distance(this.gameObject.transform.position, _player.transform.position);
			if(distance<=2  && i==0)
				text.gameObject.SetActive(true);
			else if(distance>=2){
				text.gameObject.SetActive(false);
				boite.gameObject.SetActive(false);
			}
			if(Input.GetKeyDown(KeyCode.E) && distance<=2 && i==0) {
				gagne.gameObject.SetActive(false);
				perdu.gameObject.SetActive(false);
				text.gameObject.SetActive(false);
				boite.gameObject.SetActive(true);
				dialogue.gameObject.SetActive(true);
				button.gameObject.SetActive(true);
			}
			if(Input.GetKeyDown(KeyCode.F))
				BonneReponse();
			if(Input.GetKeyDown(KeyCode.B))
				MauvaiseReponse();
	}

	public void BonneReponse() {
		dialogue.gameObject.SetActive(false);
		gagne.gameObject.SetActive(true);
		inventory.AddItem(item);
		PlayerPrefs.SetInt("Fut de ValDieu", 1);
		i = 1;
		PlayerPrefs.SetInt("question", i);
		button.gameObject.SetActive(false);
	}

	public void MauvaiseReponse(){
		dialogue.gameObject.SetActive(false);
		perdu.gameObject.SetActive(true);
		button.gameObject.SetActive(false);

	}
}

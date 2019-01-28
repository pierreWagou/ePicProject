using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {DELIRIUM, CUVEE, VALDIEU, BARBAR, BOK, KEY};
public enum Quality {COMMON, UNCOMMON, RARE, EPIC, LEGENDARY, ARTIFACT};

public class Item : MonoBehaviour {

	public Inventory inventory;
	public Transform text;
	public ItemType type;
	public Quality quality;
	public Sprite spriteNeutral;
	public Sprite spriteHighlighted;
	private GameObject player;
	public int maxSize;
	public float strength, intellect, agility, stamina;
	public string itemName;
	public string description;
	private GameObject _player;
	private int i;

	void Start () {
		_player = GameObject.FindGameObjectWithTag("Player");
		i = PlayerPrefs.GetInt("fut");
		if(PlayerPrefs.GetInt(itemName)==1)
			Destroy(this.gameObject);
	}

	void Update () {
		float distance = Vector3.Distance(this.gameObject.transform.position, _player.transform.position);
		if(distance<=1)
		 	text.gameObject.SetActive(true);
	 	else
			text.gameObject.SetActive(false);
			if(Input.GetKeyDown(KeyCode.E) && distance<=1) {
				inventory.AddItem(this);
				PlayerPrefs.SetInt(itemName, 1);
				Destroy(this.gameObject);
			}
		PlayerPrefs.SetInt("fut", i);
	}

	public void Use() {
		switch(type) {
			case ItemType.DELIRIUM:
				break;
			case ItemType.CUVEE:
				i++;
				break;
			case ItemType.VALDIEU:
				i++;
				break;
			case ItemType.BARBAR:
				i++;
				break;
			case ItemType.BOK:
				i++;
				break;
			case ItemType.KEY:
				break;
		}
	}

	public string GetTooltip () {
		string stats = string.Empty;
		string color = string.Empty;
		string newLine = string.Empty;
		if(description!=string.Empty)
			newLine = "\n";
		switch(quality) {
			case Quality.COMMON:
				color = "white";
				break;
			case Quality.UNCOMMON:
				color = "lime";
				break;
			case Quality.RARE:
				color = "navy";
				break;
			case Quality.EPIC:
				color = "magenta";
				break;
			case Quality.LEGENDARY:
				color = "orange";
				break;
			case Quality.ARTIFACT:
				color = "red";
				break;
		}
		if(strength>0)
			stats += "\n+"+strength.ToString()+" Strength";
		if(intellect>0)
			stats += "\n+"+intellect.ToString()+" Intellect";
		if(agility>0)
			stats += "\n+"+agility.ToString()+" Agility";
		if(stamina>0)
			stats += "\n+"+stamina.ToString()+" Stamina";
		return string.Format("<color="+color+"><size=16>{0}</size></color><size=14><i><color=lime>"+newLine+"{1}</color></i>{2}</size>", itemName, description, stats);
	}

	public void SetStats (Item item) {
		this.type = item.type;
		this.quality = item.quality;
		this.spriteNeutral = item.spriteNeutral;
		this.spriteHighlighted = item.spriteHighlighted;
		this.maxSize = item.maxSize;
		this.strength = item.strength;
		this.intellect = item.intellect;
		this.agility = item.agility;
		this.stamina = item.stamina;
		this.itemName = item.itemName;
		this.description = item.description;
		switch(type) {
			case ItemType.DELIRIUM:
				GetComponent<Renderer>().material = item.GetComponent<Renderer>().material;
				break;
			case ItemType.CUVEE:
				GetComponent<Renderer>().material = item.GetComponent<Renderer>().material;
				break;
			case ItemType.VALDIEU:
				GetComponent<Renderer>().material = item.GetComponent<Renderer>().material;
				break;
			case ItemType.BARBAR:
				GetComponent<Renderer>().material = item.GetComponent<Renderer>().material;
				break;
			case ItemType.BOK:
				GetComponent<Renderer>().material = item.GetComponent<Renderer>().material;
				break;
			case ItemType.KEY:
				GetComponent<Renderer>().material = item.GetComponent<Renderer>().material;
				break;
		}
	}
}

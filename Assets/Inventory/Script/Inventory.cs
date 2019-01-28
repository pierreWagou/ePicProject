using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Inventory : MonoBehaviour {

	private RectTransform inventoryRect;
	private float inventoryWidth, inventoryHeight;
	public int slots;
	public int rows;
	public float slotPaddingLeft, slotPaddingTop;
	public float slotSize;
	public GameObject slotPrefab;
	private static Slot from, to;
	private List<GameObject> allSlots;
	public GameObject iconPrefab;
	private static GameObject hoverObject;
	public Canvas canvas;
	private float hoverYOffset;
	public EventSystem eventSystem;
	private static int emptySlots;
	private static CanvasGroup canvasGroup;
	private static Inventory instance;
	private bool fadingIn;
	private bool fadingOut;
	public float fadeTime;
	private static GameObject clicked;
	public GameObject selectStackSize;
	private static GameObject selectStackSizeStatic;
	public Text stackText;
	private int splitAmount;
	private int maxStackCount;
	private static Slot movingSlot;
	public GameObject delirium;
	public GameObject cuvee;
	public GameObject valdieu;
	public GameObject barbar;
	public GameObject bok;
	public GameObject key;
	public GameObject tooltipObject;
	private static GameObject tooltip;
	public Text sizeTextObject;
	private static Text sizeText;
	public Text visualTextObject;
	private static Text visualText;
	public GameObject dropItem;
	private static GameObject playerRef;

	public static int EmptySlots {
		get{ return emptySlots; }
		set{ emptySlots = value; }
	}

	public static CanvasGroup CanvasGroup {
		get{ return Inventory.canvasGroup; }
	}

	public static Inventory Instance {
		get {
			if(instance==null)
			 	instance = GameObject.FindObjectOfType<Inventory>();
			return Inventory.instance;
		}
	}

	void Start () {
		tooltip = tooltipObject;
		sizeText = sizeTextObject;
		visualText = visualTextObject;
		selectStackSizeStatic = selectStackSize;
		playerRef = GameObject.Find("Player");
		canvasGroup = transform.parent.GetComponent<CanvasGroup>();
		CreateLayout();
		movingSlot = GameObject.Find("MovingSlot").GetComponent<Slot>();
	}

	void Update () {
		if(Input.GetMouseButtonUp(0)) {
			if(!eventSystem.IsPointerOverGameObject(-1) && from!=null) {
				from.GetComponent<Image>().color = Color.white;
				foreach(Item item in from.Items) {
					float angle = UnityEngine.Random.Range(0.0f, Mathf.PI*2);
					Vector3 v = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));
					v *= 25;
					GameObject tmpDrop = (GameObject)GameObject.Instantiate(dropItem, playerRef.transform.position - v, Quaternion.identity);
					tmpDrop.GetComponent<Item>().SetStats(item);
				}
				from.ClearSlot();
				Destroy(GameObject.Find("Hover"));
				to=null;
				from=null;
				emptySlots++;
			}
			else if(!eventSystem.IsPointerOverGameObject(-1) && !movingSlot.IsEmpty) {
				foreach(Item item in movingSlot.Items) {
					float angle = UnityEngine.Random.Range(0.0f, Mathf.PI*2);
					Vector3 v = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));
					v *= 25;
					GameObject tmpDrop = (GameObject)GameObject.Instantiate(dropItem, playerRef.transform.position - v, Quaternion.identity);
					tmpDrop.GetComponent<Item>().SetStats(item);
				}
				movingSlot.ClearSlot();
				Destroy(GameObject.Find("Hover"));
			}
		}
		if(hoverObject!=null) {
			Vector2 position;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out position);
			position.Set(position.x, position.y - hoverYOffset);
			hoverObject.transform.position = canvas.transform.TransformPoint(position);
		}
		if(Input.GetKeyDown(KeyCode.I))
			if(canvasGroup.alpha>0) {
				StartCoroutine("FadeOut");
				PutItemBack();
			}
			else
				StartCoroutine("FadeIn");
		if(Input.GetMouseButton(2) && eventSystem.IsPointerOverGameObject(-1))
			MoveInventory();
	}

	public void ShowTooltip (GameObject slot) {
		Slot tmpSlot = slot.GetComponent<Slot>();
		if(!tmpSlot.IsEmpty && hoverObject==null && !selectStackSizeStatic.activeSelf) {
			visualText.text = tmpSlot.CurrentItem.GetTooltip();
			sizeText.text = visualText.text;
			tooltip.SetActive(true);
			float xPos = slot.transform.position.x+slotPaddingLeft;
			float yPos = slot.transform.position.y-slot.GetComponent<RectTransform>().sizeDelta.y-slotPaddingTop;
			tooltip.transform.position = new Vector2(xPos, yPos);
		}
	}

	public void HideTooltip (GameObject slot) {
		tooltip.SetActive(false);
	}


	public void SaveInventory() {
		string content = string.Empty;
		for(int i=0;i<allSlots.Count;i++) {
			Slot tmp = allSlots[i].GetComponent<Slot>();
			if(!tmp.IsEmpty)
				content += i+"-"+tmp.CurrentItem.type.ToString()+"-"+tmp.Items.Count.ToString()+";";
		}
		PlayerPrefs.SetString("content", content);
		PlayerPrefs.SetInt("slots", slots);
		PlayerPrefs.SetInt("rows", rows);
		PlayerPrefs.SetFloat("slotPaddingLeft", slotPaddingLeft);
		PlayerPrefs.SetFloat("slotPaddingTop", slotPaddingTop);
		PlayerPrefs.SetFloat("slotSize", slotSize);
		//PlayerPrefs.SetFloat("xPos", inventoryRect.position.x);
		//PlayerPrefs.SetFloat("yPos", inventoryRect.position.y);
		PlayerPrefs.Save();
	}

	public void LoadInventory() {
		string content = PlayerPrefs.GetString("content");
		slots = PlayerPrefs.GetInt("slots");
		rows = PlayerPrefs.GetInt("rows");
		slotPaddingLeft = PlayerPrefs.GetFloat("slotPaddingLeft");
		slotPaddingTop = PlayerPrefs.GetFloat("slotPaddingTop");
		slotSize = PlayerPrefs.GetFloat("slotSize");
		//inventoryRect.position = new Vector3(PlayerPrefs.GetFloat("xPos"), PlayerPrefs.GetFloat("yPos"), inventoryRect.position.z);
		CreateLayout();
		string[] splitContent = content.Split(';');
		for(int x=0;x<splitContent.Length-1;x++) {
			string[] splitValues = splitContent[x].Split('-');
			int index = Int32.Parse(splitValues[0]);
			ItemType type = (ItemType)Enum.Parse(typeof(ItemType), splitValues[1]);
			int amount = Int32.Parse(splitValues[2]);
			for(int i=0;i<amount;i++)
				switch(type) {
					case ItemType.DELIRIUM:
						allSlots[index].GetComponent<Slot>().AddItem(delirium.GetComponent<Item>());
						break;
					case ItemType.CUVEE:
						allSlots[index].GetComponent<Slot>().AddItem(cuvee.GetComponent<Item>());
						break;
					case ItemType.VALDIEU:
						allSlots[index].GetComponent<Slot>().AddItem(valdieu.GetComponent<Item>());
						break;
					case ItemType.BARBAR:
						allSlots[index].GetComponent<Slot>().AddItem(barbar.GetComponent<Item>());
						break;
					case ItemType.BOK:
						allSlots[index].GetComponent<Slot>().AddItem(bok.GetComponent<Item>());
						break;
					case ItemType.KEY:
						allSlots[index].GetComponent<Slot>().AddItem(key.GetComponent<Item>());
						break;
				}
		}
	}

	private void CreateLayout() {
		slots=10;
		rows=2;
		slotPaddingLeft=5;
		slotPaddingTop=5;
		slotSize=50;
		if(allSlots!=null)
			foreach(GameObject go in allSlots)
				Destroy(go);
		allSlots = new List<GameObject>();
		hoverYOffset = slotSize*0.01f;
		emptySlots = slots;
		int columns = slots/rows;
		inventoryWidth = columns*(slotSize+slotPaddingLeft)+2*slotPaddingLeft;
		inventoryHeight = rows*(slotSize+slotPaddingTop)+2*slotPaddingTop;
		inventoryRect = GetComponent<RectTransform>();
		inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, inventoryWidth);
		inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, inventoryHeight );
		for(int y=0;y<rows;y++) {
			for(int x=0;x<columns;x++) {
				GameObject newSlot = (GameObject)Instantiate(slotPrefab);
				RectTransform slotRect= newSlot.GetComponent<RectTransform>();
				newSlot.name = "Slot";
				newSlot.transform.SetParent(this.transform.parent);
				slotRect.localPosition = inventoryRect.localPosition + new Vector3(slotPaddingLeft*(x+1)+(slotSize*x),-slotPaddingTop*(y+1)-(slotSize*y),0);
				slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize*canvas.scaleFactor);
				slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize*canvas.scaleFactor);
				allSlots.Add(newSlot);
			}
		}
	}

	public bool AddItem (Item item) {
		if(item.maxSize==1) {
			PlaceEmpty(item);
			return true;
		}
		else {
			foreach(GameObject slot in allSlots){
				Slot tmp = slot.GetComponent<Slot>();
				if(!tmp.IsEmpty)
					if(tmp.CurrentItem.type==item.type && tmp.IsAvailable)
						if(!movingSlot.IsEmpty && clicked.GetComponent<Slot>()==tmp.GetComponent<Slot>())
							continue;
						else {
						tmp.AddItem(item);
						return true;
						}
			}
			if(emptySlots>0)
				PlaceEmpty(item);
		}
		return false;
	}

	private void MoveInventory () {
		Vector2 mousePos;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, new Vector3(Input.mousePosition.x-(inventoryRect.sizeDelta.x/2*canvas.scaleFactor), Input.mousePosition.y+(inventoryRect.sizeDelta.y/2*canvas.scaleFactor)), canvas.worldCamera, out mousePos);
		transform.position = canvas.transform.TransformPoint(mousePos);
	}

	private bool PlaceEmpty (Item item) {
		if(emptySlots>0){
			foreach(GameObject slot in allSlots) {
				Slot tmp = slot.GetComponent<Slot>();
				if(tmp.IsEmpty) {
					tmp.AddItem(item);
					emptySlots--;
					return true;
				}
			}
		}
		return false;
	}

	public void MoveItem (GameObject clicked) {
		Inventory.clicked = clicked;
		if(!movingSlot.IsEmpty) {
			Slot tmp = clicked.GetComponent<Slot>();
			if(tmp.IsEmpty) {
				tmp.AddItems(movingSlot.Items);
				movingSlot.Items.Clear();
				Destroy(GameObject.Find("Hover"));
			}
			else if(!tmp.IsEmpty && movingSlot.CurrentItem.type==tmp.CurrentItem.type && tmp.IsAvailable)
				MergeStacks(movingSlot, tmp);
		}
		else if(from==null && canvasGroup.alpha==1 && !Input.GetKey(KeyCode.LeftShift))
			if(!clicked.GetComponent<Slot>().IsEmpty) {
				from = clicked.GetComponent<Slot>();
				from.GetComponent<Image>().color=Color.gray;
				CreateHoverIcon();
			}
		else if(to==null && !Input.GetKey(KeyCode.LeftShift)) {
			to = clicked.GetComponent<Slot>();
			Destroy(GameObject.Find("Hover"));
		}
		if(from!=null && to!=null) {
			if(!to.IsEmpty && from.CurrentItem.type==to.CurrentItem.type && to.IsAvailable)
				MergeStacks(from, to);
			else {
			Stack<Item> tmp = new Stack<Item>(to.Items);
			to.AddItems(from.Items);
			if(tmp.Count==0)
				from.ClearSlot();
			else
				from.AddItems(tmp);
			}
			from.GetComponent<Image>().color = Color.white;
			to=null;
			from=null;
			Destroy(GameObject.Find("Hover"));
		}
	}

	private void CreateHoverIcon () {
		hoverObject = (GameObject)Instantiate(iconPrefab);
		hoverObject.GetComponent<Image>().sprite = clicked.GetComponent<Image>().sprite;
		hoverObject.name = "Hover";
		RectTransform hoverTransform = hoverObject.GetComponent<RectTransform>();
		RectTransform clickedTransform = clicked.GetComponent<RectTransform>();
		hoverTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, clickedTransform.sizeDelta.x);
		hoverTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, clickedTransform.sizeDelta.y);
		hoverObject.transform.SetParent(GameObject.Find("Canvas").transform, true);
		hoverObject.transform.localScale = clicked.gameObject.transform.localScale;
		hoverObject.transform.GetChild(0).GetComponent<Text>().text = movingSlot.Items.Count>1 ? movingSlot.Items.Count.ToString() : string.Empty;
	}

	private void PutItemBack () {
		if(from!=null) {
			Destroy(GameObject.Find("Hover"));
			from.GetComponent<Image>().color = Color.white;
			from = null;
		}
		else if(!movingSlot.IsEmpty) {
			Destroy(GameObject.Find("Hover"));
			foreach(Item item in movingSlot.Items)
				clicked.GetComponent<Slot>().AddItem(item);
			movingSlot.ClearSlot();
		}
		selectStackSize.SetActive(false);
	}

	public void SetStackInfo (int maxStackCount) {
		selectStackSize.SetActive(true);
		tooltip.SetActive(false);
		splitAmount = 0;
		this.maxStackCount = maxStackCount;
		stackText.text = splitAmount.ToString();
	}

	public void SplitStack () {
		selectStackSize.SetActive(false);
		if(splitAmount==maxStackCount)
			MoveItem(clicked);
		else if(splitAmount>0) {
			movingSlot.Items = clicked.GetComponent<Slot>().RemoveItems(splitAmount);
			CreateHoverIcon();
		}
	}

	public void ChangeStackText (int i) {
		splitAmount += i;
		if(splitAmount<0)
			splitAmount = 0;
		if(splitAmount>maxStackCount)
			splitAmount=maxStackCount;
		stackText.text = splitAmount.ToString();
	}

	public void MergeStacks (Slot source, Slot destination) {
		int max = destination.CurrentItem.maxSize - destination.Items.Count;
		int count = source.Items.Count<max ? source.Items.Count : max;
		for(int i=0;i<count;i++) {
			destination.AddItem(source.RemoveItem());
			hoverObject.transform.GetChild(0).GetComponent<Text>().text = movingSlot.Items.Count.ToString();
		}
		if(source.Items.Count==0) {
			source.ClearSlot();
			Destroy(GameObject.Find("Hover"));
		}
	}

	private IEnumerator FadeOut () {
		if(!fadingOut) {
			fadingOut = true;
			fadingIn = false;
			StopCoroutine("FadeIn");
			float startAlpha = canvasGroup.alpha;
			float rate = 1.0f/fadeTime;
			float progress = 0.0f;
			while(progress<1.0) {
				canvasGroup.alpha = Mathf.Lerp(startAlpha,0,progress);
				progress += rate*Time.deltaTime;
				yield return null;
			}
			canvasGroup.alpha = 0;
			fadingOut = false;
		}
	}

	private IEnumerator FadeIn () {
		if(!fadingIn) {
			fadingIn = true;
			fadingOut = false;
			StopCoroutine("FadeOut");
			float startAlpha = canvasGroup.alpha;
			float rate = 1.0f/fadeTime;
			float progress = 0.0f;
			while(progress<1.0) {
				canvasGroup.alpha = Mathf.Lerp(startAlpha,1,progress);
				progress += rate*Time.deltaTime;
				yield return null;
			}
			canvasGroup.alpha = 1;
			fadingIn = false;
		}
	}
}

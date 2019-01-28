using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler {

	private Stack<Item> items;
	public Text stackText;
	public Sprite slotEmpty;
	public Sprite slotHighlight;

	public Stack<Item> Items {
		get{ return items; }
		set{ items = value; }
	}

	public bool IsEmpty {
		get{ return items.Count==0; }
	}

	public bool IsAvailable {
		get{ return CurrentItem.maxSize>items.Count; }
	}

	public Item CurrentItem {
		get{ return items.Peek(); }
	}

	void Awake (){
		 items = new Stack<Item>();
	}

	void Start () {
		RectTransform slotRect = GetComponent<RectTransform>();
		RectTransform textRect = stackText.GetComponent<RectTransform>();
		int textScaleFactor = (int)(slotRect.sizeDelta.x*0.6);
		stackText.resizeTextMaxSize = textScaleFactor;
		stackText.resizeTextMinSize = textScaleFactor;
		textRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotRect.sizeDelta.x);
		textRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotRect.sizeDelta.y);
	}

	void Update () {

	}

	public void AddItem (Item item) {
		 items.Push(item);
		 if(items.Count>1)
		 	stackText.text = items.Count.ToString();
			ChangeSprite(item.spriteNeutral, item.spriteHighlighted);
	}

	public void AddItems (Stack<Item> items) {
		this.items = new Stack<Item>(items);
		stackText.text = items.Count>1 ? items.Count.ToString() : string.Empty;
		ChangeSprite(CurrentItem.spriteNeutral, CurrentItem.spriteHighlighted);

	}

	private void ChangeSprite (Sprite neutral, Sprite highlight) {
		GetComponent<Image>().sprite = neutral;
		SpriteState st = new SpriteState();
		st.highlightedSprite = highlight;
		st.pressedSprite = neutral;
		GetComponent<Button>().spriteState = st;
	}

	private void UseItem() {
		if(!IsEmpty) {
			items.Pop().Use();
			stackText.text = items.Count>1 ? items.Count.ToString() : string.Empty;
			if(IsEmpty) {
				ChangeSprite(slotEmpty,slotHighlight);
				Inventory.EmptySlots++;
			}
		}
	}

	public void ClearSlot () {
		items.Clear();
		ChangeSprite(slotEmpty,slotHighlight);
		stackText.text=string.Empty;
	}

	public Stack<Item> RemoveItems (int amount) {
		Stack<Item> tmp = new Stack<Item>();
		for(int i=0;i<amount;i++)
			tmp.Push(items.Pop());
		stackText.text = items.Count>1 ? items.Count.ToString() : string.Empty;
		return tmp;
	}

	public Item RemoveItem () {
		Item tmp;
		tmp = items.Pop();
		stackText.text = items.Count>1 ? items.Count.ToString() : string.Empty;
		return tmp;
	}

	public void OnPointerClick(PointerEventData eventData) {
		if(eventData.button==PointerEventData.InputButton.Right && !GameObject.Find("Hover") && Inventory.CanvasGroup.alpha>0)
			UseItem();
		else if(eventData.button==PointerEventData.InputButton.Left && Input.GetKey(KeyCode.LeftShift) && !GameObject.Find("Hover")) {
			Vector2 position;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(Inventory.Instance.canvas.transform as RectTransform, Input.mousePosition, Inventory.Instance.canvas.worldCamera, out position);
			Inventory.Instance.selectStackSize.SetActive(true);
			Inventory.Instance.selectStackSize.transform.position = Inventory.Instance.canvas.transform.TransformPoint(position);
			Inventory.Instance.SetStackInfo(items.Count);
		}
	}
}

// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace woco_urogue {

	// ===================================================================================
	// ITEM SLOT
	// ===================================================================================
	public class ItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler, IDragHandler {
   
		public System.Action<ItemSlot> onDblClick;
		public System.Action<ItemSlot, bool> onHover;
		public System.Action<ItemSlot, bool> onDrag;

		public Image icon;
		public Sprite defaultIcon;
		public Text numText;
		
		public bool showHotkeyLabel = false;
		public SlotTypes slotType = SlotTypes.Inventory;
		
		public int id;
		public Item ItemInSlot { get; private set; }
		
		// -----------------------------------------------------------------------------------
		// Start
		// -----------------------------------------------------------------------------------
		void Start() {
			if (showHotkeyLabel == true) {
				numText.gameObject.SetActive(true);
				numText.text = (id + 1).ToString();
			} else {
				numText.gameObject.SetActive(false);
			}
		}
		
		// -----------------------------------------------------------------------------------
		// Initalize
		// -----------------------------------------------------------------------------------
		public void Initalize(int myid, Item item=null) {
		
			id = myid;
			if (item != null)
				SetItem(item);

		}
		
		// -----------------------------------------------------------------------------------
		// Update
		// -----------------------------------------------------------------------------------
		void Update() {
			if (slotType == SlotTypes.Inventory) {
				SetItem(Obj.GetPlayer.inventory.items[id]);
			} else if (slotType == SlotTypes.Equipment) {
				SetItem(Obj.GetPlayer.inventory.equipment[id].item);
			}
		}
		
		// -----------------------------------------------------------------------------------
		// 
		// -----------------------------------------------------------------------------------
		public void SetItem(Item item) {
			ItemInSlot = item;
			if (item.isValid) {
				icon.sprite = item.template.icon;
				if (item.durability <= 0)
					icon.color = Color.red;
			} else {
				icon.sprite = defaultIcon;
				icon.color = Color.white;
			}
		}
		
		// -----------------------------------------------------------------------------------
		// 
		// -----------------------------------------------------------------------------------
		public void OnPointerClick(PointerEventData eventData) {
			if (eventData.clickCount == 2 && onDblClick != null) {
				onDblClick(this);
			}
		}
		
		// -----------------------------------------------------------------------------------
		// 
		// -----------------------------------------------------------------------------------
		public void OnPointerEnter(PointerEventData eventData) {
			if (onHover != null) {
				onHover(this, true);
			}
		}
		
		// -----------------------------------------------------------------------------------
		// 
		// -----------------------------------------------------------------------------------
		public void OnPointerExit(PointerEventData eventData) {
			if (onHover != null) {
				onHover(this, false);
			}
		}
		
		// -----------------------------------------------------------------------------------
		// 
		// -----------------------------------------------------------------------------------
		public void OnBeginDrag(PointerEventData eventData) {
			if (onDrag != null) {
				onDrag(this, true);
			}
		}
		
		// -----------------------------------------------------------------------------------
		// 
		// -----------------------------------------------------------------------------------
		public void OnEndDrag(PointerEventData eventData) {
			if (onDrag != null) {
				onDrag(this, false);
			}
		}
		
		// -----------------------------------------------------------------------------------
		// 
		// -----------------------------------------------------------------------------------
		public void OnDrag(PointerEventData eventData) {}
		
		// -----------------------------------------------------------------------------------
		
	}

}
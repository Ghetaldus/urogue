// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace woco_urogue {

	// ===================================================================================
	// INVENTORY SCREEN
	// ===================================================================================
	public class InventoryScreen : Panel {

		public Transform contentInventory;
		public Transform contentEquipment;
		public GameObject inventorySlotPrefab;
		public GameObject equipmentSlotPrefab;
		public GameObject trashSlotPrefab;

		public TooltipPanel tooltipPanel;
		public Image dragIcon;
		public Color defaultTextColor = Color.grey;
		public Color highlightTextColor = Color.green;

		private Player player;
		private ItemSlot hoveredSlot;
		private EquipmentSlotTypes[] equipmentSlots;
	
		// -----------------------------------------------------------------------------------
		// Start
		// -----------------------------------------------------------------------------------
		void Start() {
			equipmentSlots = Obj.GetGame.configuration.equipmentSlots;
			tooltipPanel.Hide();
		}
	
		// -----------------------------------------------------------------------------------
		// Update
		// -----------------------------------------------------------------------------------
		void Update() {
	
			if (IsShown == false) return;
		
			Vector2 localPoint;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(rootPanel.GetComponent<RectTransform>(), Input.mousePosition, null, out localPoint);
			if (dragIcon.gameObject.activeSelf == true) {
				dragIcon.transform.parent.GetComponent<RectTransform>().anchoredPosition = localPoint;
			}
			  
		}
	
		// -----------------------------------------------------------------------------------
		// Show
		// -----------------------------------------------------------------------------------
		public override void Show() {
	
			player = Obj.GetPlayer;
		
			base.Show();
			tooltipPanel.Hide();
			dragIcon.gameObject.SetActive(false);
			Initalize();
		}

		// -----------------------------------------------------------------------------------
		// Initalize
		// -----------------------------------------------------------------------------------
		public void Initalize() {
			UpdateInventory();
			UpdateEquipment();
			hoveredSlot = null;
		}

		// -----------------------------------------------------------------------------------
		// UpdateInventory
		// -----------------------------------------------------------------------------------
		private void UpdateInventory() {

			// -- add the inventory item slots
			foreach (Transform child in contentInventory.transform)
						GameObject.Destroy(child.gameObject);
					
			for (int i = 0; i < player.inventory.items.Count; ++i) {
		
				var uiObject = Instantiate(inventorySlotPrefab);
				uiObject.transform.SetParent(contentInventory.transform, false);
		
				var uiSlot = uiObject.GetComponent<ItemSlot>();
			
				uiSlot.id 			= i;
				uiSlot.slotType 	= SlotTypes.Inventory;
				uiSlot.SetItem(player.inventory.items[i]);
			
				uiSlot.onDblClick 	+= OnSlotDblClick;
				uiSlot.onHover 		+= OnSlotHover;
				uiSlot.onDrag 		+= OnSlotDrag;
			
				uiObject.SetActive(true);

			}
		
			// -- add the trash slot
			var uiObjectTrash = Instantiate(trashSlotPrefab);
			uiObjectTrash.transform.SetParent(contentInventory.transform, false);
			var uiSlotTrash = uiObjectTrash.GetComponent<ItemSlot>();
			uiSlotTrash.Initalize(player.inventory.items.Count);
		
			uiSlotTrash.onDblClick 	+= OnSlotDblClick;
			uiSlotTrash.onHover 	+= OnSlotHover;
			uiSlotTrash.onDrag 		+= OnSlotDrag;
		
		}
	
		// -----------------------------------------------------------------------------------
		// UpdateEquipment
		// -----------------------------------------------------------------------------------
		private void UpdateEquipment() {
		
			foreach (Transform child in contentEquipment.transform)
						GameObject.Destroy(child.gameObject);
				
			foreach (Equipment equipment in player.inventory.equipment) {
			
				var uiObject = Instantiate(equipmentSlotPrefab);
				uiObject.transform.SetParent(contentEquipment.transform, false);
		
				var uiSlot = uiObject.GetComponent<EquipmentSlot>();
			
				uiSlot.id 			= (int)equipment.slot;
				uiSlot.slotType 	= SlotTypes.Equipment;
				uiSlot.defaultIcon 	= equipmentSlots[(int)equipment.slot].defaultIcon;
				uiSlot.equipType 	= equipmentSlots[(int)equipment.slot].slot;
				uiSlot.SetItem(equipment.item);
			
				uiSlot.onDblClick 	+= OnSlotDblClick;
				uiSlot.onHover 		+= OnSlotHover;
				uiSlot.onDrag 		+= OnSlotDrag;
			
				uiObject.SetActive(true);
						
			}
		}
	
		// -----------------------------------------------------------------------------------
		// OnSlotDblClick
		// -----------------------------------------------------------------------------------
		private void OnSlotDblClick(ItemSlot slot) {
			if (slot.ItemInSlot.isValid) {
				if (slot.slotType == SlotTypes.Equipment) {
					player.inventory.unequipItem(slot.ItemInSlot);
				} else if (slot.slotType == SlotTypes.Inventory) {
					player.inventory.UseItem(slot.ItemInSlot);
				}
			}
		}
	
		// -----------------------------------------------------------------------------------
		// OnSlotHover
		// -----------------------------------------------------------------------------------
		private void OnSlotHover(ItemSlot slot, bool isEntering) {
		
			if (isEntering == true) {
			
				hoveredSlot = slot;
			
				if (hoveredSlot.slotType != SlotTypes.Drop) {
					if (slot.ItemInSlot.isValid) {
						tooltipPanel.Show(slot.ItemInSlot.getTooltip());
					} else {
						tooltipPanel.Hide();
					}
				}
			
			} else {
				tooltipPanel.Hide();
			}
 
		}
	
		// -----------------------------------------------------------------------------------
		// OnSlotDrag
		// -----------------------------------------------------------------------------------
		private void OnSlotDrag(ItemSlot slot, bool isDragging) {
	
			if (isDragging == true) {
			
				// -- Begin/Continue Drag:
				if (slot.ItemInSlot.isValid) {
				   dragIcon.gameObject.SetActive(true);
				   dragIcon.sprite = slot.ItemInSlot.template.icon;
				}
			
			} else {
		
				// -- Stop Drag:

				if (hoveredSlot.slotType != slot.slotType || hoveredSlot.id != slot.id) {
		   
					if (slot.ItemInSlot.isValid) {
						if (hoveredSlot.slotType == SlotTypes.Drop) {						// -- Drop Item
							DropItem(slot);
						} else if (hoveredSlot.slotType == SlotTypes.Equipment) {			// -- Use/Equip Item
						   player.inventory.UseItem(slot.ItemInSlot);
						} else {															// -- Move Item
							player.inventory.MoveItem(slot, hoveredSlot);
						}
						Initalize();
					}
			
				}
			
				dragIcon.gameObject.SetActive(false);
			}
		
		}
	
		// -----------------------------------------------------------------------------------
		// DropItem
		// -----------------------------------------------------------------------------------
		public void DropItem(ItemSlot slot) {

			player.DropItem(slot.ItemInSlot);
		
			if (slot.slotType == SlotTypes.Inventory) {
				player.inventory.items[slot.id].Delete();
			} else if (slot.slotType == SlotTypes.Equipment) {
				player.inventory.equipment[slot.id].item.Delete();
			}
		
		}    

		// -----------------------------------------------------------------------------------
	}

}
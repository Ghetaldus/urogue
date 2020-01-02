// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;

using System.Collections.Generic;
using UnityEngine;

namespace woco_urogue {

	// ===================================================================================
	// INVENTORY CONTROLLER
	// ===================================================================================
	[System.Serializable]
	public class InventoryController : BaseController {
		
		[Header("-=- Inventory -=-")]
		public int inventorySlots = 23;
		
		[HideInInspector] public List<Item> items;
		[HideInInspector] public List<Equipment> equipment;
		[HideInInspector] public event System.Action<Item> onSetWeapon;
		
		// -----------------------------------------------------------------------------------
		// Initalize (Contructor)
		// -----------------------------------------------------------------------------------
		public override void Initalize(Character ownerobj) {
	
			base.Initalize(ownerobj);
			
			// -- Init Inventory
			items = new List<Item>(new Item[inventorySlots]);
			for (int i = 0; i < items.Count; ++i) {
				items[i] = new Item();
			}
			
			// -- Init Equipment
			var count = EquipmentSlots.GetNames(typeof(EquipmentSlots)).Length;
			equipment = new List<Equipment>(new Equipment[count]);
			for (int i = 0; i < equipment.Count; ++i) {
				equipment[i] = new Equipment((EquipmentSlots)i);
			}
			
		}
	
		// -----------------------------------------------------------------------------------
		// Update
		// -----------------------------------------------------------------------------------
		public void Update() {
			
			/*
			if (Obj.GetGame.ingame) {
				if (Obj.GetInput.Item1) UseItem(0);
				if (Obj.GetInput.Item2) UseItem(1);
				if (Obj.GetInput.Item3) UseItem(2);
				if (Obj.GetInput.Item4) UseItem(3);
				if (Obj.GetInput.Item5) UseItem(4);
				if (Obj.GetInput.Item6) UseItem(5);
				if (Obj.GetInput.Item7) UseItem(6);
				if (Obj.GetInput.Item8) UseItem(7);
				if (Obj.GetInput.Item9) UseItem(8);
			}
			*/
			
			// -- Updates equipped items (Torch depletion etc.)
			for (int i = 0; i < equipment.Count; ++i) {
				equipment[i].item.Update();
			}
			
			
		}

		// -----------------------------------------------------------------------------------
		// AddItem
		// -----------------------------------------------------------------------------------
		public bool AddItem(Item item) {	
			int index = getFreeInventoryIndex();
			if (index == -1) return false;
			items[index] = new Item(item.template, owner, item.durability, item.charges, item.level, item.identified);
			return true;
		}

		// -----------------------------------------------------------------------------------
		// DamageEquipment
		// -----------------------------------------------------------------------------------
		public void DamageEquipment(int amount) {
		
			var index = 0;
			amount++;
			amount = Random.Range(0, amount);
		
			for (int i = 0; i <= equipment.Count; ++i) {
				index = Random.Range(1, equipment.Count);
				if (equipment[index].item.isValid) {
					equipment[index].item.doDamage(amount);
					return;
				}
			}
   
		}
			
		// -----------------------------------------------------------------------------------
		// UseItem
		// -----------------------------------------------------------------------------------
		public void UseItem(Item item) {
			
			if (item.canUse() ) {
				if (item.template is ItemEquipableTemplate) {
					var targetSlotId = (int)((ItemEquipableTemplate)item.template).equipmentSlot;
					equipItem(item, targetSlotId);
					//item.template = null;
					
				} else if (item.template is ItemUseableTemplate) {
					item.Use();
					if (item.charges <= 0)
						item.Delete();
				}
			}
		}
	
		// -----------------------------------------------------------------------------------
		// equipItem
		// -----------------------------------------------------------------------------------
		public void equipItem(Item item, int slotId) {
			if (item.isValid && !equipment[slotId].item.isValid) {
				equipment[slotId].item = new Item(item.template, owner, item.durability, item.charges, item.level, item.identified);	
				
				
				//equipment[slotId].item = item;
				equipment[slotId].item.Equip();
			
				if (item.template is ItemWeaponTemplate)
					if (onSetWeapon != null) onSetWeapon(equipment[slotId].item);
			
				item.Delete();
			}
		}

		// -----------------------------------------------------------------------------------
		// unequipItem
		// -----------------------------------------------------------------------------------
		public void unequipItem(Item item) {
			
			if (item.template is ItemWeaponTemplate)
					onSetWeapon(null);
         	
         	item.Unequip();
			
		}
		
		// -----------------------------------------------------------------------------------
		// getFreeInventoryIndex
		// -----------------------------------------------------------------------------------
		public int getFreeInventoryIndex() {
			for (int i = 0; i < items.Count; ++i) {
				if (!items[i].isValid) {
					return i;
				}
			}
			return -1;
		}

		// -----------------------------------------------------------------------------------
		// FindAndDestroyItem
		// -----------------------------------------------------------------------------------
		public bool FindAndDestroyItem(ItemTemplate[] findItems) {
			foreach (ItemTemplate item in findItems) {
				int index = items.FindIndex(i => i.isValid && i.template == item);
				if (index >= 0) {
					return items[index].Delete();
				}
			}
			return false;
		}
	
		// -----------------------------------------------------------------------------------
		// MoveItem
		// -----------------------------------------------------------------------------------
		public void MoveItem(ItemSlot firstSlot, ItemSlot secondSlot) {
			
			if (firstSlot.slotType == SlotTypes.Inventory && secondSlot.slotType == SlotTypes.Inventory) {
			
				Item item 				= items[firstSlot.id];
				items[firstSlot.id] 	= items[secondSlot.id];
				items[secondSlot.id] 	= item;
				
			} else if (firstSlot.slotType == SlotTypes.Equipment && secondSlot.slotType == SlotTypes.Inventory) {
				
				equipment[firstSlot.id].item.Deactivate();
				Item item = equipment[firstSlot.id].item;
				equipment[firstSlot.id].item 	= items[secondSlot.id];
				equipment[firstSlot.id].item.Activate();
				items[secondSlot.id] = item;
				
				if (item.template is ItemWeaponTemplate)
					onSetWeapon(null);
				
			}
			
		}

		// -----------------------------------------------------------------------------------
	
	}

}
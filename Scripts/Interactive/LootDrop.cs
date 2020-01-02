// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;

using UnityEngine;

namespace woco_urogue {

	// ===================================================================================
	// LOOT DROP
	// ===================================================================================
	public class LootDrop : Entity, IInteractive {

		public ItemDrop item;
		public SoundTemplate pickupSound;
	   
		protected Item Item;
	
		// -----------------------------------------------------------------------------------
		// Start
		// Spawns the loot drop into the active scene
		// -----------------------------------------------------------------------------------
		protected override void Start()  {
		
			if (item == null) {
				Destroy(gameObject);
				return;
			}
		
			item.Initalize();
			Item = new Item(item.template, item.durability, item.charges, item.level, item.identified);
		
			if (item.template.icon != null)
				spriteRenderer.sprite = item.template.icon;
		
			base.Start();
		}
	
		// -----------------------------------------------------------------------------------
		// Useable
		// (via IInteractive) Always pickup-able therefore always true
		// -----------------------------------------------------------------------------------
		public bool Useable() {
			return true;
		}

		// -----------------------------------------------------------------------------------
		// Use
		// (via IInteractive)
		// -----------------------------------------------------------------------------------
		public void Use() {
			if (Obj.GetPlayer.inventory.AddItem(Item)) {
				SoundController.Play(pickupSound);
				Destroy(gameObject);
			} else {
				UIController.Instance.ShowError("_inventoryFull");
			}
		}
 
		// -----------------------------------------------------------------------------------
		// getName
		// (via IInteractive)
		// Overrides the name function as we need the item name, not the loot drop name
		// -----------------------------------------------------------------------------------
		public override string getName {
			get {
				if (item != null && item.template.description != null) {
					if (item.identified) {
						return item.template.description.getName;
					} else {
						return LanguageLibrary.GetText("_unidentified");
					}
				}
				if (description != null) {
					return description.getName;
				}
				return "";
			}
		}	
	
		// -----------------------------------------------------------------------------------
		// durability
		// -----------------------------------------------------------------------------------
		public float durability {
			get { return item.durability; }
			set {
				item.durability = value;
			}
		}
	
		// -----------------------------------------------------------------------------------
		// charges
		// -----------------------------------------------------------------------------------
		public float charges {
			get { return item.charges; }
			set {
				item.charges = value;
			}
		}

		// -----------------------------------------------------------------------------------
		// level
		// -----------------------------------------------------------------------------------
		public int level {
			get { return item.level; }
			set {
				item.level = value;
			}
		}
		
		// -----------------------------------------------------------------------------------

	}

}
// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;
using UnityEngine;

namespace woco_urogue {

	// =======================================================================================
	// ITEM
	// =======================================================================================
	public class Item : ObjectBase {
	
		public ItemTemplate template 	= null;
		
		public float durability 		= 0;
		public float charges 			= 0;
		public int level 				= 0;
		
		public bool identified			= true;
		public bool cursed				= false;
		
		protected bool isActive			= false;
		protected string tooltip		= "???";
		
		// -----------------------------------------------------------------------------------
		// Item (Constructor)
		// -----------------------------------------------------------------------------------
		public Item(ItemTemplate tmpl=null, Character ownerobj=null, float durab=1, float chrg=0, int lvl=0, bool ident=true) {
			if (tmpl != null && ownerobj != null) {
				template 	= tmpl;
				base.Init(ownerobj);
				durability 	= durab;
				charges 	= chrg;
				level		= lvl;
				identified	= ident;
				owner.AdjustProperty(template.carryModifiers);
			}
		}
		
		// -----------------------------------------------------------------------------------
		// Item (Constructor)
		// -----------------------------------------------------------------------------------
		public Item(ItemTemplate tmpl=null, float durab=1, float chrg=0, int lvl=0, bool ident=true) {
			if (tmpl != null) {
				template 	= tmpl;
				durability 	= durab;
				charges 	= chrg;
				level		= lvl;
				identified	= ident;
			}
		}
		
		// -----------------------------------------------------------------------------------
		// Item (Constructor)
		// -----------------------------------------------------------------------------------
		public Item() {
			template = null;
			owner = null;
		}
		
		// -----------------------------------------------------------------------------------
		// Update
		// Only equipable/equipped Items are updated every frame
		// -----------------------------------------------------------------------------------
		public void Update() {
			if (isValid && template is ItemEquipableTemplate && charges > 0) {
				charges -= template.depleteUpdate(updateInterval);
				if (charges < 0)
					charges = 0;
			}
		}
		
		// -----------------------------------------------------------------------------------
		// Delete
		// -----------------------------------------------------------------------------------
		public bool Delete() {
			owner.AdjustProperty(template.carryModifiers, 0, -1);
			template = null;
			owner = null;
			return true;
		}
		
		// -----------------------------------------------------------------------------------
		// isValid
		// -----------------------------------------------------------------------------------
		public bool isValid {
			get { return (template != null); }
		}
		
		// -----------------------------------------------------------------------------------
		// canUse
		// -----------------------------------------------------------------------------------
		public bool canUse() {
			if (isValid &&
				durability > 0 &&
				template.canUse(owner, charges, durability)
				) {
					return true;
			}
			return false;
		}
		
		// -----------------------------------------------------------------------------------
		// depleteUsage
		// -----------------------------------------------------------------------------------
		public void depleteUsage() {
			charges -= template.usageCharges;
			durability -= template.usageDurability;
			template.depleteUsage(owner);
			checkDurablity();
		}
		
		// -----------------------------------------------------------------------------------
		// doDamage
		// -----------------------------------------------------------------------------------
		public void doDamage(int amount) {
			if (!isValid) return;
			durability -= amount;
			checkDurablity();
		}
		
		// -----------------------------------------------------------------------------------
		// checkDurablity
		// -----------------------------------------------------------------------------------
		public void checkDurablity() {
			if (durability > 0) return;
			durability = 0;
			Deactivate();
			UIController.Instance.ShowError("_equipmentBroken");
			
			/* fhiz: todo:
				needs better feedback message and a sound
				- should auto-unequip if it is a weapon, or even drop the weapon
			*/
			
		}
		
		// -----------------------------------------------------------------------------------
		// Equip
		// -----------------------------------------------------------------------------------
		public virtual void Equip() {
			Activate();
		}
		
		// -----------------------------------------------------------------------------------
		// Unequip
		// -----------------------------------------------------------------------------------
		public virtual void Unequip() {
			Deactivate();
			Obj.GetPlayer.inventory.AddItem(this);
			Delete();
		}

		// -----------------------------------------------------------------------------------
		// Activate
		// -----------------------------------------------------------------------------------
		public virtual void Activate() {
			if (isValid && !isActive) {
				template.Activate(owner, level);
				SoundController.Play(template.activateSound);
				isActive = true;
			}
		}

		// -----------------------------------------------------------------------------------
		// Deactivate
		// -----------------------------------------------------------------------------------
		public virtual void Deactivate() {
			if (isValid && isActive) {
				template.Deactivate(owner, level);
				SoundController.Play(template.deactivateSound);
				isActive = false;
			}
		}
		
		// -----------------------------------------------------------------------------------
		// Use
		// -----------------------------------------------------------------------------------
		public virtual void Use() {
			if (canUse()) {
				template.Use(level);
				depleteUsage();
			}
		}
			
		// -----------------------------------------------------------------------------------
		// getTooltip
		// -----------------------------------------------------------------------------------
		public string getTooltip() {
			
			if (isValid && identified) {

				tooltip = 	"<b>" + template.description.getName;							// Name
				
				if (level > 0) {															// Level
					tooltip += " +"+level;
				} else if (level < 0) {
					tooltip += " "+level;
				}
				
				tooltip += 	"</b>\n";
				
				tooltip += 	"<i>" + template.description.getDescription + "</i>\n";			// Description
					
				if (template is ItemEquipableTemplate)										// Durability
					tooltip += LanguageLibrary.GetText("_durability") + ": "+durability + "/" + template.maxDurability + "\n";
			
				if (template is ItemUseableTemplate || template is ItemEquipableTemplate && template.maxCharges > 0)	// Charges
					tooltip += LanguageLibrary.GetText("_charges") + ": "+charges + "/" + template.maxCharges + "\n";

			}
		
			return tooltip;

		}
			
		// -----------------------------------------------------------------------------------
		
	}	
	
	// =======================================================================================
	
}
// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;
using UnityEngine;

namespace woco_urogue {

	// =======================================================================================
	// ITEM USEABLE TEMPLATE
	// =======================================================================================
	public abstract class ItemUseableTemplate : ItemTemplate {
		
		[Header("-=- Usage Modifiers -=-")]
		public int _usageDurability;
     	public int _usageCharges;
     	public PropertyModifier[] usageModifiers;
 		
 		// -----------------------------------------------------------------------------------
		// usageDurability
		// -----------------------------------------------------------------------------------
 		public override int usageDurability {
 			get { return _usageDurability; }
 			set { _usageDurability = value; }
 		}
 		
 		// -----------------------------------------------------------------------------------
		// usageCharges
		// -----------------------------------------------------------------------------------
 		public override int usageCharges {
 			get { return _usageCharges; }
 			set { _usageCharges = value; }
 		}

 		// -----------------------------------------------------------------------------------
		// canUse
		// -----------------------------------------------------------------------------------
 		public override bool canUse(Character owner, float charges, float durability) {
			if (
				charges >= _usageCharges &&
				durability >= _usageDurability &&
				owner.CheckProperty(usageModifiers)
				) {
					return true;
			}
			return false;
		}
		
 		// -----------------------------------------------------------------------------------
		// depleteUsage
		// -----------------------------------------------------------------------------------
		public override void depleteUsage(Character owner) {
			
			if (owner != null)
				owner.AdjustProperty(usageModifiers, -1);	
		}
		
		// -----------------------------------------------------------------------------------
		
	}
	
	// =======================================================================================

}
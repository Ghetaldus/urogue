// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================


using woco_urogue;

using System;
using System.Collections.Generic;
using UnityEngine;

namespace woco_urogue {

	// =======================================================================================
	// ITEM EQUIPMENT TEMPLATE
	// =======================================================================================
	[CreateAssetMenu(fileName="ItemEquipment", menuName="uRogue Item/New Item (Equipment)", order=999)]
	public class ItemEquipmentTemplate : ItemEquipableTemplate {
		
		[Header("-=- Equipment Options -=-")]
		public PropertyModifier[] OnActivateModifier;

		// -----------------------------------------------------------------------------------
		// Activate
		// -----------------------------------------------------------------------------------
		public override void Activate(Character owner, int level=0) {
			if (owner != null) {
				owner.AdjustProperty(OnActivateModifier, level, 1);
			}
		}
		
		// -----------------------------------------------------------------------------------
		// Deactivate
		// -----------------------------------------------------------------------------------
		public override void Deactivate(Character owner, int level=0) {
			if (owner != null) {
				owner.AdjustProperty(OnActivateModifier, level, -1);
			}
		}
		
		// -----------------------------------------------------------------------------------
    
	}

}
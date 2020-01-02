// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;
using UnityEngine;

namespace woco_urogue {

	// =======================================================================================
	// ITEM EQUIPABLE TEMPLATE
	// =======================================================================================
	public abstract class ItemEquipableTemplate : ItemTemplate {
	
		[Header("-=- Equip Options -=-")]
		public EquipmentSlots equipmentSlot;
		public int _updateCharges;
		
		protected float updateTime;

		// -----------------------------------------------------------------------------------
		// updateCharges
		// -----------------------------------------------------------------------------------
 		public override int updateCharges {
 			get { return _updateCharges; }
 			set { _updateCharges = value; }
 		}
		
		// -----------------------------------------------------------------------------------
		// depleteUpdate
		// -----------------------------------------------------------------------------------		
		public override int depleteUpdate(float updateInterval) {
			if (updateTime <= 0 || (Time.time - updateTime > updateInterval)) {
				updateTime = Time.time;
				return _updateCharges;
			}
			return 0;
		}
		
		// -----------------------------------------------------------------------------------	
		
	}
	
	// =======================================================================================

}
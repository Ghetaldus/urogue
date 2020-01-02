// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;

using UnityEngine;

namespace woco_urogue {

	// ===================================================================================
	// BASE CONTROLLER
	// ===================================================================================
	[System.Serializable]
	public class BaseController {

		protected Character owner;
	
		// -------------------------------------------------------------------------------
		// Initalize (Constructor)
		// -------------------------------------------------------------------------------
		public virtual void Initalize(Character ownerobj=null) {
			owner = ownerobj;     
		}
	
		// -------------------------------------------------------------------------------

	}

}
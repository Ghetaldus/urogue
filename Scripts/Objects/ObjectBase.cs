// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;
using UnityEngine;

namespace woco_urogue {

	// =======================================================================================
	// BASE OBJECT
	// =======================================================================================
	public class ObjectBase {
	
		protected Character owner;
		protected float updateInterval;
		
		// -----------------------------------------------------------------------------------
		// Init
		// -----------------------------------------------------------------------------------
		public virtual void Init(Character ownerobj=null) {
			owner = ownerobj;
			updateInterval = Obj.GetGame.configuration.updateTime;
		}

		// -----------------------------------------------------------------------------------
		
	}
		
	// =======================================================================================
	
}
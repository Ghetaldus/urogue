// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace woco_urogue {

	// =======================================================================================
	// STATUS TEMPLATE
	// =======================================================================================
	public abstract class StatusTemplate : PropertyTemplate {
		
		public SoundTemplate applySound;
		public SoundTemplate removeSound;
   
		public float duration;
		public bool isPermanent;
		public bool invisibleStatus;
		
		public bool ignoreAccuracy;
		public bool ignoreResistance;
		
		public PropertyModifier[] OnActivateModifier;

	}

}

// =======================================================================================
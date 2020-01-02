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
	// LANGUAGE TEMPLATE (UNIQUE)
	// =======================================================================================
	[CreateAssetMenu(fileName="Language", menuName="New Language", order=998)]
	public class LanguageTemplate : BaseTemplate {
				
		[Header("-=- Common Names -=-")]
		public string _charges;
		public string _durability;
		public string _level;
		public string _unidentified;
		
		[Header("-=- Common Messages -=-")]
		public string _equipmentBroken;
		public string _inventoryFull;
		
		
		// -----------------------------------------------------------------------------------
    
	}

}

// =======================================================================================
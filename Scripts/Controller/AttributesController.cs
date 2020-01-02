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
	// ATTRIBUTES CONTROLLER
	// ===================================================================================
	[System.Serializable]
	public class AttributesController : BaseController {

		[Header("-=- Attributes -=-")]
		public List<Attribute> attributes;
			   
		// -----------------------------------------------------------------------------------
		// Initalize (Constructor)
		// -----------------------------------------------------------------------------------
		public override void Initalize(Character ownerobj=null) {
		
			base.Initalize(ownerobj);
		
			foreach (Attribute stat in attributes) {
				stat.Init(ownerobj);
			}

		}
	
		// -----------------------------------------------------------------------------------

	}

}
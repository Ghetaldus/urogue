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
	// RESISTANCES CONTROLLER
	// ===================================================================================
	[System.Serializable]
	public class ResistancesController : BaseController {
		
		[Header("-=- Resistances -=-")]
		public List<Element> resistances;
			   
		// -----------------------------------------------------------------------------------
		// Initalize (Constructor)
		// -----------------------------------------------------------------------------------
		public override void Initalize(Character ownerobj=null) {
		
			base.Initalize(ownerobj);
		
			foreach (Element stat in resistances) {
				stat.Init(ownerobj);
			}

		}

		// -----------------------------------------------------------------------------------

	}

}
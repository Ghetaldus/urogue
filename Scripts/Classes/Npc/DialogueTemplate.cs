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
	// DIALOGUE TEMPLATE
	// =======================================================================================
	[CreateAssetMenu(fileName="Dialogue", menuName="uRogue Npc/New Dialogue", order=999)]
	public class DialogueTemplate : BaseTemplate {
		
		[Header("-=- Dialogue Options -=-")]
		public DescriptionTemplate description;
		public Sprite portrait;
		
		// -----------------------------------------------------------------------------------
    
	}

}

// =======================================================================================
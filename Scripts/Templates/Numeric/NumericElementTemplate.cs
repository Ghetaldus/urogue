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
	// NUMERIC ELEMENT TEMPLATE 
	// =======================================================================================
	[CreateAssetMenu(fileName="NumericElement", menuName="uRogue Numeric/New NumericElement", order=999)]
	public class NumericElementTemplate : PropertyTemplate {
	
		[Header("-=- Boost Properties -=-")]		
		public PropertyModifier[] BaseIncreaseProperties;
		
		// -----------------------------------------------------------------------------------
    
	}

}

// =======================================================================================
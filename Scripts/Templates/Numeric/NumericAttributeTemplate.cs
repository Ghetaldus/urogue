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
	// NUMERIC ATTRIBUTE TEMPLATE 
	// =======================================================================================
	[CreateAssetMenu(fileName="NumericAttribute", menuName="uRogue Numeric/New NumericAttribute", order=999)]
	public class NumericAttributeTemplate : PropertyTemplate {
	
		public NumericStatisticTemplate increaseStatistic;
		public int maxValue;
	
		// -----------------------------------------------------------------------------------
    
	}

}

// =======================================================================================
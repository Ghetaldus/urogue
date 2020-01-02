// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;
using UnityEngine;

namespace woco_urogue {

	// =======================================================================================
	// PROPERTY MODIFIER
	// =======================================================================================
	[System.Serializable]
	public class PropertyModifier {

		public PropertyTemplate template = null;
		public float FixedValue;
		[Range(-1,1)]public float PercentValue;
		public PropertyTypes propertyType = PropertyTypes.Value;

	}	
	
	// =======================================================================================
	
}
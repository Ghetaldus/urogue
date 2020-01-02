// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;
using UnityEngine;

namespace woco_urogue {

	// ===================================================================================
	// ELEMENT
	// ===================================================================================
	[System.Serializable]
	public class Element : PropertyBase {
	
		public NumericElementTemplate template;
		
		// -----------------------------------------------------------------------------------
		// Value
		// -----------------------------------------------------------------------------------
		public override float Value {
			get {
				if (owner != null)
					BaseValue = owner.CalculateProperties(template.BaseIncreaseProperties, PropertyTypes.TotalValue);
				return base.Value;
			}
        	set {
            	base.Value = value;
            }
		}		

		// -------------------------------------------------------------------------------
		
	}
	
	// ===================================================================================
	
}

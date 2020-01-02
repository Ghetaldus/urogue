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
	// DESCRIPTION TEMPLATE
	// =======================================================================================
	[CreateAssetMenu(fileName="Description", menuName="New Description", order=998)]
	public class DescriptionTemplate : BaseTemplate {
		
		public ObjectDescription[] description;
		
		// -----------------------------------------------------------------------------------
		// getName
		// retrieves the actual name of the object by the current language
		// -----------------------------------------------------------------------------------
		public virtual string getName {
			get {
				if (description != null) {
					for (int i = 0; i < description.Length; i++) {
						if (description[i].language == Obj.GetGame.language) {
							return description[i].name;
						}
					}
				}
				return "[missing]";
			}
		}

		// -----------------------------------------------------------------------------------
		// getDescription
		// retrieves the tooltip description of the object by the current language
		// -----------------------------------------------------------------------------------
		public virtual string getDescription {
			get {
				if (description != null) {
					for (int i = 0; i < description.Length; i++) {
						if (description[i].language == Obj.GetGame.language) {
							return description[i].description;
						}
					}
				}
				return "";
			}
		}	
		
		// -----------------------------------------------------------------------------------
    
	}

}

// =======================================================================================
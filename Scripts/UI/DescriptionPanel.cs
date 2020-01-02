// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;

using UnityEngine;
using UnityEngine.UI;

namespace woco_urogue {

	// ===================================================================================
	// DESCRIPTION PANEL
	// ===================================================================================
	public class DescriptionPanel : Panel {
	
		public DescriptionTemplate template;
		public Text text;

		// -----------------------------------------------------------------------------------
		// Update
		// -----------------------------------------------------------------------------------
		void Update() {
		
			if (IsShown == false) return;
	
			if (template != null) {
				text.text 		= DescriptionLibrary.GetName(template.name);
			} else {
				Debug.LogWarning("You forgot to assign inspector properties to: " +this.name);
			}
	   
		}
	
		// -----------------------------------------------------------------------------------
	
	}

}
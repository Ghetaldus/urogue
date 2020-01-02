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
	// SKILL SCREEN
	// ===================================================================================
	public class SkillScreen : Panel {
		
		public Transform content;
		public GameObject slotPrefab;
		
		// -----------------------------------------------------------------------------------
		// Show
		// -----------------------------------------------------------------------------------
		public override void Show() {
			base.Show();
			Initalize();
		}

		// -----------------------------------------------------------------------------------
		// Initalize
		// -----------------------------------------------------------------------------------
		public void Initalize() {
	
			Player player = Obj.GetPlayer;
			
			foreach (Skill skill in player.skills.skills) {
				
				if (skill.isValid && skill.template.showGUI) {
			
					var uiObject = Instantiate(slotPrefab);
					uiObject.transform.SetParent(content.transform, false);
				
					var uiSlot = uiObject.GetComponent<SkillSlot>();
					uiSlot.template = skill.template;
					uiSlot.Initalize();
					
					uiObject.SetActive(true);
				
				}
			}

		}

		// -----------------------------------------------------------------------------------
		
	}

}
// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;

using UnityEngine;

namespace woco_urogue {

	// ===================================================================================
	// PANEL
	// ===================================================================================
	public class Panel : MonoBehaviour {

		public GameObject rootPanel;
		public Panel[] childPanels;
	
		public bool IsShown { get; private set; }
		
		// -------------------------------------------------------------------------------
		// Start
		// -------------------------------------------------------------------------------
		public virtual void Show() {
			rootPanel.SetActive(true);
			IsShown = true;
		}
		
		// -------------------------------------------------------------------------------
		// Start
		// -------------------------------------------------------------------------------
		public virtual void Hide() {
			rootPanel.SetActive(false);
			IsShown = false;
		}
		
		// -------------------------------------------------------------------------------
		// Start
		// -------------------------------------------------------------------------------
		public void SetState(bool show) {
		
			foreach (Panel go in childPanels) {
				if (show) {
					go.Show();
				} else {
					go.Hide();
				}
			}
	
			if (show) Show();
			else Hide();
		}
	
		// -------------------------------------------------------------------------------
		
	}

}
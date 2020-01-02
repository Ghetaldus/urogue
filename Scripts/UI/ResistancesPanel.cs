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
// RESISTANCES PANEL
// ===================================================================================
public class ResistancesPanel : Panel {
    
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
		
		foreach (Transform child in content.transform)
			GameObject.Destroy(child.gameObject);
		
		foreach (Element element in player.resistances.resistances) {
			
			if (element.template.showGUI) {
		
				var uiObject = Instantiate(slotPrefab);
				uiObject.transform.SetParent(content.transform, false);
			
				var uiSlot = uiObject.GetComponent<ResistanceSlot>();
				uiSlot.template = element.template;
				uiSlot.Initalize();
				
				uiObject.SetActive(true);
			
			}
		}

	}

	// -----------------------------------------------------------------------------------
		
}

}
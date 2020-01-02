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
// ATTRIBUTES PANEL
// ===================================================================================
public class AttributesPanel : Panel {
    
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
		
		foreach (Attribute attribute in player.attributes.attributes) {
			
			if (attribute.template.showGUI) {
		
				var uiObject = Instantiate(slotPrefab);
				uiObject.transform.SetParent(content.transform, false);
			
				var uiSlot = uiObject.GetComponent<AttributeSlot>();
				uiSlot.template = attribute.template;
				uiSlot.Show();
				
				uiObject.SetActive(true);
			
			}
		}

	}

	// -----------------------------------------------------------------------------------
		
}

}
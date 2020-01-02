// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace woco_urogue {

// ===================================================================================
// BUFF SLOT
// ===================================================================================
public class BuffSlot : MonoBehaviour {

    public Image icon;
    public Text numText;
    public int id = 0;
   
    public Status StatusInSlot { get; private set; }
    
	// -------------------------------------------------------------------------------
	// 
	// -------------------------------------------------------------------------------
    public void SetStatus(Status status) {
        StatusInSlot = status;
        if (StatusInSlot.isValid && !StatusInSlot.template.invisibleStatus) {
            icon.gameObject.SetActive(true);
            icon.sprite = StatusInSlot.template.icon;
            
            if (StatusInSlot.template.isPermanent) {
            	numText.text = "";
            } else {
            	numText.text = StatusInSlot.getRemainingTimeInSeconds().ToString();
            }
            
        } else {
        	icon.gameObject.SetActive(false);
        }
    }
    
	// -------------------------------------------------------------------------------
	// 
	// -------------------------------------------------------------------------------
	void Update() {
		if (StatusInSlot.isValid &&
			!StatusInSlot.template.invisibleStatus &&
			!StatusInSlot.template.isPermanent) {
            	numText.text = StatusInSlot.getRemainingTimeInSeconds().ToString();
		}
	}


}

}
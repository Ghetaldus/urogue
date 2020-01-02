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
	// TOOLTIP PANEL
	// ===================================================================================
	public class TooltipPanel : Panel {

		public GameObject tooltipPanel;
		public Text tooltipText;
		protected bool isVisible;
			
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
		void Update() {
			
        	if (isVisible) {
        		Vector2 localPoint;
        		RectTransformUtility.ScreenPointToLocalPointInRectangle(rootPanel.GetComponent<RectTransform>(), Input.mousePosition, null, out localPoint);
        	    tooltipPanel.transform.GetComponent<RectTransform>().anchoredPosition = localPoint;
        	}
		}
	
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
		public void Show(string newText) {
			tooltipText.text = newText;
			tooltipPanel.gameObject.SetActive(true);
			isVisible = true;
		}
		
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
		public override void Hide() {
			isVisible = false;
			tooltipText.text = "";
			tooltipPanel.gameObject.SetActive(false);
		}
		
		// -------------------------------------------------------------------------------
		
	}

}
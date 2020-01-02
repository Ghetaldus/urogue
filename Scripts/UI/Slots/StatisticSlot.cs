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
// STATISTIC SLOT
// ===================================================================================

public class StatisticSlot : Panel {
	
	public NumericStatisticTemplate template;
	public Slider slider;
	public Image fill;
    public Text text;
    public GameObject childGameObject;
    public bool showMax;
    public bool autoHide;
    
    protected int index = -1;
    protected Player player;
     
	// -----------------------------------------------------------------------------------
	// Awake
	// -----------------------------------------------------------------------------------
    public void Awake() {
    
    	if (template != null) {
    	
    		player 		= Obj.GetPlayer;
			index 		= player.statistics.statistics.FindIndex(s => s.template == template);
    		fill.color 	= template.color;
    		
    	} else {
        	Debug.LogWarning("You forgot to assign inspector properties to: " +this.name);
        }
    }
    
	// -----------------------------------------------------------------------------------
	// Update
	// -----------------------------------------------------------------------------------
    void Update() {
    
    	if (template != null) {

			if (IsShown == false) return;
		
			if (index != -1) {
			
				if (autoHide && !player.statistics.statistics[index].isActive) {
					childGameObject.SetActive(false);
					
				} else if (autoHide && player.statistics.statistics[index].isActive) {
					childGameObject.SetActive(true);
				}
			
				string stat_name 	= DescriptionLibrary.GetName(template.description.name);
				float valueNow 		= player.statistics.statistics[index].Value;
				float valueMax 		= player.statistics.statistics[index].MaxValue;
				float valuePercent 	= player.statistics.statistics[index].PercentValue;
			
				slider.value 	= valuePercent;
				
				if (showMax) {
					text.text 	= stat_name	+ ": " 	+ valueNow 	+ "/" + valueMax;
				} else {
					text.text 	= stat_name	+ ": " 	+ valueNow;
				}
				
			} else {
        		Debug.LogWarning("Requested Statistic was not found in: " +this.name);
        	}

        } else {
        	Debug.LogWarning("You forgot to assign inspector properties to: " +this.name);
        }
       
    }
    
    // -----------------------------------------------------------------------------------
    
}

}
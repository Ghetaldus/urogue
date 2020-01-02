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
// SKILL SLOT
// ===================================================================================

public class SkillSlot : MonoBehaviour {
	
	public NumericAttributeTemplate template;
	public Slider slider;
    public Image fill;
    public Text text;
    public Text value;
    public Button button;
    public Image image;
    
    public bool showMod;
        
    protected int index_stat = -1;
    protected int index_increase = -1;
    protected Player player;
    
	// -----------------------------------------------------------------------------------
	// Initalize
	// -----------------------------------------------------------------------------------
    public void Initalize() {
    
    	if (template != null) {
    		
    		player = Obj.GetPlayer;
    		index_stat 		= player.skills.skills.FindIndex(s => s.template == template);
    		index_increase 	= player.statistics.statistics.FindIndex(s => s.template == template.increaseStatistic);
    		
    		if (image != null)
    			image.sprite	= template.icon;
    		
    		if (fill != null)
    			fill.color = template.color;
    		
    	} else {
        	Debug.LogWarning("You forgot to assign inspector properties to: " +this.name);
        }
    }
    
	// -----------------------------------------------------------------------------------
	// Update
	// -----------------------------------------------------------------------------------
    void Update() {
    
    	if (template != null) {
    	
			float valueNow = -1;
			float valueMod = -1;
			float valueMax = -1;
			float valuePercent = 0;
			
			if (button != null)			
				button.interactable = false;

			if (index_stat != -1) {
			
				string stat_name 	= template.description.getName;
				valueNow 			= player.skills.skills[index_stat].Value;
				valueMod 			= player.skills.skills[index_stat].BonusValue;
				valueMax 			= player.skills.skills[index_stat].MaxValue;
				valuePercent 		= player.skills.skills[index_stat].PercentValue;
				
				if (button != null && index_increase != -1)
					button.interactable = player.statistics.statistics[index_increase].Value > 0 && valueNow < player.skills.skills[index_stat].MaxValue;
				
				text.text 		= stat_name;
				value.text = valueNow.ToString();

				if (showMod)
					value.text += " + " + valueMod.ToString();
				
				if (slider != null)
					slider.value 	= valuePercent;
				
				if (value != null)
        			value.text 		= valueNow.ToString() + " + " + valueMod.ToString();
			
			} else {
        		Debug.LogWarning("Requested Statistic was not found in: " +this.name);
        	}
			
        } else {
        	Debug.LogWarning("You forgot to assign inspector properties to: " +this.name);
        }
       
    }
    
	// -----------------------------------------------------------------------------------
	// OnClickIncrease
	// -----------------------------------------------------------------------------------
    public void OnClickIncrease() {
    	if (template != null) {
        	player.skills.skills[index_stat].Value++;										// fhiz: todo: increase by a certain amount
        	player.statistics.statistics[index_increase].Value--;							// fhiz: todo: decrease by a certain amount
        }
    }
    
    // -----------------------------------------------------------------------------------
    
}

}
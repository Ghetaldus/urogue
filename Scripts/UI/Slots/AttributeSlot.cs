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
// ATTRIBUTES SLOT
// ===================================================================================

public class AttributeSlot : Panel {
	
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
	// Show
	// -----------------------------------------------------------------------------------
    public override void Show() {
    
    	if (template != null) {
    		
    		player = Obj.GetPlayer;
    		index_stat 		= player.attributes.attributes.FindIndex(a => a.template == template);
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
			
				string stat_name 	= DescriptionLibrary.GetName(template.description.name);
				valueNow 		= player.attributes.attributes[index_stat].Value;
				valueMod 		= player.attributes.attributes[index_stat].BonusValue;
				valueMax 		= player.attributes.attributes[index_stat].MaxValue;
				valuePercent 	= player.attributes.attributes[index_stat].PercentValue;
				
				if (button != null)
					button.interactable = player.statistics.statistics[index_increase].Value > 0 && valueNow < player.attributes.attributes[index_stat].MaxValue;
				
				if (text != null && value == null) {
					text.text 		= stat_name + ": " + valueNow.ToString();
					if (showMod && valueMod > 0)
						text.text += " + " + valueMod.ToString();
				} else {
					text.text 		= stat_name;
				}
				
				if (slider != null)
					slider.value 	= valuePercent;
				
				if (value != null) {
        			value.text 		= valueNow.ToString();
        			
        			if (showMod && valueMod > 0)
        				value.text += " + " + valueMod.ToString();
			
				}
			
			} else {
        		Debug.LogWarning("Requested Attribute was not found in: " +this.name);
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
        	player.attributes.attributes[index_stat].Value++;			// fhiz: todo: increase by a certain amount
        	player.statistics.statistics[index_increase].Value--;		// fhiz: todo: decrease by a certain amount
        }
    }
    
    // -----------------------------------------------------------------------------------
    
}

}
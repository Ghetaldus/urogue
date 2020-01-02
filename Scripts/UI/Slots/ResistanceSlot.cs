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
	// RESISTANCE SLOT
	// ===================================================================================
	public class ResistanceSlot : MonoBehaviour {
	
		public NumericElementTemplate template;
	
		public Text text;
		public Text value;
		public Image image;
		
		protected int index = -1;
		protected Player player;
	
		// -----------------------------------------------------------------------------------
		// Initalize
		// -----------------------------------------------------------------------------------
		public void Initalize() {
	
			if (template != null) {
			
				player 		= Obj.GetPlayer;
				index 		= player.resistances.resistances.FindIndex(e => e.template == template);
			
				if (image != null)
					image.sprite	= template.icon;

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
			
				if (index != -1) {
			
					string stat_name 	= DescriptionLibrary.GetName(template.description.name);
					valueNow 			= player.resistances.resistances[index].Value;
					valueMod 			= player.resistances.resistances[index].BonusValue;
				
				
					text.text 		= stat_name;
					
					value.text 		= valueNow.ToString();
					
					if (valueMod > 0)
						value.text += " + " + valueMod.ToString();

				} else {
					Debug.LogWarning("Requested Resistance was not found in: " +this.name);
				}
			
			} else {
				Debug.LogWarning("You forgot to assign inspector properties to: " +this.name);
			}
	   
		}

		// -----------------------------------------------------------------------------------
	
	}

}
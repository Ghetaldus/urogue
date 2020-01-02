// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;
using UnityEngine;

namespace woco_urogue {

	// =======================================================================================
	// SKILL
	// =======================================================================================
	public class Skill : PropertyBase {
	
		public SkillTemplate template = null;
		
		// -----------------------------------------------------------------------------------
		// Skill (Constructor)
		// -----------------------------------------------------------------------------------
		public Skill(SkillTemplate tmpl=null, Character ownerobj=null, int lvl=0) {
			if (tmpl != null) {
				template = tmpl;
				base.Init(ownerobj);
				
				
				
				defaultValue = lvl;
				Value = defaultValue;
			}
		}
		
		// -----------------------------------------------------------------------------------
		// isValid
		// -----------------------------------------------------------------------------------
		public override bool isValid {
			get { return (template != null && Value > 0); }
		}

		// -----------------------------------------------------------------------------------
		// Activate
		// -----------------------------------------------------------------------------------
		protected void Activate() {

			//SoundController.Play(template.applySound);
			
		}

	   // -----------------------------------------------------------------------------------
		// Deactivate
		// -----------------------------------------------------------------------------------
		public virtual void Deactivate() {
	
			//SoundController.Play(template.removeSound);
			
			
		}
	
		// -----------------------------------------------------------------------------------
		
		
	}	
	
	// =======================================================================================
	
}
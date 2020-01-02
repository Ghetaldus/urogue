// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;
using UnityEngine;

namespace woco_urogue {

	// =======================================================================================
	// STATUS
	// =======================================================================================
	public class Status : PropertyBase {
	
		public StatusTemplate template = null;
		
		public float remainingDuration = 0;
		
		// -----------------------------------------------------------------------------------
		// Status
		// -----------------------------------------------------------------------------------
		public Status(StatusTemplate tmpl=null, Character ownerobj=null) {
			remainingDuration = 0;
			if (tmpl != null) {
				template = tmpl;
				owner = ownerobj;
				Activate();
			}
		}
		
		// -----------------------------------------------------------------------------------
		// isValid
		// -----------------------------------------------------------------------------------
		public override bool isValid {
			get { return (template != null && (remainingDuration > Time.time || template.isPermanent) ); }
		}
		
		// -----------------------------------------------------------------------------------
		// getRemainingTimeInSeconds
		// -----------------------------------------------------------------------------------
		public int getRemainingTimeInSeconds() {
			return Mathf.RoundToInt(remainingDuration - Time.time);
		}

		// -----------------------------------------------------------------------------------
		// Activate
		// -----------------------------------------------------------------------------------
		protected void Activate() {

			owner.AdjustProperty(template.OnActivateModifier, 1);

			remainingDuration = Time.time + template.duration;
			SoundController.Play(template.applySound);
			
		}

	   // -----------------------------------------------------------------------------------
		// Deactivate
		// -----------------------------------------------------------------------------------
		public virtual void Deactivate() {
	
			SoundController.Play(template.removeSound);
						
			owner.AdjustProperty(template.OnActivateModifier, -1);
			
			owner = null;
			template = null;
			remainingDuration = 0;
			
		}
	
		// -----------------------------------------------------------------------------------
		
		
	}	
	
	// =======================================================================================
	
}
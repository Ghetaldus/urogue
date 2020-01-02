// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;

using UnityEngine;
using UnityEngine.Events;

namespace woco_urogue {

	// ===================================================================================
	// ENTITY BASE
	// ===================================================================================
	public abstract class EntityBase : MonoBehaviour {
	
		[Header("-=- Base Entity Options -=-")]
		public DescriptionTemplate description;
		
		protected bool IsActive;
		
		// -----------------------------------------------------------------------------------
		// getName
		// -----------------------------------------------------------------------------------
		public virtual string getName {
			get {
				if (description != null) {
					return description.getName;
				}
				return "[missing]";
			}
		}	
		
		// -----------------------------------------------------------------------------------
		// getDescription
		// -----------------------------------------------------------------------------------
		public virtual string getDescription {
			get {
				if (description != null) {
					return description.getDescription;
				}
				return "[missing]";
			}
		}

		// -----------------------------------------------------------------------------------
		
	}
		
}
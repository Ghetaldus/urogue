// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;

using UnityEngine;

namespace woco_urogue {

	// ===================================================================================
	// FLOOR PIT
	// ===================================================================================
	[RequireComponent(typeof(BoxCollider))]
	public class FloorPit : EntityBase, IInteractive {

		[Header("-=-  Options -=-")]
		
		
		public SoundTemplate activateSound;
		public Transform targetTransform;
		
		
		
		// -------------------------------------------------------------------------------
		// Useable
		// -------------------------------------------------------------------------------
		public bool Useable() {
			return true;
		}

		// -------------------------------------------------------------------------------
		// Use
		// -------------------------------------------------------------------------------
		public void Use() {
			
			if (targetTransform != null) {
			
			Player player = Obj.GetPlayer;
			
			
			
			player.transform.position = targetTransform.position;
			player.transform.rotation = targetTransform.rotation;
			
			}
			
		}		
		
		// -------------------------------------------------------------------------------

	}

}
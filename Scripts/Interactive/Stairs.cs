// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace woco_urogue {

	// ===================================================================================
	// CONTAINER
	// ===================================================================================
	[HelpURL("http://example.com/docs/MyComponent.html")]
	public class Stairs : Entity, IInteractive {
	
		public SoundTemplate activateSound;
		public string targetSceneName;
		[Range(1,999)]public int targetWayPointId;
		
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
			climbStairs();
		}
	
		// -------------------------------------------------------------------------------
		// climbStairs
		// -------------------------------------------------------------------------------
		public void climbStairs() {
			if (!string.IsNullOrEmpty(targetSceneName) && targetWayPointId > 0) {
				SoundController.Play(activateSound, transform.position);
				
				Obj.GetPlayer.Warp(targetSceneName, targetWayPointId);
				
				
			}
			

		}
	
		// -------------------------------------------------------------------------------
		
	}

}
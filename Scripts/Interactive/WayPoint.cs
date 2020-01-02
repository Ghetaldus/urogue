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
	// WAY POINT
	// ===================================================================================
	[HelpURL("http://example.com/docs/MyComponent.html")]
	public class WayPoint : MonoBehaviour {
	
		public SoundTemplate activateSound;
		[Range(1,999)]public int id;
		
		// -------------------------------------------------------------------------------
		// Start
		// -------------------------------------------------------------------------------
		public void Start() {
			Player player = Obj.GetPlayer;
			if (player.nextWayPoint == id) {
				SoundController.Play(activateSound, transform.position);
				player.transform.position = transform.position;
				player.transform.rotation = transform.rotation;
				player.nextWayPoint = 0;
			}
		}
	
		// -------------------------------------------------------------------------------
		
	}

}
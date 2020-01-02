// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;

using UnityEngine;

namespace woco_urogue {

	// ===================================================================================
	// MUSIC OBJECT
	// ===================================================================================
	public class MusicArea : MonoBehaviour {

		[Tooltip("The audio clip that is played while the player is inside this area")]
		public AudioClip musicClip;
		[Tooltip("The duration it takes to fade the music in/out when entering/leaving the area")]
		public float fadeInFadeOut = 2.0f;
		[Tooltip("Set to true if you want the music to loop while the player stays inside the area")]
		public bool loop = true;
	
		[Range(0,1)]public float volume = 1.0f;
		[Tooltip("Set to true to not switch music when the player attacks or is attacked")]
		public bool priority = false;

		// -------------------------------------------------------------------------------
		// OnTriggerEnter
		// -------------------------------------------------------------------------------
		void OnTriggerEnter(Collider co) {
			if (musicClip != null) {
				var player = co.GetComponentInParent<Player>();
				if (player != null) {
					if (checkBGMTime(player.hitTime)) {
						MusicController.PlayBGM(musicClip, volume, fadeInFadeOut, loop);
					}
				}
			}
		}
	
		// -------------------------------------------------------------------------------
		// OnTriggerExit
		// -------------------------------------------------------------------------------
		void OnTriggerExit(Collider co) {
			if (musicClip != null) {
				var player = co.GetComponentInParent<Player>();
				if (player != null) {			
					if (checkBGMTime(player.hitTime)) {
						MusicController.PlayGameStateBGM(GameStates.Game);
					}
				}
			}
		}
	
		// -------------------------------------------------------------------------------
		// checkBGMTime
		// -------------------------------------------------------------------------------
		private bool checkBGMTime(float time) {
			return (time <= 0 || (Time.time - time > 30) || priority);
	
		}
	
		// -------------------------------------------------------------------------------

	
	}

}
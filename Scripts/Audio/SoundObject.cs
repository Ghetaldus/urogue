// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;

using UnityEngine;

namespace woco_urogue {

	// ===================================================================================
	// SOUND OBJECT
	// ===================================================================================
	public class SoundObject : MonoBehaviour {
	
		private AudioSource source;

		// -------------------------------------------------------------------------------
		// SetClip
		// -------------------------------------------------------------------------------
		public void SetClip(SoundTemplate sound) {
			if (source == null) source = GetComponent<AudioSource>();
			source.clip = sound.clip;
			source.volume = sound.volume * SoundController.GetVolume();
			source.spatialBlend = sound.is2d ? 0 : 1;
			source.Play();
			Destroy(this.gameObject, sound.clip.length + 0.5f);
		}
		
		// -------------------------------------------------------------------------------
	
	}

}
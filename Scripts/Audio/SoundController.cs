// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;
using UnityEngine;

namespace woco_urogue {

	// ===================================================================================
	// SOUND CONTROLLER
	// ===================================================================================
	public class SoundController : MonoBehaviour {
			
		protected float MaxVolume = 1f;
		
		protected static SoundController _instance;
		protected static bool isMuted = false;
		
		protected float CurrentVolumeNormalized = 1f;
				
		// -------------------------------------------------------------------------------
		// MusicController (Protected Singleton Constructor)
		// -------------------------------------------------------------------------------
		protected SoundController() { }

		// -------------------------------------------------------------------------------
		// GetInstance (Singleton)
		// -------------------------------------------------------------------------------
		public static SoundController GetInstance() {
			if (!_instance) {
				GameObject instance = new GameObject("SoundController");
        		_instance = instance.AddComponent<SoundController>();
        		
        		_instance.MaxVolume = Obj.GetGame.configuration.MaxSFXVolume;
        		
			}
			return _instance;
		}

		// ===============================================================================
		// GENERAL FUNCTIONS
		// ===============================================================================
		
		// -------------------------------------------------------------------------------
		// SetVolume
		// -------------------------------------------------------------------------------		
		public static float Volume {
			get { 
				SoundController instance = GetInstance();
				return instance.CurrentVolumeNormalized;
			}
			set { 
				SoundController instance = GetInstance();
				instance.CurrentVolumeNormalized = value;
			}
		}
		
		// -------------------------------------------------------------------------------
		// GetVolume
		// -------------------------------------------------------------------------------
		public static float GetVolume() {
			SoundController instance = GetInstance();
			return isMuted ? 0f : instance.MaxVolume * instance.CurrentVolumeNormalized;
		}
		
		// -------------------------------------------------------------------------------
		// Play
		// -------------------------------------------------------------------------------
		public static SoundObject Play(SoundTemplate tmpl) {
			SoundObject sound = PlaySound(tmpl);
			return sound;
		}
		
		// -------------------------------------------------------------------------------
		// Play
		// -------------------------------------------------------------------------------
		public static SoundObject Play(SoundTemplate tmpl, Vector3 pos) {
			SoundObject sound = PlaySound(tmpl);
			if (sound == null) return null;

			sound.transform.position = pos;
			return sound;
		}
		
		// -------------------------------------------------------------------------------
		// Play
		// -------------------------------------------------------------------------------
		public static SoundObject Play(SoundTemplate tmpl, Transform tran) {
			SoundObject sound = PlaySound(tmpl);
			if (sound == null) return null;

			sound.transform.parent = tran;
			sound.transform.localPosition = Vector3.zero;
			return sound;
		}
		
		// -------------------------------------------------------------------------------
		// PlaySound
		// -------------------------------------------------------------------------------
		private static SoundObject PlaySound(SoundTemplate tmpl) {
			if (tmpl == null) return null;
			SoundObject sound = ((GameObject) GameObject.Instantiate(tmpl.soundObjectPrefab)).GetComponent<SoundObject>();
			sound.SetClip(tmpl);
			return sound;
		}
	}

}

// =======================================================================================
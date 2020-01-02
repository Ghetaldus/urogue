// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace woco_urogue {

	// ===================================================================================
	// MUSIC CONTROLLER (SINGLETON)
	// ===================================================================================
	public class MusicController : MonoBehaviour {

		protected float MaxVolume = 1f;
		protected AudioClip titleMusicClip;
		protected float titleFadeInFadeOut = 1.0f;
		protected AudioClip menuMusicClip;
		protected float menuFadeInFadeOut = 1.0f;
		protected AudioClip gameOverMusicClip;
		protected float gameOverFadeInFadeOut = 1.0f;
		protected AudioClip defaultMusicClip;
		protected float defaultFadeInFadeOut = 1.0f;
		protected AudioClip combatMusicClip;
	
		protected static MusicController _instance;
		protected static bool isMuted = false;
		
		protected float CurrentVolumeNormalized = 1f;
		
		protected MusicState[] musicList;
		
		List<AudioSource> bgmSources;
		
		// -------------------------------------------------------------------------------
		// MusicController (Protected Singleton Constructor)
		// -------------------------------------------------------------------------------
		protected MusicController() { }

		// -------------------------------------------------------------------------------
		// GetInstance (Singleton)
		// -------------------------------------------------------------------------------
		public static MusicController GetInstance() {
			if (!_instance) {
				GameObject instance = new GameObject("MusicController");
        		_instance = instance.AddComponent<MusicController>();
        		_instance.MaxVolume = Obj.GetGame.configuration.MaxBGMVolume;
        		_instance.musicList = Obj.GetGame.configuration.musicList;
			}
			return _instance;
		}
	
		// ===============================================================================
		// GENERAL FUNCTIONS
		// ===============================================================================
		
		// -------------------------------------------------------------------------------
		// PlayGameStateBGM
		// -------------------------------------------------------------------------------		
		public static void PlayGameStateBGM(GameStates gameState) {
		
			MusicController instance = GetInstance();
			
			foreach (MusicState musicState in instance.musicList) {
				if (musicState.gameState == gameState) {
					if (musicState.musicClip != null) {
						StopAllBGM(0);
						PlayBGM(musicState.musicClip, musicState.volume, musicState.fadeInFadeOut, true);
					} else {
						StopAllBGM(1);
					}
				}
			}

		}

		// -------------------------------------------------------------------------------
		// SetVolume
		// -------------------------------------------------------------------------------		
		public static float Volume {
			get { 
				MusicController instance = GetInstance();
				return instance.CurrentVolumeNormalized;
			}
			set { 
				MusicController instance = GetInstance();
				instance.CurrentVolumeNormalized = value;
				AdjustSoundImmediate();
			}
		}
		
		// -------------------------------------------------------------------------------
		// GetVolume
		// -------------------------------------------------------------------------------
		public static float GetVolume(float individualVolume=1) {
			MusicController instance = GetInstance();
			return isMuted ? 0f : instance.MaxVolume * instance.CurrentVolumeNormalized * individualVolume;
		}
		
		// -------------------------------------------------------------------------------
		// DisableSoundImmediate
		// -------------------------------------------------------------------------------
		public static void DisableSoundImmediate() {
			MusicController instance = GetInstance();
			instance.StopAllCoroutines();
			if (instance.bgmSources != null) {
				foreach (AudioSource source in instance.bgmSources) {
					source.volume = 0;
				}
			}
			isMuted = true;
		}
		
		// -------------------------------------------------------------------------------
		// EnableSoundImmediate
		// -------------------------------------------------------------------------------
		public static void EnableSoundImmediate() {
			MusicController instance = GetInstance();
			if (instance.bgmSources != null) {
				foreach (AudioSource source in instance.bgmSources) {
					source.volume = GetVolume();
				}
			}
			isMuted = false;
		 }
		 
		// -------------------------------------------------------------------------------
		// SetGlobalVolume
		// -------------------------------------------------------------------------------
		public static void SetGlobalVolume(float newVolume) {
			MusicController instance = GetInstance();
			instance.CurrentVolumeNormalized = newVolume;
			AdjustSoundImmediate();
		}
		 
		// -------------------------------------------------------------------------------
		// SetBGMVolume
		// -------------------------------------------------------------------------------
		public static void SetBGMVolume(float newVolume) {
			MusicController instance = GetInstance();
			instance.CurrentVolumeNormalized = newVolume;
			AdjustSoundImmediate();
		}
		 
		// -------------------------------------------------------------------------------
		// AdjustSoundImmediate
		// -------------------------------------------------------------------------------
		public static void AdjustSoundImmediate() {
			 MusicController instance = GetInstance();
			 if (instance.bgmSources != null) {
				 foreach (AudioSource source in instance.bgmSources) {
					 source.volume = GetVolume();
				 }
			 }
		}

		// ===============================================================================
		// BGM FUNCTIONS
		// ===============================================================================
		
		// -------------------------------------------------------------------------------
		// GetBGMSource
		// -------------------------------------------------------------------------------
		protected AudioSource GetBGMSource() {
			MusicController instance = GetInstance();
			AudioSource BGMSource = gameObject.AddComponent<AudioSource>();
			BGMSource.playOnAwake = false;
			BGMSource.volume = GetVolume();
			if (instance.bgmSources == null) {
				instance.bgmSources = new List<AudioSource>();
			}
			instance.bgmSources.Add(BGMSource);
			return BGMSource;
		}
		
		// -------------------------------------------------------------------------------
		// RemoveBGMSource
		// -------------------------------------------------------------------------------
		protected IEnumerator RemoveBGMSource(AudioSource BGMSource, float delay = 0) {
			MusicController instance = GetInstance();
			delay = (delay <= 0) ? BGMSource.clip.length : delay;
			yield return new WaitForSeconds(delay);
			instance.bgmSources.Remove(BGMSource);
			Destroy(BGMSource);
		}

		// -------------------------------------------------------------------------------
		// PlayBGM
		// -------------------------------------------------------------------------------
		public static void PlayBGM(AudioClip bgmClip, float volume, float fadeDuration, bool loop) {
			MusicController instance = GetInstance();
			
			//AudioSource curBgm = instance.getCurrentBGMPlaying();
			AudioSource source = instance.GetBGMSource();
			
			// -- fade-out current BGM
			foreach (AudioSource csource in instance.bgmSources) {
				if (csource.isPlaying) {
					if (fadeDuration > 0) {
						instance.FadeBGMOut(csource, fadeDuration/2);
						instance.StartCoroutine(instance.RemoveBGMSource(csource, fadeDuration/2));
					} else {
						instance.FadeBGMOut(csource, 0);
						instance.StartCoroutine(instance.RemoveBGMSource(csource));
					}
				}
			}
			
			// -- start new BGM
			if (bgmClip != null) {
			
			  	source.volume = GetVolume(volume);
				source.clip = bgmClip;
				source.loop = loop;
				source.Play();
				
				// -- adjust new BGM (either fade-in or not)
				if (fadeDuration > 0) {
					instance.FadeBGMIn(source, fadeDuration / 2, fadeDuration / 2, volume);
				} else {
					float delay = 0f;
					instance.FadeBGMIn(source, delay, fadeDuration, volume);
				}
				
				if (!loop) {
					instance.StartCoroutine(instance.RemoveBGMSource(source));
				}
				
			}

		}
		
		// -------------------------------------------------------------------------------
		// StopBGM
		// -------------------------------------------------------------------------------
		public static void StopBGM(AudioClip bgmClip, float fadeDuration) {
			MusicController instance = GetInstance();
			if (instance.bgmSources != null) {
				foreach (AudioSource source in instance.bgmSources) {
					if (source.clip == bgmClip && source.isPlaying) {
						instance.FadeBGMOut(source, fadeDuration);
						instance.StartCoroutine(instance.RemoveBGMSource(source, fadeDuration));
					}
				}
			}
		}
		
		
		
		// -------------------------------------------------------------------------------
		// FadeBGMOut
		// -------------------------------------------------------------------------------
		public void FadeBGMOut(AudioSource source, float fadeDuration) {
			MusicController instance = GetInstance();
			float delay = 0f;
			float toVolume = 0f;
			instance.StartCoroutine(instance.FadeBGM(source, toVolume, delay, fadeDuration));
		}

		// -------------------------------------------------------------------------------
		// FadeBGMIn
		// -------------------------------------------------------------------------------
		public void FadeBGMIn(AudioSource source, float delay, float fadeDuration, float volume) {
			MusicController instance = GetInstance();
			float toVolume = GetVolume(volume);
			instance.StartCoroutine(FadeBGM(source, toVolume, delay, fadeDuration));
		}

		// -------------------------------------------------------------------------------
		// checkBGMPlaying
		// -------------------------------------------------------------------------------
		private bool checkBGMPlaying(AudioSource source) {
			MusicController instance = GetInstance();
			foreach (AudioSource bgmSource in instance.bgmSources) {
				if (bgmSource == source && bgmSource.isPlaying) {
					return true;
				}
			}
			return false;
		}

		// -------------------------------------------------------------------------------
		// FadeBGM
		// -------------------------------------------------------------------------------
		public IEnumerator FadeBGM(AudioSource source, float fadeToVolume, float delay, float duration) {

			yield return new WaitForSeconds(delay);
			
			if (source != null) {
				if (duration > 0) {
			
					float volumeDifference = fadeToVolume - source.volume;
					bool pass = false;
				
					while (!pass) {
						if (source)
							source.volume += volumeDifference * Time.deltaTime / duration;
						else
							break;

						if (volumeDifference > 0) {
							pass = source.volume >= fadeToVolume ? true : false;
						} else if (volumeDifference < 0) {
							pass = source.volume <= fadeToVolume ? true : false;
						} else {
							yield return new WaitForSeconds(duration);
							break;
						}
						yield return null;
					}
			
				} else {
					source.volume = fadeToVolume;
				}
			}
			
		}

		// -------------------------------------------------------------------------------
		// StopAllBGM
		// -------------------------------------------------------------------------------
		public static void StopAllBGM(float fadeDuration) {
			MusicController instance = GetInstance();
			if (instance.bgmSources != null) {
				foreach (AudioSource source in instance.bgmSources) {
					if (source != null && source.isPlaying) {
						instance.FadeBGMOut(source, fadeDuration);
					}
				}
			}	
		}

		// -------------------------------------------------------------------------------
		
		}
	
	// ===================================================================================
}

// =======================================================================================
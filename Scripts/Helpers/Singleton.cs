// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;

using UnityEngine;

namespace woco_urogue {

	// ===================================================================================
	// SINGLETON
	// ===================================================================================
	public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {

		protected static T instance;

		public static T Instance {
			get {
				if (instance == null || instance.gameObject == null) {
					instance = (T)FindObjectOfType(typeof(T));
					if (instance == null) {
					   Debug.LogWarning("An instance of " + typeof(T) + " is needed in the scene, but there is none.");
					}
				}
				return instance;
			}
		}
	}

}
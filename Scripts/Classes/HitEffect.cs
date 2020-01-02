// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;

using UnityEngine;

namespace woco_urogue {

	// ===================================================================================
	// HIT EFFECT
	// ===================================================================================
	public class HitEffect : MonoBehaviour, IHitable {

		public GameObject hitEffectObject;
		public SoundTemplate hitSound;

		// -------------------------------------------------------------------------------
		// Hit
		// -------------------------------------------------------------------------------
		public void Hit(HitInfo hitInfo) {
			if (hitInfo.isShowEffect) {
				SoundController.Play(hitSound, transform.position);
				if (hitEffectObject != null) {
					GameObject go = Instantiate(hitEffectObject, hitInfo.point, Quaternion.identity) as GameObject;
					Destroy(go, 3);
				}
			}
		}
	
	}

}
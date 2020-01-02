// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;

using System.Collections.Generic;
using UnityEngine;

namespace woco_urogue {

	// =======================================================================================
	// DESTROYABLE
	// =======================================================================================
	public class Destroyable : MonoBehaviour, IHitable {

		public int health;
		public List<Element> defence;
		public GameObject corpse;
		public GameObject hitEffectObject;
		public SoundTemplate hitSound;
	
		// -----------------------------------------------------------------------------------
		// Hit
		// -----------------------------------------------------------------------------------
		public void Hit(HitInfo hitInfo) {
			if (health<=0) return;
			
			Character source = null;
			if (hitInfo.source is Player)
				source = Obj.GetPlayer;
				
			DamageResult damageResult = Utl.DamageFormula(hitInfo.damage, defence, source);

			health -= damageResult.amount;
			SoundController.Play(hitSound, transform.position);

			if (hitInfo.isShowEffect && damageResult.amount > 0) {
				if (hitEffectObject != null) {
					GameObject hitEffect = Instantiate(hitEffectObject, hitInfo.point, Quaternion.identity) as GameObject;
					Destroy(hitEffect, 3);
				}
			}

			if (health <= 0) Death();
		}
	
		// -----------------------------------------------------------------------------------
		// Death
		// -----------------------------------------------------------------------------------
		protected virtual void Death() {
			if (corpse != null)
			{
				Instantiate(corpse,transform.position,transform.rotation);
			}
			Destroy(gameObject);
		}
		// -----------------------------------------------------------------------------------
	}

}
// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace woco_urogue {

	// =======================================================================================
	// EFFECT TEMPLATE
	// =======================================================================================
	[CreateAssetMenu(fileName="Effect", menuName="uRogue Effects/New Effect", order=999)]
	public class EffectTemplate : BaseTemplate {

		public GameObject dropPrefab;
		public ItemDropProbability[] createItem;
		public StatusChance[] addStatus;
		public StatusChance[] removeStatus;
		public PropertyModifier[] OnActiveModifier;
		public DamageTemplate dealDamage;
		
		// -----------------------------------------------------------------------------------
		// Activate
		// -----------------------------------------------------------------------------------
		public virtual void Activate(Character owner, int level=0) {

			owner.AdjustProperty(OnActiveModifier, level);
			
			foreach (StatusChance tmpl in addStatus) {
				owner.states.TryApplyStatus(tmpl.template, tmpl.probability);
			}
			
			foreach (StatusChance tmpl in removeStatus) {
				owner.states.TryRemoveStatus(tmpl.template, tmpl.probability);
			}
			
			foreach (ItemDropProbability tmpl in createItem) {
				Utl.SpawnItem(tmpl, tmpl.spawnProbability, owner.transform.position, dropPrefab);
			}
		
			if (dealDamage != null) {
				IHitable hitable = owner.gameObject.GetComponent(typeof(IHitable)) as IHitable;
   		     	if (hitable != null) {
   		         	hitable.Hit(new HitInfo(dealDamage, owner.transform.position, false, null, level));
	        	}
        	}
			
		}

		// -----------------------------------------------------------------------------------
    
	}

}

// =======================================================================================
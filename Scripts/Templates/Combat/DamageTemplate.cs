// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;

using System.Collections.Generic;
using UnityEngine;

namespace woco_urogue {

	// ===================================================================================
	// DAMAGE TEMPLATE
	// ===================================================================================
	[CreateAssetMenu(fileName="ItemWeaponMelee", menuName="uRogue Combat/New Damage", order=999)]
	public class DamageTemplate : BaseTemplate {
		
		public bool triggerCombat;
		
		[Header("-=- Attack Elements -=-")]
		public List<Element> elements;
		
		[Header("-=- Attacker Modifiers -=-")]
		public PropertyModifier[] attackModifiers;
		
		[Header("-=- Damage Options -=-")]
		[Range(0,1)] public float damageVariance = 0.2f;
		[Range(0,1)] public float equipmentDamage = 0.1f;
		
		[Header("-=- Critical Options -=-")]
		[Range(0,1)] public float criticalProbability = 0.2f;
		public PropertyModifier[] probabilityModifiers;
		[Range(1,10)] public float criticalPower = 3.0f;
		public PropertyModifier[] powerModifiers;
		
		[Header("-=- Status Options -=-")]
		public StatusChance[] applyStatus;
		public StatusChance[] applyStatusOnCrit;
		
		// -------------------------------------------------------------------------------
		// DamageTemplate (Constructor)
		// -------------------------------------------------------------------------------
		public void Initalize() {
		
			foreach (Element ele in elements) {
				ele.Init();
			}
		
		}
		
		// -------------------------------------------------------------------------------
		
	}
	
	// ===================================================================================
	
}
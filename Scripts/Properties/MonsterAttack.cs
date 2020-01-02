// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;
using UnityEngine;

namespace woco_urogue {

	// =======================================================================================
	// MONSTER ATTACK
	// =======================================================================================
	public class MonsterAttack : PropertyBase {
		public MonsterAttackTemplate template;
		public float currentCooldown;
		
		public MonsterAttack(MonsterAttackTemplate tmpl) {
			template = tmpl;
			currentCooldown = 0;	
		}
		
	}	
	
	// =======================================================================================
	
}
// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;
using UnityEngine;

namespace woco_urogue {

	// ===================================================================================
	// MONSTER ATTACK RANGED TEMPLATE
	// ===================================================================================
	[CreateAssetMenu(fileName="MonsterAttack Ranged", menuName="uRogue MonsterAttack/New MonsterAttack (Ranged)", order=999)]
	public class MonsterAttackRangedTemplate : MonsterAttackTemplate {
		
		public GameObject projectile;
		
		// -----------------------------------------------------------------------------------
		// attackDistance
		// -----------------------------------------------------------------------------------
		public override float attackDistance {
			get {
				if (projectile != null) {
					return projectile.GetComponent<Projectile>().attackDistance;
				} else {
					return 0;
				}
			}
		}
		// -----------------------------------------------------------------------------------

	}
	
	// ===================================================================================
	
}
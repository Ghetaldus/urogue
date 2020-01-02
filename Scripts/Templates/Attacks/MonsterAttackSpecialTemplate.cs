// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;
using UnityEngine;

namespace woco_urogue {

	// =======================================================================================
	// MONSTER ATTACK SPECIAL TEMPLATE 
	// =======================================================================================
	[CreateAssetMenu(fileName="MonsterAttack Special", menuName="uRogue MonsterAttack/New MonsterAttack (Special)", order=999)]
	public class MonsterAttackSpecialTemplate : MonsterAttackTemplate {
		
		/* fhiz: todo:
			future feature 
			special attacks have no target but apply buffs/heal to the monster itself
		*/

	}
	
}
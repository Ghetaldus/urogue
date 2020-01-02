// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;
using UnityEngine;

namespace woco_urogue {

	// =======================================================================================
	// MONSTER ATTACK AREA TEMPLATE 
	// =======================================================================================
	[CreateAssetMenu(fileName="MonsterAttack Area", menuName="uRogue MonsterAttack/New MonsterAttack (Area)", order=999)]
	public class MonsterAttackAreaTemplate : MonsterAttackTemplate {
		
		/* fhiz: todo:
			future feature 
			area attacks have no target but apply damage/buffs within radius
		*/

	}
	
}
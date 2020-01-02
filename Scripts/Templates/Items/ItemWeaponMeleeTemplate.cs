// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;
using UnityEngine;

namespace woco_urogue {

	// =======================================================================================
	// ITEM WEAPON MELEE TEMPLATE 
	// =======================================================================================
	[CreateAssetMenu(fileName="ItemWeaponMelee", menuName="uRogue Item/New Item (Weapon Melee)", order=999)]
	public class ItemWeaponMeleeTemplate : ItemWeaponTemplate {

		[Header("-=- Melee Weapon Options -=-")]
		public int animationId;
		public DamageTemplate damage;
		
	}

}
// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;

using UnityEngine;


namespace woco_urogue {

	// =======================================================================================
	// ITEM WEAPON RANGED TEMPLATE
	// =======================================================================================
	[CreateAssetMenu(fileName="ItemWeaponRanged", menuName="uRogue Item/New Item (Weapon Ranged)", order=999)]
	public class ItemWeaponRangedTemplate : ItemWeaponTemplate {
		
		[Header("-=- Ranged Weapon Options -=-")]
    	public GameObject projectile;

	}

}
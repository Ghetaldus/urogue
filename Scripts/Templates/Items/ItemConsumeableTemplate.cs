// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;

using UnityEngine;

namespace woco_urogue {

	// =======================================================================================
	// ITEM CONSUMEABLE TEMPLATE
	// =======================================================================================
	[CreateAssetMenu(fileName="ItemConsumeable", menuName="uRogue Item/New Item (Consumeable)", order=999)]
	public class ItemConsumeableTemplate : ItemUseableTemplate {
	
	[Header("-=- Usage Effect -=-")]
    public EffectTemplate useEffect;
    
	// -----------------------------------------------------------------------------------
	// Use
	// -----------------------------------------------------------------------------------
    public override void Use(int level=0) {
		useEffect.Activate(Obj.GetPlayer, level);
    }

   	// -----------------------------------------------------------------------------------

}

}
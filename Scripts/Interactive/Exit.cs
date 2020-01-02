// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;

using UnityEngine;

namespace woco_urogue {

public class Exit : EntityBase, IInteractive
{


    void Start()
    {
        IsActive = true;
    }
    
	// -------------------------------------------------------------------------------
	// Useable
	// -------------------------------------------------------------------------------
	public bool Useable() {
		return IsActive;
	}
    
    public void Use()
    {
        Obj.GetGame.GameState = GameStates.Finish;
        IsActive = false;
    }
}

}
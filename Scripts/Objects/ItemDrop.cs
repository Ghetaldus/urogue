// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;
using UnityEngine;

namespace woco_urogue {

	// ===================================================================================
	// ITEM DROP PROBABILITY
	// ===================================================================================
	[System.Serializable]
	public class ItemDropProbability : ItemDrop {
	
		[Range(0,1)] public float spawnProbability;
		
	}

	// ===================================================================================
	// ITEM DROP 
	// ===================================================================================
	[System.Serializable]
	public class ItemDrop {
	
		public ItemTemplate template;
		
	    [Range(0,1)] public float defaultDurability = 1.0f;
   	 	[Range(0,1)] public float defaultCharges	= 1.0f;
		[Range(0,1)] public float defaultLevel		= 0.0f;
		[Range(0,1)] public float defaultIdentified	= 1.0f;
		
		[HideInInspector] public float charges;
		[HideInInspector] public float durability;
		[HideInInspector] public int level;
		[HideInInspector] public bool identified	= false;
		
		// -------------------------------------------------------------------------------
		// Initalize
		// -------------------------------------------------------------------------------
		public void Initalize() {
			charges 	= Mathf.Round(template.maxCharges * defaultCharges);
			durability 	= Mathf.Round(template.maxDurability * defaultDurability);
			level 		= Mathf.RoundToInt(template.maxLevel * defaultLevel);
			
			if (Random.value <= defaultIdentified)
				identified = true;
			
		}
	
		// -------------------------------------------------------------------------------
	
	}
	
}
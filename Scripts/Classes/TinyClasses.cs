// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;
using UnityEngine;

namespace woco_urogue {

	// =======================================================================================
	// MUSIC STATE
	// =======================================================================================
	[System.Serializable]
	public class MusicState {
		public GameStates gameState;
		public AudioClip musicClip;
		[Range(0,1)] public float volume = 1.0f;
		public float fadeInFadeOut = 1.0f;
	}

	// =======================================================================================
	// STATUS CHANCE 
	// =======================================================================================
	[System.Serializable]
	public class StatusChance {
		public StatusTemplate template;
		[Range(0,1)] public float probability;
		public bool removeStatus;
	}
	
	// =======================================================================================
	// SKILL LEVEL
	// =======================================================================================
	[System.Serializable]
	public class SkillLevel {
		public SkillTemplate template;
		[Range(0,999)] public int level;
	}	
	
	// =======================================================================================
	// OBJECT DESCRIPTION
	// =======================================================================================
	[System.Serializable]
	public class ObjectDescription {
		public LanguageTemplate language;
		public string name;
		[TextArea(5,10)]public string description;
	}
	
	// =======================================================================================
	// EQUIPMENT
	// =======================================================================================
	[System.Serializable]
	public class Equipment {
		public EquipmentSlots slot;
		[HideInInspector]public Item item;
		
		public Equipment(EquipmentSlots slotId) {
			slot = slotId;
			item = new Item();
		}
		
	}
	
	// =======================================================================================
	// EQUIPMENT SLOT
	// =======================================================================================
	[System.Serializable]
	public class EquipmentSlotTypes {
		public EquipmentSlots slot;
		public string equipmentSlot;
		public Sprite defaultIcon;
	}
	
	// =======================================================================================
	// DAMAGE RESULT
	// =======================================================================================
	public class DamageResult {
		public int amount;
		public DamageTypes type;
	}

	// =======================================================================================
	
}
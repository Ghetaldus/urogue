// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace woco_urogue {

	// =======================================================================================
	// CONFIGURATION TEMPLATE (UNIQUE) 
	// =======================================================================================
	[CreateAssetMenu(fileName="Configuration", menuName="New Configuration", order=998)]
	public class ConfigurationTemplate : BaseTemplate {
		
		
		public float updateTime = 2f;
		
		[Header("-=- Language -=-")]
		public LanguageTemplate defaultLanguage;
		
		
		
		
		[Header("-=- Equipment Slots -=-")]
		public EquipmentSlotTypes[] equipmentSlots;
		
		[Header("-=- Music & Sound -=-")]
		[Range(0,1)]public float MaxBGMVolume = 1f;
		[Range(0,1)]public float MaxSFXVolume = 1f;
		
		[Header("-=- Music -=-")]
		public MusicState[] musicList;
		
		[Header("-=- Environment -=-")]
		public DamageTemplate fallDamage;
		

		
		// -----------------------------------------------------------------------------------
    
	}

}

// =======================================================================================
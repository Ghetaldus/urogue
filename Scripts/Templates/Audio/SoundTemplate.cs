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
	// SOUND TEMPLATE
	// =======================================================================================
	[CreateAssetMenu(fileName="Sound", menuName="uRogue Audio/New Sound", order=999)]
	public class SoundTemplate : BaseTemplate {
		
		public GameObject soundObjectPrefab;
   		public AudioClip clip;
    	[Range(0,1)]public float volume = 1f;
    	public bool is2d = false;

		// -----------------------------------------------------------------------------------
    
	}

}

// =======================================================================================
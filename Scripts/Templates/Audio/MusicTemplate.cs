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
	// MUSIC TEMPLATE
	// =======================================================================================
	[CreateAssetMenu(fileName="Music", menuName="uRogue Audio/New Music", order=999)]
	public class MusicTemplate : BaseTemplate {
		
		public GameObject musicObjectPrefab;
   		public AudioClip clip;
    	[Range(0,1)]public float volume = 1f;
    	public bool is2d = true;

		// -----------------------------------------------------------------------------------
    
	}

}

// =======================================================================================
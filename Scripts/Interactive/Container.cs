// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;
using UnityEngine;

namespace woco_urogue {

	// ===================================================================================
	// CONTAINER
	// ===================================================================================
	[HelpURL("http://example.com/docs/MyComponent.html")]
	public class Container : Destroyable {
	
		public GameObject dropPrefab;
		public ItemDropProbability[] items;
		
		// -------------------------------------------------------------------------------
		// Death
		// -------------------------------------------------------------------------------
		protected override void Death() {
			Utl.SpawnItems(items, transform.position, dropPrefab);
			base.Death();
		}

		// -------------------------------------------------------------------------------
		
	}

}
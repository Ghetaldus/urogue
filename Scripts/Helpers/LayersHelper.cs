// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;

namespace woco_urogue {

	// ===================================================================================
	// LAYERS HELPER
	// ===================================================================================
	public class LayersHelper {

		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
		private static int defaultEnemy = -1;
		public static int DefaultEnemy {
			get {
				if (defaultEnemy == -1) {
					defaultEnemy = 1 << 0;
					defaultEnemy |= 1 << 10;
				}
				return defaultEnemy;
			}
		}
	
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
		private static int defaultEnemyPlayer = -1;
		public static int DefaultEnemyPlayer {
			get {
				if (defaultEnemyPlayer == -1) {
					defaultEnemyPlayer = 1 << 0;
					defaultEnemyPlayer |= 1 << 10;
					defaultEnemyPlayer |= 1 << 11;
				}
				return defaultEnemyPlayer;
			}
		}
		
		// -------------------------------------------------------------------------------
		
	}

}
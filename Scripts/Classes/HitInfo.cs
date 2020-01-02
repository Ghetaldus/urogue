// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;

using UnityEngine;

namespace woco_urogue {

	// ===================================================================================
	// HIT INFO
	// ===================================================================================
	[System.Serializable]
	public class HitInfo {

		public float baseAmount;
		public int amount;
		public DamageTypes type = DamageTypes.Normal;
		public DamageTemplate damage;
		public Vector3 point;
		public bool isShowEffect = true;
		public Character source;
		
		// -------------------------------------------------------------------------------
		// HitInfo (Constructor)
		// -------------------------------------------------------------------------------
		public HitInfo() { }
		
		// -------------------------------------------------------------------------------
		// HitInfo (Constructor)
		// -------------------------------------------------------------------------------
		public HitInfo(DamageTemplate damage, Vector3 point, bool isShowEffect, Character source=null, float BaseAmount=0) {
			this.baseAmount = BaseAmount;
			this.damage = damage;
			this.point = point;
			this.isShowEffect = isShowEffect;
			this.source = source;
			
			this.damage.Initalize();
			
		}
		
		// -------------------------------------------------------------------------------
		
	}

}
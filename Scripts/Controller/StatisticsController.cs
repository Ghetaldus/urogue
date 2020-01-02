// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;

using System.Collections.Generic;
using UnityEngine;

namespace woco_urogue {

	// ===================================================================================
	// STATISTICS CONTROLLER
	// ===================================================================================
	[System.Serializable]
	public class StatisticsController : BaseController {
		
		[Header("-=- Statistics -=-")]
		public List<Statistic> statistics;
			   
		// -----------------------------------------------------------------------------------
		// Initalize (Constructor)
		// -----------------------------------------------------------------------------------
		public override void Initalize(Character ownerobj=null) {
		
			base.Initalize(ownerobj);
		
			foreach (Statistic stat in statistics) {
				stat.Init(ownerobj);
			}

		}
	
		// -----------------------------------------------------------------------------------
		// Update
		// -----------------------------------------------------------------------------------
		public void Update() {
		
			if (Obj.GetGame.paused || owner == null) return;
		
			foreach (Statistic stat in statistics) {
				stat.Update();
			}
		
		}
		
		// ===================================================================================
		// SHORTCUTS
		//
		// These statistic getters/setters act as shortcuts to common values that are
		// required almost everywhere.
		// ===================================================================================
	
		// -----------------------------------------------------------------------------------
		// Health
		// When health reaches 0 this game object dies
		// -----------------------------------------------------------------------------------
		public float Health {
			get {
				NumericStatisticTemplate tmp_tmpl = StatisticsLibrary.Get("Health");
				int index = statistics.FindIndex(s => s.template == tmp_tmpl);
				if (index != -1) {
					return statistics[index].Value;
				} else {
					Debug.LogWarning("Required Statistic <Health> missing on: "+owner.name);
				}
				return -1;
			}
			set {
				NumericStatisticTemplate tmp_tmpl = StatisticsLibrary.Get("Health");
				int index = statistics.FindIndex(s => s.template == tmp_tmpl);
				if (index != -1) {
					statistics[index].Value = value;
				} else {
					Debug.LogWarning("Required Statistic <Health> missing on: "+owner.name);
				}
			
			}
		}
	
		// -----------------------------------------------------------------------------------
		// Mana
		// Used to cast spells - is also used as "ammo" by monsters
		// -----------------------------------------------------------------------------------
		public float Mana {
			get {
				NumericStatisticTemplate tmp_tmpl = StatisticsLibrary.Get("Mana");
				int index = statistics.FindIndex(s => s.template == tmp_tmpl);
				if (index != -1) {
					return statistics[index].Value;
				} else {
					Debug.LogWarning("Required Statistic <Mana> missing on: "+owner.name);
				}
				return -1;
			}
			set {
				NumericStatisticTemplate tmp_tmpl = StatisticsLibrary.Get("Mana");
				int index = statistics.FindIndex(s => s.template == tmp_tmpl);
				if (index != -1) {
					statistics[index].Value = value;
				} else {
					Debug.LogWarning("Required Statistic <Mana> missing on: "+owner.name);
				}
			}
		}
		
		// -----------------------------------------------------------------------------------
		// Speed
		// Movement and turning speed for both player and monsters
		// -----------------------------------------------------------------------------------
		public float Speed {
			get {
				NumericStatisticTemplate tmp_tmpl = StatisticsLibrary.Get("Speed");
				int index = statistics.FindIndex(s => s.template == tmp_tmpl);
				if (index != -1) {
					return statistics[index].Value;
				} else {
					Debug.LogWarning("Required Statistic <Speed> missing on: "+owner.name);
				}
				return -1;
			}
			set {
				NumericStatisticTemplate tmp_tmpl = StatisticsLibrary.Get("Speed");
				int index = statistics.FindIndex(s => s.template == tmp_tmpl);
				if (index != -1) {
					statistics[index].Value = value;
				} else {
					Debug.LogWarning("Required Statistic <Speed> missing on: "+owner.name);
				}
			}
		}	
		
		// -----------------------------------------------------------------------------------
		// Sight
		// The sight range for enemies, also affects trap sight for players etc.
		// -----------------------------------------------------------------------------------
		public float Sight {
			get {
				NumericStatisticTemplate tmp_tmpl = StatisticsLibrary.Get("Sight");
				int index = statistics.FindIndex(s => s.template == tmp_tmpl);
				if (index != -1) {
					return statistics[index].Value;
				} else {
					Debug.LogWarning("Required Statistic <Sight> missing on: "+owner.name);
				}
				return -1;
			}
			set {
				NumericStatisticTemplate tmp_tmpl = StatisticsLibrary.Get("Sight");
				int index = statistics.FindIndex(s => s.template == tmp_tmpl);
				if (index != -1) {
					statistics[index].Value = value;
				} else {
					Debug.LogWarning("Required Statistic <Sight> missing on: "+owner.name);
				}
			}
		}
		
		// -----------------------------------------------------------------------------------
		// Accuracy
		// Increases the chance to add a Status effect to an attacked enemy
		// -----------------------------------------------------------------------------------
		public float Accuracy {
			get {
				NumericStatisticTemplate tmp_tmpl = StatisticsLibrary.Get("Accuracy");
				int index = statistics.FindIndex(s => s.template == tmp_tmpl);
				if (index != -1) {
					return statistics[index].Value;
				} else {
					Debug.LogWarning("Required Statistic <Accuracy> missing on: "+owner.name);
				}
				return -1;
			}
			set {
				NumericStatisticTemplate tmp_tmpl = StatisticsLibrary.Get("Accuracy");
				int index = statistics.FindIndex(s => s.template == tmp_tmpl);
				if (index != -1) {
					statistics[index].Value = value;
				} else {
					Debug.LogWarning("Required Statistic <Accuracy> missing on: "+owner.name);
				}
			}
		}
		
		// -----------------------------------------------------------------------------------
		// Resistance
		// Decreases the chance to be affected by a status effect
		// -----------------------------------------------------------------------------------
		public float Resistance {
			get {
				NumericStatisticTemplate tmp_tmpl = StatisticsLibrary.Get("Resistance");
				int index = statistics.FindIndex(s => s.template == tmp_tmpl);
				if (index != -1) {
					return statistics[index].Value;
				} else {
					Debug.LogWarning("Required Statistic <Resistance> missing on: "+owner.name);
				}
				return -1;
			}
			set {
				NumericStatisticTemplate tmp_tmpl = StatisticsLibrary.Get("Resistance");
				int index = statistics.FindIndex(s => s.template == tmp_tmpl);
				if (index != -1) {
					statistics[index].Value = value;
				} else {
					Debug.LogWarning("Required Statistic <Resistance> missing on: "+owner.name);
				}
			}
		}		
		

		// -----------------------------------------------------------------------------------

	}

}
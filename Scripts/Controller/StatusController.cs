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
	// STATUS CONTROLLER
	// ===================================================================================
	public class StatusController : BaseController {
		
		[Header("-=- Status -=-")]
		public List<Status> states;
		public int slotsCount = 6;
	
		// -----------------------------------------------------------------------------------
		// Initalize (Constructor)
		// -----------------------------------------------------------------------------------
		public override void Initalize(Character ownerobj=null) {
			base.Initalize(ownerobj);
			states = new List<Status>(new Status[slotsCount]);
			for (int i = 0; i < states.Count; ++i) {
				states[i] = new Status();
			}
		}
	
		// -----------------------------------------------------------------------------------
		// update
		// -----------------------------------------------------------------------------------
		public void update() {
			foreach (Status tmp_tmpl in states) {
				if (!tmp_tmpl.isValid) {
					RemoveStatus(tmp_tmpl.template);
				}
			}
		}
	
		// -----------------------------------------------------------------------------------
		// GetLatestStatus
		// -----------------------------------------------------------------------------------
		public StatusTemplate GetLatestStatus() {
			foreach (Status tmp_tmpl in states) {
				if (tmp_tmpl.isValid) {
					return tmp_tmpl.template;
				}
			}
			return null;
		}
		
		// -------------------------------------------------------------------------------
		// getHasStates
		// -------------------------------------------------------------------------------
		public bool getHasStates(StatusTemplate[] requiredStatus) {
			if (requiredStatus.Length <= 0) return false;
			foreach (StatusTemplate status in requiredStatus) {
				if (status == null || GetHasStatus(status))
					return true;
			}
			return false;
		}
		
		// -----------------------------------------------------------------------------------
		// GetHasStatus
		// -----------------------------------------------------------------------------------
		public bool GetHasStatus(StatusTemplate tmpl) {
			foreach (Status tmp_tmpl in states) {
				if (tmp_tmpl.isValid && tmp_tmpl.template == tmpl) {
					return true;
				}
			}
			return false;
		}
	
		// -----------------------------------------------------------------------------------
		// AddStatus
		// -----------------------------------------------------------------------------------
		public bool AddStatus(StatusTemplate tmpl) {
			if (tmpl != null && states != null) {
				RemoveStatus(tmpl);
				int index = states.FindIndex(i => !i.isValid);
				if (index >= 0) {
					states[index] = new Status(tmpl, owner);
					if (owner is Player)
						UIController.Instance.ShowError(tmpl.description.getDescription);
					return true;
				}
			}
			return false;
		}
	
		// -----------------------------------------------------------------------------------
		// RemoveStatus
		// -----------------------------------------------------------------------------------
		public bool RemoveStatus(StatusTemplate tmpl) {
			if (tmpl != null && states != null) {
				int index = states.FindIndex(i => i.template == tmpl);
				if (index >= 0) {
					states[index].Deactivate();
					return true;
				}
			}
			return false;
		}
	
		// -----------------------------------------------------------------------------------
		// TryApplyStatus
		// try to apply a certain tmpl effect with a optionally specified chance
		// -----------------------------------------------------------------------------------
		public bool TryApplyStatus(StatusTemplate tmpl, float tmplChance=1.0f) {
			if (tmpl != null) {
					
				if (owner != null && !tmpl.ignoreResistance)
					tmplChance -= owner.statistics.Resistance;

				if (tmplChance > 0 && (Random.value <= tmplChance) ) {
					return AddStatus(tmpl);
				}
			
			}
			return false;
		}
	
		// -----------------------------------------------------------------------------------
		// TryRemoveStatus
		// try to remove a certain tmpl effect with a optionally specified chance
		// -----------------------------------------------------------------------------------
		public bool TryRemoveStatus(StatusTemplate tmpl, float tmplChance=1.0f) {
			if (tmplChance > 0 && tmpl != null && Random.value <= tmplChance) {
				return RemoveStatus(tmpl);
			}
			return false;
		}
	
		// -----------------------------------------------------------------------------------
	  
	}

}
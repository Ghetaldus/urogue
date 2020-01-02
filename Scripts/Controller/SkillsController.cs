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
	// SKILL CONTROLLER
	// ===================================================================================
	[System.Serializable]
	public class SkillsController : BaseController {
	
		[Header("-=- Skills -=-")]
		public List<Skill> skills;
		public int slotsCount = 20;
		
		// -----------------------------------------------------------------------------------
		// Initalize (Constructor)
		// -----------------------------------------------------------------------------------
		public override void Initalize(Character ownerobj=null) {
			base.Initalize(ownerobj);
			skills = new List<Skill>(new Skill[slotsCount]);
			for (int i = 0; i < skills.Count; ++i) {
				skills[i] = new Skill();
			}
		}
   
 		// -----------------------------------------------------------------------------------
		// AddSkill
		// -----------------------------------------------------------------------------------
		public bool AddSkill(SkillTemplate tmpl, int level=0) {
			if (tmpl != null) {
				if (!getHasSkill(tmpl, level)) {
					int index = skills.FindIndex(i => !i.isValid);
					if (index >= 0) {
						skills[index] = new Skill(tmpl, owner, level);
						return true;
					}
				}
			}
			return false;
		}   
   
		// -----------------------------------------------------------------------------------
		// skillCheck
		// -----------------------------------------------------------------------------------
		public bool skillCheck(SkillTemplate tmpl, float baseDifficulty) {
			if (tmpl == null || baseDifficulty == 0) return true;
			if (baseDifficulty >= 1.0f || !getHasSkill(tmpl)) return false;
			
			Skill skill = getSkill(tmpl);
			
			float baseChance = baseDifficulty - (skill.MaxValue / skill.Value);
			
			if (Random.value <= baseChance) {
				return true;
			}
			
			return false;
		}


		// -----------------------------------------------------------------------------------
		// getHasSkill
		// -----------------------------------------------------------------------------------
		public bool getHasSkill(SkillTemplate tmpl, int level=0) {
			if (tmpl == null || level == 0) return true;
			foreach (Skill skill in skills) {
				if (skill.isValid && skill.template == tmpl && skill.Value >= level) {
					return true;
				}
			}
			return false;
		}
		
		// -----------------------------------------------------------------------------------
		// getSkill
		// -----------------------------------------------------------------------------------
		public Skill getSkill(SkillTemplate tmpl) {
			if (tmpl == null) return null;
			foreach (Skill skill in skills) {
				if (skill.isValid && skill.template == tmpl) {
					return skill;
				}
			}
			return null;
		}
				
		// -----------------------------------------------------------------------------------
	
	}

}
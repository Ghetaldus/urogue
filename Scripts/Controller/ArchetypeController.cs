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
	// ARCHETYPE CONTROLLER
	// ===================================================================================
	[System.Serializable]
	public class ArchetypeController : BaseController {

		public CharacterClassTemplate 	characterClass;
			   
		// -----------------------------------------------------------------------------------
		// Initalize (Constructor)
		// -----------------------------------------------------------------------------------
		public override void Initalize(Character ownerobj=null) {
			base.Initalize(ownerobj);
		}
		
		// -----------------------------------------------------------------------------------
		// InitalizeClass
		// -----------------------------------------------------------------------------------
		public void InitalizeArchetype(CharacterClassTemplate tmpl) {
			if (tmpl != null && owner != null) {
				
				characterClass = tmpl;
				
				setDefaultAttributes();
				setDefaultStats();
				setDefaultResistances();
				setDefaultItems();
				setDefaultSkills();
				
			}
		}
		
		// -----------------------------------------------------------------------------------
		// setDefaultAttributes
		// -----------------------------------------------------------------------------------
		public void setDefaultAttributes() {
			foreach (Attribute attribute in characterClass.defaultAttributes) {
				int index = owner.attributes.attributes.FindIndex(a => a.template == attribute.template);
				if (index != -1)
					owner.attributes.attributes[index].defaultValue += attribute.defaultValue;
					owner.attributes.attributes[index].Init(owner);
			}
		}
		
		// -----------------------------------------------------------------------------------
		// setDefaultStats
		// -----------------------------------------------------------------------------------
		public void setDefaultStats() {
			foreach (Statistic statistic in characterClass.defaultStatistics) {
				int index = owner.statistics.statistics.FindIndex(s => s.template == statistic.template);
				if (index != -1)
					owner.statistics.statistics[index].defaultValue += statistic.defaultValue;
					owner.statistics.statistics[index].Init(owner);
			}
		}

		// -----------------------------------------------------------------------------------
		// setDefaultResistances
		// -----------------------------------------------------------------------------------
		public void setDefaultResistances() {
			foreach (Element element in characterClass.defaultResistances) {
				int index = owner.resistances.resistances.FindIndex(e => e.template == element.template);
				if (index != -1)
					owner.resistances.resistances[index].defaultValue += element.defaultValue;
					owner.resistances.resistances[index].Init(owner);
			}
		}

		// -----------------------------------------------------------------------------------
		// setDefaultItems
		// -----------------------------------------------------------------------------------
		public void setDefaultItems() {
			if (owner is Player) {
				foreach (ItemDrop item in characterClass.defaultItems) {
					item.Initalize();
					Item Item = new Item(item.template, item.durability, item.charges, item.level, item.identified);
					((Player)owner).inventory.AddItem(Item);					
				}
			}
		}
		
		// -----------------------------------------------------------------------------------
		// setDefaultSkills
		// -----------------------------------------------------------------------------------
		public void setDefaultSkills() {
			foreach (SkillLevel skill in characterClass.defaultSkills) {
				owner.skills.AddSkill(skill.template, skill.level);	
			}
		}
		
		// -----------------------------------------------------------------------------------

	}

}
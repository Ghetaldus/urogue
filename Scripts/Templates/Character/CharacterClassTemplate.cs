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
	// CHARACTER CLASS TEMPLATE 
	// =======================================================================================
	[CreateAssetMenu(fileName="New Character Class", menuName="uRogue Character/New Character Class", order=999)]
	public class CharacterClassTemplate : BaseTemplate {
		
		[Header("-=- Character Class -=-")]
		public DescriptionTemplate description;
		
		public List<Attribute> defaultAttributes;
		public List<Statistic> defaultStatistics;
		public List<Element> defaultResistances;
		
		public SkillLevel[] defaultSkills;
		public ItemDrop[] defaultItems;

		
		// -----------------------------------------------------------------------------------
    
	}

}

// =======================================================================================
// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace woco_urogue {

	// =======================================================================================
	// NUMERIC STATISTIC TEMPLATE 
	// =======================================================================================
	[CreateAssetMenu(fileName="NumericStatistic", menuName="uRogue Numeric/New NumericStatistic", order=999)]
	public class NumericStatisticTemplate : PropertyTemplate {
	
		public int minValue;
		[Range(0,1)] public float startPercentage;
		[Range(-999,999)]public int regeneration;
		
		[Header("-=- Status Triggers -=-")]	
		public StatusChance[] onMaxAddStatus;
		public StatusChance[] onZeroAddStatus;
		public StatusChance[] onNonMaxAddStatus;
		public StatusChance[] onNonZeroAddStatus;
		
		[Header("-=- Event Triggers -=-")]	
		public bool killOwnerOnZero;
		public bool resetOnMax;
		public bool resetCarryOver;
		
		public PropertyModifier[] OnMaxModifier;
		
		[Header("-=- Maximum Increase Properties -=-")]		
		public PropertyModifier[] MaxIncreaseProperties;
		
		// -----------------------------------------------------------------------------------
    
	}

}

// =======================================================================================
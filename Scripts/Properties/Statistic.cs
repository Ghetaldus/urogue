// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;
using System;
using UnityEngine;

namespace woco_urogue {

	// =======================================================================================
	// STATISTIC
	// =======================================================================================
	[System.Serializable]
	public class Statistic : PropertyBase {
	
		public NumericStatisticTemplate template = null;
		
		public float Regeneration;
		
		[HideInInspector]public bool blockRegeneration;
		[HideInInspector]public bool blockDegeneration;
		
		protected float lastUpdate;
		protected float updateInterval;
		protected float updateTime;
		
		protected bool wasMax;
		protected bool wasMin;
		
		// =======================================================================================
		// GETTERS / SETTERS
		// =======================================================================================
		
		// -----------------------------------------------------------------------------------
		// Value
		// -----------------------------------------------------------------------------------
		public override float Value {
			get { return base.Value; }
        	set {
        		_value = value;
        		lastUpdate = Time.time;
        		if (_value < MaxValue) onNonMax();
        		if (_value > _minValue) onNonMin();
        		if (_value >= MaxValue) onMax();
        		if (_value <= _minValue) onMin();
        		if (Value > MaxValue) Value = MaxValue;
        		if (Value < _minValue) Value = _minValue;
        	}
		}
		
		// -----------------------------------------------------------------------------------
		// MaxValue
		// -----------------------------------------------------------------------------------
		public override float MaxValue {
			get {
				/*
				if (owner != null)
					_maxValue = owner.CalculateProperties(template.MaxIncreaseProperties, PropertyTypes.TotalValue, defaultValue);
				*/
				return _maxValue;
			}
        	set {
        		_maxValue = value;
        		
        		if (owner != null)
        			_maxValue = owner.CalculateProperties(template.MaxIncreaseProperties, PropertyTypes.TotalValue, defaultValue);
            	if (Value > MaxValue) Value = MaxValue;
            }
		}			

	   	// -----------------------------------------------------------------------------------
		// isMaximum
		// -----------------------------------------------------------------------------------
		public bool isMaximum(int val = 0) {
			return (Value + val > MaxValue) ? true : false;
		}
				
  		// -----------------------------------------------------------------------------------
		// ActualRegeneration
		// -----------------------------------------------------------------------------------
		public float ActualRegeneration {
			get {
				return template.regeneration + Regeneration;
			}
		}
		
  		// -----------------------------------------------------------------------------------
		// isActive
		// -----------------------------------------------------------------------------------
		public bool isActive {
			get {
				return (ActualRegeneration != 0 || (lastUpdate != 0 && Time.time - lastUpdate <= updateInterval) );
			}
		}
				
  		// -----------------------------------------------------------------------------------
		// Init
		// -----------------------------------------------------------------------------------
		public override void Init(Character ownerobj=null) {
			base.Init(ownerobj);
			MaxValue = defaultValue;
			Value = (int)Mathf.Round(MaxValue*template.startPercentage);
			updateInterval = Obj.GetGame.configuration.updateTime;
		}
		
   		// -----------------------------------------------------------------------------------
		// update
		// -----------------------------------------------------------------------------------
		public void Update() {
			if (updateInterval > 0 && ActualRegeneration != 0) {
				if (Time.time - updateTime > updateInterval) {
					updateTime = Time.time;
					if (blockRegeneration && ActualRegeneration > 0) return;
					if (blockDegeneration && ActualRegeneration < 0) return;
					Value += ActualRegeneration;
				}
			}	
		}

		// =======================================================================================
		// EVENT TRIGGERS
		// =======================================================================================
		
 		// -----------------------------------------------------------------------------------
		// onMax
		// -----------------------------------------------------------------------------------
    	protected override void onMax() {
    			
    		if (!isValid || wasMax) return;
    		
    		wasMax = true;
    				
			foreach (StatusChance tmpl in template.onMaxAddStatus) {
				if (tmpl.removeStatus) {
					owner.states.TryRemoveStatus(tmpl.template, tmpl.probability);
				} else {
					owner.states.TryApplyStatus(tmpl.template, tmpl.probability);
				}
			}

			owner.AdjustProperty(template.OnMaxModifier);

			float rest = MaxValue - _value;
			
			if (template.resetOnMax)
				_value = template.minValue;
			
			if (template.resetCarryOver)
				_value += rest;
			
    	}

		// -----------------------------------------------------------------------------------
		// onNonMax
		// -----------------------------------------------------------------------------------
    	protected override void onNonMax() {
    			
    		if (!isValid || !wasMax) return;
    		
    		wasMax = false;
    		
			foreach (StatusChance tmpl in template.onNonMaxAddStatus) {
				if (tmpl.removeStatus) {
					owner.states.TryRemoveStatus(tmpl.template, tmpl.probability);
				} else {
					owner.states.TryApplyStatus(tmpl.template, tmpl.probability);
				}
			}

			
			
    	}


 		// -----------------------------------------------------------------------------------
		// onMin
		// -----------------------------------------------------------------------------------
    	protected override void onMin() {
    		
    		if (!isValid || wasMin) return;
    		
    		wasMin = true;
    		
			foreach (StatusChance tmpl in template.onZeroAddStatus) {
				if (tmpl.removeStatus) {
					owner.states.TryRemoveStatus(tmpl.template, tmpl.probability);
				} else {
					owner.states.TryApplyStatus(tmpl.template, tmpl.probability);
				}
			}
		
			if (template.killOwnerOnZero && Value <= template.minValue) {
				owner.Death();
			}

    		_value = template.minValue;
    		
    	}

		// -----------------------------------------------------------------------------------
		// onNonMin
		// -----------------------------------------------------------------------------------
    	protected override void onNonMin() {
    		
    		if (!isValid || !wasMin) return;
    		
    		wasMin = false;
    		
			foreach (StatusChance tmpl in template.onNonZeroAddStatus) {
				if (tmpl.removeStatus) {
					owner.states.TryRemoveStatus(tmpl.template, tmpl.probability);
				} else {
					owner.states.TryApplyStatus(tmpl.template, tmpl.probability);
				}
			}
		
    		
    	}
		// -----------------------------------------------------------------------------------
		
	}	
	
	// =======================================================================================
	
}
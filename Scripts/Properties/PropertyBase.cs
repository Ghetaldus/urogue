// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;
using UnityEngine;

namespace woco_urogue {

	// =======================================================================================
	// BASE PROPERTY
	// =======================================================================================
	public class PropertyBase {
	
		public float defaultValue;
		
		protected Character owner;
		
		protected float _value;
		protected float _baseValue;
		protected float _bonusValue;
		protected float _minValue;
		protected float _maxValue;
		
		// =======================================================================================
		// GETTERS / SETTERS
		// =======================================================================================

		// -----------------------------------------------------------------------------------
		// Init
		// -----------------------------------------------------------------------------------
		public virtual void Init(Character ownerobj=null) {
			owner = ownerobj;
			MaxValue = defaultValue;
			Value = defaultValue;
		}

		// -----------------------------------------------------------------------------------
		// isValid
		// -----------------------------------------------------------------------------------
		public virtual bool isValid {
			get {
				return (owner != null && Obj.GetGame.ingame);
			}
		}

		// -----------------------------------------------------------------------------------
		// Value
		// -----------------------------------------------------------------------------------
		public virtual float Value {
			get { return _value + _baseValue; }
        	set { 
        		_value = value;
        		if (_value >= MaxValue) onMax();
        		if (_value <= _minValue) onMin();
        		if (Value > MaxValue) Value = MaxValue;
        	}
		}

		// -----------------------------------------------------------------------------------
		// BaseValue
		// -----------------------------------------------------------------------------------
		public float BaseValue {
			get { return _baseValue; }
        	set { 
        		_baseValue = value;
        	}
		}

		// -----------------------------------------------------------------------------------
		// BonusValue
		// -----------------------------------------------------------------------------------
		public float BonusValue {
			get { return _bonusValue; }
        	set { 
        		_bonusValue = value;
        	}
		}

		// -----------------------------------------------------------------------------------
		// MaxValue
		// -----------------------------------------------------------------------------------
		public virtual float MaxValue {
			get {
				return _maxValue;
			}
        	set {
        		_maxValue = value;
            	if (Value > MaxValue) Value = MaxValue;
            }
		}			
		
		// -------------------------------------------------------------------------------
		// TotalValue
		// -------------------------------------------------------------------------------
		public float TotalValue {
			get {
				return Value + BonusValue;
			}
		}
			
		// -----------------------------------------------------------------------------------
		// PercentValue
		// -----------------------------------------------------------------------------------
		public float PercentValue {
			get {
				return (Value != 0 && MaxValue != 0) ? (float)Value / (float)MaxValue : 0;
			}
		}
		
		// =======================================================================================
		// EVENT TRIGGERS
		// =======================================================================================

    	protected virtual void onMax() {}
    	protected virtual void onMin() {}
		protected virtual void onNonMax() {}
		protected virtual void onNonMin() {}
		
		// -----------------------------------------------------------------------------------
		
	}
		
	// =======================================================================================
	
}
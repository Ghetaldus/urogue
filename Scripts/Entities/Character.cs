// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace woco_urogue {

	// ===================================================================================
	// CHARACTER
	// ===================================================================================
	public abstract class Character : Entity {

		[Header("-=- Character Options -=-")]
		
		public SoundTemplate hitSound;
		public GameObject dropPrefab;
				
		public StatusController 		states = new StatusController();
		
		public AttributesController 	attributes;
		public ResistancesController 	resistances;
		public StatisticsController 	statistics;
		public SkillsController 		skills;
		
		protected CharacterController moveController;
		
		protected DamageTemplate fallDamage;
		
		public float hitTime 			{ get; protected set; }
		public float fallTime			{ get; protected set; }
		public float warpTime			{ get; protected set; }
		
    	public virtual void Death() {}

		// -----------------------------------------------------------------------------------
		// Awake
		// -----------------------------------------------------------------------------------
    	protected virtual void Awake() {
    		
    		moveController = GetComponent<CharacterController>();
    		
    		fallDamage = Obj.GetGame.configuration.fallDamage;
    		
    		attributes.Initalize(this);
    		resistances.Initalize(this);
    		statistics.Initalize(this);
    		states.Initalize(this);
    		
    	}
		
		// -----------------------------------------------------------------------------------
		// Update
		// -----------------------------------------------------------------------------------
    	protected virtual void Update() {
    		
    		if (!Obj.GetGame.ingame) return;
    
    		states.update();
        	statistics.Update();
    
    		checkFalling();
    
    	}
    	
  		// -----------------------------------------------------------------------------------
		// Hit
		// -----------------------------------------------------------------------------------
    	public virtual void Hit(HitInfo hitInfo) {
    		
    		float probability = 0;
    		
    		SoundController.Play(hitSound);
       	 	
       	 	// -- Remember time of last Hit (if that hit has triggerCombat set to true)
       	 	if (hitInfo.damage.triggerCombat) {
        		hitTime = Time.time;
        	}

        	// -- Deduct Health
        	statistics.Health -= hitInfo.amount;
        		
        	// -- Apply ACCURACY modifier to affect status application chance (only when ADDING status)
        	if (hitInfo.source != null)
        		probability = hitInfo.source.statistics.Accuracy;
        	
        	// -- Apply RESISTANCE modifier to affect status application chance (only when ADDING status)
        	probability += statistics.Resistance;
        	
        	// -- Try to apply all status changes (only if the attack did at least 1 damage)
        	if (statistics.Health > 0 && hitInfo.amount > 0) {
        	
				foreach (StatusChance status in hitInfo.damage.applyStatus) {
					if (status.removeStatus) {
						states.TryRemoveStatus(status.template, status.probability + probability);
					} else {
						states.TryApplyStatus(status.template, status.probability + probability);
					}
				}
				
				if (hitInfo.type == DamageTypes.Critical) {
					foreach (StatusChance status in hitInfo.damage.applyStatusOnCrit) {
						if (status.removeStatus) {
							states.TryRemoveStatus(status.template, status.probability);
						} else {
							states.TryApplyStatus(status.template, status.probability);
						}
					}
				}
			}
			
    	}
    	
		// -----------------------------------------------------------------------------------
		// DropItem
		// We require a dropItem function here as Instantiate is for MonoBehaviour only
		// -----------------------------------------------------------------------------------
		public void DropItem(Item item) {

			LootDrop drop = ((GameObject)Instantiate(dropPrefab, transform.position, Quaternion.identity)).GetComponent<LootDrop>();
			drop.transform.Translate(0, -(moveController.height / 2 + 0.08f), 0); // 0.08 - Skin Width
			drop.transform.Translate(transform.forward*0.5f);
		
			drop.item.template			= item.template;
			drop.item.defaultDurability = 1.0f;
			drop.item.defaultCharges	= 1.0f;
			drop.durability 			= item.durability;
			drop.charges 				= item.charges;

		}
		
		// -----------------------------------------------------------------------------------
		// Falling
		// -----------------------------------------------------------------------------------
		protected void checkFalling() {
			
			if (!moveController.isGrounded) {
				fallTime += Time.deltaTime;
			} else {
				if (fallTime*10 >= moveController.height) {
					if (fallDamage != null) {
						IHitable hitable = gameObject.GetComponent(typeof(IHitable)) as IHitable;
   		     			if (hitable != null) {
   		         			hitable.Hit(new HitInfo(fallDamage, transform.position, true, null, fallTime*moveController.height*10));
	        			}
        			}
				}
				fallTime = 0;
			}
		}
		
		// ===================================================================================
		// WARP & TELEPORTATION
		// ===================================================================================

		// -----------------------------------------------------------------------------------
		// Warp
		// -----------------------------------------------------------------------------------
		public void Warp(Transform targetTransform, float warpDelay=1f) {
			warpTime = Time.time;
			transform.position = targetTransform.position;
			transform.rotation = targetTransform.rotation;
		}
		
		// -----------------------------------------------------------------------------------
		// Warp
		// -----------------------------------------------------------------------------------
		public void Warp(string targetScene, int waypointId, float warpDelay=1f) {
			warpTime = Time.time;
			SceneManager.LoadScene(targetScene, LoadSceneMode.Single);
			Obj.GetPlayer.nextWayPoint = waypointId;
		}
	
		// ===================================================================================
		// PROPERTY CALCULATIONS
		// ===================================================================================

		// -----------------------------------------------------------------------------------
		// GetPropertySum
		// -----------------------------------------------------------------------------------
		public float GetPropertySum(PropertyModifier modifier, PropertyTypes property, int level) {
			
			float tmpValue = 0;
			int additive = 1;
			
			if (modifier != null) {
		
				if (modifier.template is NumericAttributeTemplate) {
			
					int index = attributes.attributes.FindIndex(m => m.template == modifier.template);
					if (index != -1) {
						if (property == PropertyTypes.Value) {
							tmpValue += GetModifierValue(modifier.FixedValue, modifier.PercentValue, level, additive, attributes.attributes[index].Value);
						} else if (property == PropertyTypes.TotalValue) {
							tmpValue += GetModifierValue(modifier.FixedValue, modifier.PercentValue, level, additive, attributes.attributes[index].TotalValue);
						} else if (property == PropertyTypes.BonusValue) {
							tmpValue += GetModifierValue(modifier.FixedValue, modifier.PercentValue, level, additive, attributes.attributes[index].BonusValue);
						}
					}
				
				} else if (modifier.template is NumericElementTemplate) {
							
					int index = resistances.resistances.FindIndex(m => m.template == modifier.template);
					if (index != -1) {
						if (property == PropertyTypes.Value) {
							tmpValue += GetModifierValue(modifier.FixedValue, modifier.PercentValue, level, additive, resistances.resistances[index].Value);
						} else if (property == PropertyTypes.TotalValue) {
							tmpValue += GetModifierValue(modifier.FixedValue, modifier.PercentValue, level, additive, resistances.resistances[index].TotalValue);
						} else if (property == PropertyTypes.BonusValue) {
							tmpValue += GetModifierValue(modifier.FixedValue, modifier.PercentValue, level, additive, resistances.resistances[index].BonusValue);
						}
					}
							
				} else if (modifier.template is NumericStatisticTemplate) {
			
					int index = statistics.statistics.FindIndex(m => m.template == modifier.template);
					if (index != -1) {
						if (property == PropertyTypes.Value) {
							tmpValue += GetModifierValue(modifier.FixedValue, modifier.PercentValue, level, additive, statistics.statistics[index].Value);
						} else if (property == PropertyTypes.TotalValue) {
							tmpValue += GetModifierValue(modifier.FixedValue, modifier.PercentValue, level, additive, statistics.statistics[index].TotalValue);
						} else if (property == PropertyTypes.BonusValue) {
							tmpValue += GetModifierValue(modifier.FixedValue, modifier.PercentValue, level, additive, statistics.statistics[index].BonusValue);
						}
					}
				
				} else if (modifier.template is SkillTemplate) {
				
					var index = skills.skills.FindIndex(m => m.template == modifier.template);
					if (index != -1) {
						if (property == PropertyTypes.Value) {
							tmpValue += GetModifierValue(modifier.FixedValue, modifier.PercentValue, level, additive, skills.skills[index].Value);
						} else if (property == PropertyTypes.TotalValue) {
							tmpValue += GetModifierValue(modifier.FixedValue, modifier.PercentValue, level, additive, skills.skills[index].TotalValue);
						} else if (property == PropertyTypes.BonusValue) {
							tmpValue += GetModifierValue(modifier.FixedValue, modifier.PercentValue, level, additive, skills.skills[index].BonusValue);
						}
					}
			
				} else if (modifier.template is StatusTemplate) {
			
					var index = states.states.FindIndex(m => m.template == modifier.template);
					if (index != -1) {
						if (property == PropertyTypes.Value) {
							tmpValue += GetModifierValue(modifier.FixedValue, modifier.PercentValue, level, additive, states.states[index].Value);
						} else if (property == PropertyTypes.TotalValue) {
							tmpValue += GetModifierValue(modifier.FixedValue, modifier.PercentValue, level, additive, states.states[index].TotalValue);
						} else if (property == PropertyTypes.BonusValue) {
							tmpValue += GetModifierValue(modifier.FixedValue, modifier.PercentValue, level, additive, states.states[index].BonusValue);
						}
					}
					
				}

			}
	
			return tmpValue;
			
		}	
		
		// -----------------------------------------------------------------------------------
		// AdjustProperty
		// -----------------------------------------------------------------------------------
		public void AdjustProperty(PropertyModifier[] modifiers, int level=0, int additive=1) {
			
			if (modifiers != null) {
			
				foreach (PropertyModifier modifier in modifiers) {

					if (modifier.template is NumericAttributeTemplate) {
				
						int index = attributes.attributes.FindIndex(m => m.template == modifier.template);
						if (index != -1) {
							
							if (modifier.propertyType == PropertyTypes.BonusValue) {
								attributes.attributes[index].BonusValue += GetModifierValue(modifier.FixedValue, modifier.PercentValue, level, additive, attributes.attributes[index].BonusValue);
							} else {
								attributes.attributes[index].Value += GetModifierValue(modifier.FixedValue, modifier.PercentValue, level, additive, attributes.attributes[index].MaxValue);
							}
						}
					
					} else if (modifier.template is NumericElementTemplate) {
								
						int index = resistances.resistances.FindIndex(m => m.template == modifier.template);
						if (index != -1) {
														
							if (modifier.propertyType == PropertyTypes.BonusValue) {
								resistances.resistances[index].BonusValue += GetModifierValue(modifier.FixedValue, modifier.PercentValue, level, additive, resistances.resistances[index].BonusValue);
							} else {
								resistances.resistances[index].Value += GetModifierValue(modifier.FixedValue, modifier.PercentValue, level, additive, resistances.resistances[index].MaxValue);
							}
						}
								
					} else if (modifier.template is NumericStatisticTemplate) {
				
						int index = statistics.statistics.FindIndex(m => m.template == modifier.template);
						if (index != -1) {
						
							if (modifier.propertyType == PropertyTypes.BonusValue) {
								statistics.statistics[index].BonusValue += GetModifierValue(modifier.FixedValue, modifier.PercentValue, level, additive, statistics.statistics[index].BonusValue);
							} else if (modifier.propertyType == PropertyTypes.Regeneration) {
								statistics.statistics[index].Regeneration += GetModifierValue(modifier.FixedValue, modifier.PercentValue, level, additive, statistics.statistics[index].Regeneration);
							} else {
								statistics.statistics[index].Value += GetModifierValue(modifier.FixedValue, modifier.PercentValue, level, additive, statistics.statistics[index].MaxValue);
							}
						
						}
					
					} else if (modifier.template is SkillTemplate) {
					
						var index = skills.skills.FindIndex(m => m.template == modifier.template);
						if (index != -1) {
							
							if (modifier.propertyType == PropertyTypes.BonusValue) {
								skills.skills[index].BonusValue += GetModifierValue(modifier.FixedValue, modifier.PercentValue, level, additive, skills.skills[index].BonusValue);
							} else {
								skills.skills[index].Value += GetModifierValue(modifier.FixedValue, modifier.PercentValue, level, additive, skills.skills[index].MaxValue);
							}
							
							
						}
				
					} else if (modifier.template is StatusTemplate) {
				
						var index = states.states.FindIndex(m => m.template == modifier.template);
						if (index != -1) {
							
							if (modifier.propertyType == PropertyTypes.BonusValue) {
								states.states[index].BonusValue += GetModifierValue(modifier.FixedValue, modifier.PercentValue, level, additive, states.states[index].BonusValue);
							} else {
								states.states[index].Value += GetModifierValue(modifier.FixedValue, modifier.PercentValue, level, additive, states.states[index].MaxValue);
							}
							
							
						}
						
					}

				}
			
			}
	
		}

		// -----------------------------------------------------------------------------------
		// GetModifierValue
		// -----------------------------------------------------------------------------------
		protected float GetModifierValue(float FixedValue, float PercentValue, int level=0, int additive=1, float baseValue=0) {
		
			float adjustValue = 0;
			
			if (PercentValue != 0 && baseValue != 0) {
				adjustValue = (baseValue * PercentValue) * additive;
			} else {
				adjustValue = (FixedValue * additive);
			}
			
			adjustValue += level;
			
			return adjustValue;
		
		}
				
		// -----------------------------------------------------------------------------------
		// CalculateProperties
		// -----------------------------------------------------------------------------------
		public float CalculateProperties(PropertyModifier[] modifiers, PropertyTypes valueType, float baseValue=0) {
			float tmpValue = 0;
			foreach (PropertyModifier modifier in modifiers) {
				tmpValue += GetPropertySum(modifier, valueType, 0);
			}
			return baseValue + tmpValue;
		}
	
		// -----------------------------------------------------------------------------------
		// CalculateBonusProperty
		// -----------------------------------------------------------------------------------
		public float CalculateBonusProperty(PropertyModifier modifier, int level) {
			return GetPropertySum(modifier, PropertyTypes.BonusValue, level);
		}

		// -----------------------------------------------------------------------------------
		// CheckProperty
		// -----------------------------------------------------------------------------------
		public bool CheckProperty(PropertyModifier[] modifiers) {
			foreach (PropertyModifier modifier in modifiers) {
				
				float tmpValue = GetPropertySum(modifier, PropertyTypes.TotalValue, 0);
				
				if (modifier.PercentValue != 0) {
					return (tmpValue >= modifier.FixedValue);
				} else {
					return (tmpValue >= tmpValue * modifier.PercentValue);
				}

			}
			return true;
		}
		
		// -------------------------------------------------------------------------------
	
	}

}
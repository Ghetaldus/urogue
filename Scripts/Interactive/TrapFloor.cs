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

	// ===================================================================================
	// FLOOR TRAP
	// ===================================================================================
	[HelpURL("http://example.com/docs/MyComponent.html")]
	[RequireComponent(typeof(BoxCollider))]
	public class TrapFloor : EntityBase, IInteractive {
		
		[Header("-=- Visibility Options -=-")]
		public bool IsVisible;
		public StatusTemplate[] seeTrapStatus;
		public SkillTemplate seeTrapSkill;
		public int seeTrapSkillLevel;
		
		[Header("-=- Trap Options -=-")]
		public GameObject trapProjectile;
		public int trapTriggerLimit;
		[Range(0,1)] public float dodgeDifficulty;
		
		[Header("-=- Disarm Options -=-")]
		public SkillTemplate disarmTrapSkill;
		[Range(0,1)] public float disarmDifficulty;
		
		[Header("-=- Avoid Options -=-")]
		public StatusTemplate[] avoidTrapStatus;
		public SkillTemplate avoidTrapSkill;
		
		[Header("-=- Messages & Sounds -=-")]
		public DescriptionTemplate trapTriggered;
		public DescriptionTemplate trapAvoided;
		public DescriptionTemplate disarmSuccess;
		public DescriptionTemplate disarmFailed;

		
		private SpriteRenderer spriterenderer;
		private int trapTriggerCounter;
				
		// -------------------------------------------------------------------------------
		// Awake
		// -------------------------------------------------------------------------------
		void Awake () {
			spriterenderer = GetComponent<SpriteRenderer>();
			spriterenderer.enabled = IsVisible;
			IsActive = true;
		}

		// -------------------------------------------------------------------------------
		// Useable
		// -------------------------------------------------------------------------------
		public bool Useable() {
			return IsActive && IsVisible;
		}

		// -------------------------------------------------------------------------------
		// Use
		// -------------------------------------------------------------------------------
		public void Use() {
			disarmTrap();
		}
	
		// -------------------------------------------------------------------------------
		// Update
		// Check if player can see the trap and if trap is visible to the player or not
		// -------------------------------------------------------------------------------
		public void Update () {		
			if (IsActive && IsVisible && trapTriggerCounter <= trapTriggerLimit) {
				RaycastHit hit;
				if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Obj.GetPlayer.statistics.Sight, LayersHelper.DefaultEnemyPlayer)) {
					if (hit.collider.gameObject.tag == "Player") {
						if (Obj.GetPlayer.states.getHasStates(seeTrapStatus) ||
							Obj.GetPlayer.skills.getHasSkill(seeTrapSkill, seeTrapSkillLevel) ) {
							spriterenderer.enabled = true;
						}
					}
				}
			}
			
		}
	
		// -------------------------------------------------------------------------------
		// OnTriggerEnter
		// -------------------------------------------------------------------------------
		public void OnTriggerEnter(Collider other) {
			if (IsActive) {
				Character victim = other.gameObject.GetComponent(typeof(Character)) as Character;
				if (victim != null)
					TriggerTrap(victim);
			}
		}
		
		// -------------------------------------------------------------------------------
		// TriggerTrap
		// Launch the trap either triggered by the Player (player only)
		// -------------------------------------------------------------------------------
		public void TriggerTrap(Character victim) {
			if (!dodgeTrap(victim)) {
				trapTriggerCounter++;
				Vector3 lookDir = Obj.GetPlayer.transform.position - transform.position;
				Instantiate(trapProjectile, transform.position, Quaternion.LookRotation(lookDir));
				
				if (victim is Player)
					UIController.Instance.ShowError(trapTriggered);
			}
		}	

		// -------------------------------------------------------------------------------
		// disarmTrap
		// 
		// -------------------------------------------------------------------------------
		private void disarmTrap() {
			if (Useable() && Obj.GetPlayer.skills.skillCheck(disarmTrapSkill, disarmDifficulty)) {	
				IsActive = false;
				UIController.Instance.ShowText(disarmSuccess);
			} else {
				TriggerTrap(Obj.GetPlayer);
				UIController.Instance.ShowError(disarmFailed);
			}
		}	
	
		// -------------------------------------------------------------------------------
		// dodgeTrap
		// check if Player is able to dodge the trap (player only)
		// -------------------------------------------------------------------------------
		private bool dodgeTrap(Character victim) {
			if (trapProjectile != null && (trapTriggerLimit == 0 || trapTriggerCounter < trapTriggerLimit) ) {
				if (
				 !victim.states.getHasStates(avoidTrapStatus) &&
			 	 !victim.skills.skillCheck(avoidTrapSkill, dodgeDifficulty)
			 	) {
			 		return false;
			 	}
			 	if (victim is Player)
			 	 	UIController.Instance.ShowText(trapAvoided);
				return true;
			}
			return true;
		}

		
		// -------------------------------------------------------------------------------
	
	}

}
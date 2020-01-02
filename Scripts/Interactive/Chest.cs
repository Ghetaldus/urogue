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
	// CHEST
	// ===================================================================================
#pragma warning disable
	[HelpURL("http://example.com/docs/MyComponent.html")]
	public class Chest : EntityBase, IInteractive {
		
		[Header("-=- Options -=-")]
		[Tooltip("Can be opened manually by player?")]public bool canOpen;
		
		[Header("-=- Lock/Keys -=-")]
		public ItemTemplate[] unlockKeyItems;
		public SkillTemplate unlockChestSkill;
		public int unlockChestSkillLevel;
		
		[Header("-=- Trap -=-")]
		[Range(0,1)]public float trapDifficulty;
		public GameObject trapProjectile;
		public int trapTriggerLimit;
		
		[Header("-=- Avoid Options -=-")]
		public StatusTemplate[] avoidTrapStatus;
		public SkillTemplate avoidTrapSkill;
		
		[Header("-=- Loot -=-")]
    	public GameObject dropPrefab;
    	public ItemDropProbability[] items;
		
		[Header("-=- Messages & Sounds -=-")]
		public SoundTemplate openSound;
		public DescriptionTemplate keyRequired;
		public DescriptionTemplate trapTriggered;
		public DescriptionTemplate trapAvoided;
		public DescriptionTemplate keyUsed;
		public DescriptionTemplate unlockSuccess;
		public DescriptionTemplate unlockFailed;
		
		private bool looted;
		private bool unlocked;
		private int trapTriggerCounter;
		private Animator animator;
		
		// -------------------------------------------------------------------------------
		// Awake
		// -------------------------------------------------------------------------------
		void Awake () {
			//animator = GetComponent<Animator>();
			IsActive = true;
			//animator.SetBool("IsOpen", IsOpen);
		}

		// -------------------------------------------------------------------------------
		// Useable
		// -------------------------------------------------------------------------------
		public bool Useable() {
			return IsActive;
		}

		// -------------------------------------------------------------------------------
		// Use
		// -------------------------------------------------------------------------------
		public void Use() {
			UseOpen();
			
			// fhiz: todo:
			// the plan was to have a "Close" function as well
			
		}
		
		// -------------------------------------------------------------------------------
		// UseOpen
		// Open the door via player use action (triggers traps, consumes keys and so on)
		// -------------------------------------------------------------------------------
		private void UseOpen() {
			if (looted == true || canOpen == false) return;
			
			if (unlocked || unlockChest() ) {
				SoundController.Play(openSound, transform.position);
				//animator.SetBool("IsOpen", true);
				//IsActive = canClose;
				//IsOpen = true;

				Open();

			} else {
				UIController.Instance.ShowError(keyRequired);
			}
		}
		
		// -------------------------------------------------------------------------------
		// Open
		// Auto open the door (either by player or via external action)
		// -------------------------------------------------------------------------------
		public void Open() {
			
			//animator.SetBool("IsOpen", true);
						
			SoundController.Play(openSound, transform.position);
			Utl.SpawnItems(items, transform.position, dropPrefab);
			looted = true;
			
		}
		
		// -------------------------------------------------------------------------------
		// TriggerTrap
		// Launch the trap either triggered by the Player (player only)
		// -------------------------------------------------------------------------------
		public void TriggerTrap() {
			if (!dodgeTrap()) {
				trapTriggerCounter++;
				Vector3 lookDir = Obj.GetPlayer.transform.position - transform.position;
				Instantiate(trapProjectile, transform.position, Quaternion.LookRotation(lookDir));
				UIController.Instance.ShowError(trapTriggered);
			}
		}	
		
		// -------------------------------------------------------------------------------
		// dodgeTrap
		// check if Player is able to dodge the trap (player only)
		// -------------------------------------------------------------------------------
		private bool dodgeTrap() {
			if (trapProjectile != null && (trapTriggerLimit == 0 || trapTriggerCounter < trapTriggerLimit) ) {
				 if (!Obj.GetPlayer.states.getHasStates(avoidTrapStatus) ||
					!Obj.GetPlayer.skills.skillCheck(avoidTrapSkill, trapDifficulty) ) {
			 	 	return false;
			 	 }
			 	 UIController.Instance.ShowText(trapAvoided);
				return true;
			}
			return true;
		}
		
		// -------------------------------------------------------------------------------
		// unlockChest
		// -------------------------------------------------------------------------------
		private bool unlockChest() {
			if (Obj.GetPlayer.inventory.FindAndDestroyItem(unlockKeyItems)) {
				UIController.Instance.ShowText(keyUsed);
				return true;
			} else if (Obj.GetPlayer.skills.getHasSkill(unlockChestSkill, unlockChestSkillLevel)) {
				UIController.Instance.ShowText(unlockSuccess);
				return true;
			}
			UIController.Instance.ShowError(unlockFailed);
			return false;
		}	

		// -------------------------------------------------------------------------------
	
	}
#pragma warning enable
	// ===================================================================================

}
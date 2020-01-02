// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;
using UnityEngine;

namespace woco_urogue {

	// ===================================================================================
	// DOOR
	// ===================================================================================
#pragma warning disable
	[HelpURL("http://example.com/docs/MyComponent.html")]
	class Door : EntityBase, IInteractive {

		[Header("-=- Options -=-")]
		[Tooltip("Starts the game opened?")]public bool IsOpen;
		[Tooltip("Can be opened manually by player?")]public bool canOpen;
		[Tooltip("Can be closed manually by player?")]public bool canClose;
		
		[Header("-=- Lock/Keys -=-")]
		public ItemTemplate[] unlockKeyItems;
		public SkillTemplate unlockDoorSkill;
		public int unlockDoorSkillLevel;
		
		[Header("-=- Trap -=-")]
		public GameObject trapProjectile;
		public int trapTriggerLimit;
		public StatusTemplate[] avoidTrapStatus;
		public SkillTemplate avoidTrapSkill;
		[Range(0,1)]public float dodgeDifficulty;
		
		[Header("-=- Messages & Sounds -=-")]
		public SoundTemplate openSound;
		public SoundTemplate closeSound;
		
		public DescriptionTemplate keyRequired;
		public DescriptionTemplate trapTriggered;
		public DescriptionTemplate trapAvoided;
		
		public DescriptionTemplate keyUsed;
		public DescriptionTemplate unlockSuccess;
		public DescriptionTemplate unlockFailed;
		
		private bool unlocked;
		private int trapTriggerCounter;
		private Animator animator;

		// -------------------------------------------------------------------------------
		// Awake
		// -------------------------------------------------------------------------------
		void Awake() {
			animator = GetComponent<Animator>();
			IsActive = true;
			animator.SetBool("IsOpen", IsOpen);
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
			Toggle();
		}
		
		// -------------------------------------------------------------------------------
		// Toggle
		// -------------------------------------------------------------------------------
		private void Toggle() {
			if (IsOpen) {
				UseClose();
			} else {
				TriggerTrap();
				UseOpen();
			}
		}
		
		// -------------------------------------------------------------------------------
		// UseOpen
		// Open the door via player use action (triggers traps, consumes keys and so on)
		// -------------------------------------------------------------------------------
		private void UseOpen() {
			if (IsOpen == true || canOpen == false) return;
			if (unlocked || unlockDoor() ) {
				SoundController.Play(openSound, transform.position);
				animator.SetBool("IsOpen", true);
				IsActive = canClose;
				IsOpen = true;
				
				if (canClose)
					unlocked = true;
				
			} else {
				UIController.Instance.ShowError(keyRequired);
			}
		}
		
		// -------------------------------------------------------------------------------
		// UseClose
		// Close the door via player use action
		// -------------------------------------------------------------------------------
		private void UseClose() {
			if (IsOpen == false || canClose == false) return;
			SoundController.Play(closeSound, transform.position);
			animator.SetBool("IsOpen", false);
			IsOpen = false;
		}
		
		// -------------------------------------------------------------------------------
		// Open
		// Auto open the door (either by player or via external action)
		// -------------------------------------------------------------------------------
		public void Open() {
			if (IsOpen == true) return;
			SoundController.Play(openSound, transform.position);
			animator.SetBool("IsOpen", true);
			IsActive = canClose;
			IsOpen = true;	
		}
		
		// -------------------------------------------------------------------------------
		// Close
		// Auto close the door (either by player or via external action)
		// -------------------------------------------------------------------------------
		public void Close() {
			if (IsOpen == false) return;
			SoundController.Play(closeSound, transform.position);
			animator.SetBool("IsOpen", false);
			IsOpen = false;
		}

		// -------------------------------------------------------------------------------
		// TriggerTrap
		// If a trap is set on the door, check and eventually trigger it
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
		// -------------------------------------------------------------------------------
		private bool dodgeTrap() {
			if (trapProjectile != null && (trapTriggerLimit == 0 || trapTriggerCounter < trapTriggerLimit) ) {
				 if (
				 Obj.GetPlayer.states.getHasStates(avoidTrapStatus) ||
			 	 Obj.GetPlayer.skills.skillCheck(avoidTrapSkill, dodgeDifficulty)
			 	 ) {
			 	 	UIController.Instance.ShowText(trapAvoided);
					return true;
			 	 }
				return false;
			}
			return true;
		}
		
		
		// -------------------------------------------------------------------------------
		// unlockDoor
		// -------------------------------------------------------------------------------
		private bool unlockDoor() {
			if (Obj.GetPlayer.inventory.FindAndDestroyItem(unlockKeyItems)) {
				UIController.Instance.ShowText(keyUsed);
				return true;
			} else if (Obj.GetPlayer.skills.getHasSkill(unlockDoorSkill, unlockDoorSkillLevel)) {
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
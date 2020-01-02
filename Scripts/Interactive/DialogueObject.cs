// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;

using UnityEngine;

namespace woco_urogue {

	// ===================================================================================
	// DIALOGUE OBJECT
	// ===================================================================================
	public class DialogueObject : EntityBase, IInteractive {

		[Header("-=- Dialogue Options -=-")]
		
		public StatusTemplate[] requiredStatus;
		public SkillTemplate requiredSkill;
		public int requiredSkillLevel;
		
		public DialogueTemplate[] dialogue;
		
		[Header("-=- Messages -=-")]
		public DescriptionTemplate cannotCommunicate;
		
		// -------------------------------------------------------------------------------
		// Useable
		// -------------------------------------------------------------------------------
		public bool Useable() {
			return true;
		}
		
		// -------------------------------------------------------------------------------
		// Use
		// -------------------------------------------------------------------------------
		public void Use() {
			if (dialogue != null) {
			
				if (Obj.GetPlayer.states.getHasStates(requiredStatus) ||
					Obj.GetPlayer.skills.getHasSkill(requiredSkill, requiredSkillLevel)) {
			
					Obj.GetGame.GameState = GameStates.Dialog;
					UIController.Instance.dialogScreen.SetDialog(dialogue);
				
				} else {
					UIController.Instance.ShowError(cannotCommunicate);
				}
			}
		}
	
		// -------------------------------------------------------------------------------
		
	}

}
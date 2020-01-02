// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;

using UnityEngine.UI;

namespace woco_urogue {

	// =======================================================================================
	// DIALOG SCREEN
	// =======================================================================================
	public class DialogScreen : Panel {
	
		public Image portraitImage;
		public Text nameText;
		public Text messageText;

		private DialogueTemplate[] dialogues;
		
		private int index = 0;

		// -----------------------------------------------------------------------------------
		// Update
		// -----------------------------------------------------------------------------------
		void Update() {
			if (IsShown == false) return;
			
			if (Obj.GetInput.Use == true) {
				index++;
				if (index >= dialogues.Length) {
					Obj.GetGame.GameState = GameStates.Game;
				} else {
					UpdateDialogue();
				}
			}
			
		}

		// -----------------------------------------------------------------------------------
		// SetDialog
		// -----------------------------------------------------------------------------------
		public void SetDialog(DialogueTemplate[] dialogues) {
		
			if (dialogues.Length <= 0) {
				Obj.GetGame.GameState = GameStates.Game;
				return;
			}
			
			this.dialogues = dialogues;
			index = 0;
			UpdateDialogue();
		}

		// -----------------------------------------------------------------------------------
		// UpdateDialog
		// -----------------------------------------------------------------------------------
		private void UpdateDialogue() {
			
			portraitImage.sprite = dialogues[index].portrait;
			nameText.text = dialogues[index].description.getName;
			messageText.text = dialogues[index].description.getDescription;
		}
	
		// -----------------------------------------------------------------------------------
	}

}
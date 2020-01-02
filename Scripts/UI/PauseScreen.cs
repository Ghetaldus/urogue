// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;

using UnityEngine;
using UnityEngine.UI;

namespace woco_urogue {

	// ===================================================================================
	// PAUSE SCREEN
	// ===================================================================================
	public class PauseScreen : Panel {

		public Button optionsButton;
		public Button continueButton;
		public Button exitButton;
		
		public Text pauseHeadline;
		
		// -------------------------------------------------------------------------------
		// Start
		// -------------------------------------------------------------------------------
		void Start() {
			optionsButton.onClick.AddListener(OnClickOptions);
			continueButton.onClick.AddListener(OnClickContinue);
			exitButton.onClick.AddListener(OnClickExit);
		}
		
		// -------------------------------------------------------------------------------
		// Show
		// -------------------------------------------------------------------------------
		public override void Show() {
			
			pauseHeadline.text 		= DescriptionLibrary.GetName("Hdl_Pause");
			
			continueButton.GetComponentInChildren<Text>().text 	= DescriptionLibrary.GetName("Btn_Continue");
			optionsButton.GetComponentInChildren<Text>().text 	= DescriptionLibrary.GetName("Btn_GameOptions");
			exitButton.GetComponentInChildren<Text>().text 		= DescriptionLibrary.GetName("Btn_QuitGame");
			base.Show();
		}
		
		// -------------------------------------------------------------------------------
		// OnClickOptions
		// -------------------------------------------------------------------------------
		private void OnClickOptions() {
			Obj.GetGame.GameState = GameStates.Options;
		}
		
		// -------------------------------------------------------------------------------
		// OnClickContinue
		// -------------------------------------------------------------------------------
		private void OnClickContinue() {
			Obj.GetGame.GameState = GameStates.Game;
		}

		// -------------------------------------------------------------------------------
		// OnClickExit
		// -------------------------------------------------------------------------------
		private void OnClickExit() {
			Obj.GetGame.GameState = GameStates.Title;
		}
		
		// -------------------------------------------------------------------------------
	
	}

}
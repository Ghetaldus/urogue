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
	// TITLE SCREEN
	// ===================================================================================
	public class TitleScreen : Panel {

		public Button newButton;
		public Button optionsButton;
		public Button loadButton;
		public Button quitButton;
		
		// -----------------------------------------------------------------------------------
		// Start
		// -----------------------------------------------------------------------------------
		void Start() {
			newButton.onClick.AddListener(OnClickNewGame);
			loadButton.onClick.AddListener(OnClickLoadGame);
			optionsButton.onClick.AddListener(OnClickOptions);
			quitButton.onClick.AddListener(OnClickQuit);
		}
		
		// -----------------------------------------------------------------------------------
		// Init
		// -----------------------------------------------------------------------------------
		public override void Show() {
			newButton.GetComponentInChildren<Text>().text = DescriptionLibrary.GetName("Btn_NewGame");
			loadButton.GetComponentInChildren<Text>().text = DescriptionLibrary.GetName("Btn_LoadGame");
			optionsButton.GetComponentInChildren<Text>().text = DescriptionLibrary.GetName("Btn_GameOptions");
			quitButton.GetComponentInChildren<Text>().text = DescriptionLibrary.GetName("Btn_QuitGame");
			base.Show();
		}
		
		// -----------------------------------------------------------------------------------
		// OnClickNewGame
		// -----------------------------------------------------------------------------------
		private void OnClickNewGame() {
			Obj.GetGame.GameState = GameStates.Select;
		}
		
		// -----------------------------------------------------------------------------------
		// OnClickLoadGame
		// -----------------------------------------------------------------------------------
		private void OnClickLoadGame() {
			Obj.GetGame.GameState = GameStates.Game;
		}
		
		// -----------------------------------------------------------------------------------
		// OnClickOptions
		// -----------------------------------------------------------------------------------
		private void OnClickOptions() {
			Obj.GetGame.GameState = GameStates.Options;
		}
		
		// -----------------------------------------------------------------------------------
		// OnClickQuit
		// -----------------------------------------------------------------------------------
		private void OnClickQuit() {
			Application.Quit();
		}
		
		// -----------------------------------------------------------------------------------
		
	}

}
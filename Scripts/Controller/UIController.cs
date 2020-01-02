// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;

using UnityEngine;

namespace woco_urogue {

	// ===================================================================================
	// UI CONTROLLER
	// ===================================================================================
	public class UIController : Singleton<UIController> {
		
		public TitleScreen titleScreen;
		public SelectScreen selectScreen;
		public OptionsScreen optionsScreen;
		public PauseScreen pauseScreen;
		public GameScreen gameScreen;
		public GameOverScreen gameOverScreen;
		public DialogScreen dialogScreen;
		public InventoryScreen inventoryScreen;
		public StatsScreen statsScreen;
		public SkillScreen skillScreen;
		public SoundTemplate errorSound;
		
		// -------------------------------------------------------------------------------
		// Awake
		// -------------------------------------------------------------------------------
		void Awake() {
			Obj.GetGame.onStateChanged += OnGameStateChanged;
			DontDestroyOnLoad(this.gameObject);
		}
		
		// -------------------------------------------------------------------------------
		// OnGameStateChanged
		// -------------------------------------------------------------------------------
		void OnGameStateChanged(GameStates state) {
		
			if (state == GameStates.Game || state == GameStates.Combat) {
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			} else {
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}
			
			gameScreen.SetState(state == GameStates.Game || state == GameStates.Inventory || state == GameStates.Stats || state == GameStates.Skills || state == GameStates.Combat);
			optionsScreen.SetState(state == GameStates.Options);
			pauseScreen.SetState(state == GameStates.Pause);
			gameOverScreen.SetState(state == GameStates.GameOver);
			dialogScreen.SetState(state == GameStates.Dialog);
			inventoryScreen.SetState(state == GameStates.Inventory);
			statsScreen.SetState(state == GameStates.Stats);
			titleScreen.SetState(state == GameStates.Title);
			selectScreen.SetState(state == GameStates.Select);
			skillScreen.SetState(state == GameStates.Skills);
			
		}

		// -------------------------------------------------------------------------------
		// ShowText
		// -------------------------------------------------------------------------------
		public void ShowText(string vocab) {
			string tmp_vocab = LanguageLibrary.GetText(vocab);
			gameScreen.ShowError(tmp_vocab);
		}

		// -------------------------------------------------------------------------------
		// ShowText
		// -------------------------------------------------------------------------------
		public void ShowText(DescriptionTemplate text) {
			if (text != null)
				gameScreen.ShowError(text.getDescription);
		}
		
		// -------------------------------------------------------------------------------
		// ShowError
		// Displays an error message using the provided string as DescriptionTemplate name
		// -------------------------------------------------------------------------------
		public void ShowError(string error) {
			SoundController.Play(errorSound);
			string tmp_vocab = LanguageLibrary.GetText(error);
			gameScreen.ShowError(tmp_vocab);
			
		}
	
		// -------------------------------------------------------------------------------
		// ShowError
		// Displays an error using the provided DescriptionTemplate
		// -------------------------------------------------------------------------------
		public void ShowError(DescriptionTemplate error) {
			if (error != null) {
				SoundController.Play(errorSound);
				gameScreen.ShowError(error.getDescription);
			}
		}
	
		// -------------------------------------------------------------------------------
	
	}

}
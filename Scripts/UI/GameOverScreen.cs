// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;

using UnityEngine.UI;

namespace woco_urogue {

	// ===================================================================================
	// GAME OVER SCREEN
	// ===================================================================================
	public class GameOverScreen : Panel {
	
		public Text titleText;
		public Button restartButton;
		public Image deathEffect;

		// -----------------------------------------------------------------------------------
		// Awake
		// -----------------------------------------------------------------------------------
		void Awake() {
			restartButton.onClick.AddListener(OnRestartClick);
		}
	
		// -----------------------------------------------------------------------------------
		// Show
		// -----------------------------------------------------------------------------------
		public override void Show() {
			titleText.text = DescriptionLibrary.GetName("Hdl_GameOver");
			restartButton.GetComponentInChildren<Text>().text = DescriptionLibrary.GetName("Btn_Restart");
			deathEffect.gameObject.SetActive(Obj.GetPlayer.statistics.Health <= 0);
			base.Show();
		}

		// -----------------------------------------------------------------------------------
		// OnRestartClick
		// -----------------------------------------------------------------------------------
		private void OnRestartClick() {
			Obj.GetGame.GameState = GameStates.Title;
		}
		
		// -----------------------------------------------------------------------------------
	
	}

}
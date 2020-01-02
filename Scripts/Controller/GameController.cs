// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace woco_urogue {

	// ===================================================================================
	// GAME CONTROLLER
	// ===================================================================================
	public class GameController : MonoBehaviour {
		
		public ConfigurationTemplate configuration;
		[HideInInspector] public LanguageTemplate language;
		[HideInInspector] public float BGMVolume;
		[HideInInspector] public float SFXVolume;
		[HideInInspector] public bool gameStarted;
		
		public event System.Action<GameStates> onStateChanged;

		protected GameStates gameState;
		
		// -----------------------------------------------------------------------------------
		// paused
		// -----------------------------------------------------------------------------------
		public bool paused {
			get {
				return GameState != GameStates.Game && GameState != GameStates.Combat;
			}
		}
		
		// -----------------------------------------------------------------------------------
		// inmenu
		// -----------------------------------------------------------------------------------
		public bool inmenu {
			get {
				return GameState != GameStates.Game && GameState != GameStates.Combat;
			}
		}

		// -----------------------------------------------------------------------------------
		// ingame
		// -----------------------------------------------------------------------------------
		public bool ingame {
			get {
				return GameState == GameStates.Game || GameState == GameStates.Combat;
			}
		}		
		
		// -----------------------------------------------------------------------------------
		// 
		// -----------------------------------------------------------------------------------
		public GameStates GameState {
			get { 
				return gameState;
			}
			set {
				if (value == gameState) return;
				gameState = value;
				if (onStateChanged != null) onStateChanged(gameState);
			}
		}
	
		// -----------------------------------------------------------------------------------
		// 
		// -----------------------------------------------------------------------------------
		void Awake() {
			onStateChanged += OnStateChanged;
			language = configuration.defaultLanguage;
			SceneManager.LoadScene("GUI", LoadSceneMode.Additive);
			DontDestroyOnLoad(this.gameObject);
		}
	
		// -----------------------------------------------------------------------------------
		// Start
		// -----------------------------------------------------------------------------------
		void Start() {
			GameState = GameStates.Title;
		}
		
		// -----------------------------------------------------------------------------------
		// OnStateChanged
		// -----------------------------------------------------------------------------------
		private void OnStateChanged(GameStates state) {
			Time.timeScale = paused ? 0 : 1;
			MusicController.PlayGameStateBGM(state);
			if (ingame) gameStarted = true;
		}
	
		// -----------------------------------------------------------------------------------
		// Update
		// -----------------------------------------------------------------------------------
		void Update() {
		
			if (Obj.GetInput.Escape) {
				if (!inmenu) {
					GameState = GameStates.Pause;
				} else if (inmenu) {
					if (gameStarted) {
						GameState = GameStates.Game;
					} else {
						GameState = GameStates.Title;
					}
				}
			}

			if (Obj.GetInput.Inventory) {
				if (!inmenu) {
					GameState = GameStates.Inventory;
				} else if (inmenu) {
					GameState = GameStates.Game;
				}
			}
		
			if (Obj.GetInput.Stats) {
				if (!inmenu) {
					GameState = GameStates.Stats;
				} else if (inmenu) {
					GameState = GameStates.Game;
				}
			}
			
			if (Obj.GetInput.Skills) {
				if (!inmenu) {
					GameState = GameStates.Skills;
				} else if (inmenu) {
					GameState = GameStates.Game;
				}
			}
			
			if (!paused && !inmenu) {
				Player player = Obj.GetPlayer;
				if (player.hitTime > 0 && Time.time - player.hitTime < 30) {
					GameState = GameStates.Combat;
				} else {
					GameState = GameStates.Game;
				}
			}
			
		}
	
		// -----------------------------------------------------------------------------------
		// RestartGame
		// -----------------------------------------------------------------------------------
		public void RestartGame() {
			SceneManager.LoadScene("Level_0");
		}
		
		// -----------------------------------------------------------------------------------
		
	}

}
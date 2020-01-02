// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;

using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace woco_urogue {

	// ===================================================================================
	// SELECT SCREEN
	// ===================================================================================
	public class SelectScreen : Panel {

		public Dropdown classDropdown;
		public Button startButton;
		public Button cancelButton;
		public Text headlineText;
		
		// -----------------------------------------------------------------------------------
		// Start
		// -----------------------------------------------------------------------------------
		void Start() {
			startButton.onClick.AddListener(OnClickStartGame);
			cancelButton.onClick.AddListener(OnClickCancel);
		}
		
		// -----------------------------------------------------------------------------------
		// Show
		// -----------------------------------------------------------------------------------
		public override void Show() {
			headlineText.text =  DescriptionLibrary.GetName("Hdl_SelectClass");
			startButton.GetComponentInChildren<Text>().text = DescriptionLibrary.GetName("Btn_StartGame");
			cancelButton.GetComponentInChildren<Text>().text = DescriptionLibrary.GetName("Btn_Cancel");
			
			// fhiz: todo:
			// can't replace (name) with description.getName as it screws assigning the class
			// to the player later on
			
			classDropdown.options = CharacterClassLibrary.GetAllTemplates().Select(
            	p => new Dropdown.OptionData(p.name)
        		).ToList();
        	
        	base.Show();
		}
		
		// -----------------------------------------------------------------------------------
		// OnClickStartGame
		// -----------------------------------------------------------------------------------
		private void OnClickStartGame() {
			OnClassChanged();
			Obj.GetGame.GameState = GameStates.Game;
		}
		
		// -----------------------------------------------------------------------------------
		// OnClickCancel
		// -----------------------------------------------------------------------------------
		private void OnClickCancel() {
			Obj.GetGame.GameState = GameStates.Title;
		}
		
		// -----------------------------------------------------------------------------------
		// OnClassChanged
		// -----------------------------------------------------------------------------------
		public void OnClassChanged() {
			CharacterClassTemplate tmpl = CharacterClassLibrary.GetTemplate(classDropdown.options[classDropdown.value].text);
			
			Obj.GetPlayer.archetype.InitalizeArchetype(tmpl);
		}
		
		// -----------------------------------------------------------------------------------
		
	}

}
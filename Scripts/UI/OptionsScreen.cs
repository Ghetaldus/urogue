// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

// ===================================================================================
// OPTIONS SCREEN
// ===================================================================================
namespace woco_urogue {

	public class OptionsScreen : Panel {

		public Button continueButton;
		public Dropdown languageDropdown;
		public Slider bgmSlider;
		public Slider sfxSlider;
		
		public Text optionsHeadline;
		public Text musicVolumeHeadline;
		public Text soundVolumeHeadline;
		
		// -----------------------------------------------------------------------------------
		// Start
		// -----------------------------------------------------------------------------------
		void Start() {
			continueButton.onClick.AddListener(OnClickContinue);
			bgmSlider.value = MusicController.Volume;
			sfxSlider.value = SoundController.Volume;
		}
		
		// -----------------------------------------------------------------------------------
		// Show
		// -----------------------------------------------------------------------------------
		public override void Show() {
		
			optionsHeadline.text 		= DescriptionLibrary.GetName("Hdl_Options");
			musicVolumeHeadline.text 	= DescriptionLibrary.GetName("Hdl_MusicVolume");
			soundVolumeHeadline.text 	= DescriptionLibrary.GetName("Hdl_SoundVolume");
			
			continueButton.GetComponentInChildren<Text>().text = DescriptionLibrary.GetName("Btn_Confirm");
			
			languageDropdown.options = LanguageLibrary.GetAllTemplates().Select(
            	p => new Dropdown.OptionData(p.name)
        		).ToList();
			
			base.Show();	
		}
		
		// -----------------------------------------------------------------------------------
		// OnClickContinue
		// -----------------------------------------------------------------------------------
		private void OnClickContinue() {
			if (Obj.GetGame.gameStarted) {
				Obj.GetGame.GameState = GameStates.Game;
			} else {
				Obj.GetGame.GameState = GameStates.Title;
			}
		}

		// -----------------------------------------------------------------------------------
		// OnLangChanged
		// -----------------------------------------------------------------------------------
		public void OnLangChanged() {
			LanguageTemplate tmpl = LanguageLibrary.GetTemplate(languageDropdown.options[languageDropdown.value].text);
			Obj.GetGame.language = tmpl;
			Show();
		}
		
		// -----------------------------------------------------------------------------------
		// OnBGMVolumeChanged
		// -----------------------------------------------------------------------------------
		public void OnBGMVolumeChanged() {
			MusicController.Volume = bgmSlider.value;
		}
		
		// -----------------------------------------------------------------------------------
		// OnSFXVolumeChanged
		// -----------------------------------------------------------------------------------
		public void OnSFXVolumeChanged() {
			SoundController.Volume = sfxSlider.value;
		}
		
		// -----------------------------------------------------------------------------------
   
	}

}
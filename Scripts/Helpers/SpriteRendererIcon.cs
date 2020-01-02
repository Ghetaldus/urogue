// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;

using System;
using UnityEngine;
using UnityEngine.Networking;

namespace woco_urogue {

	// ===================================================================================
	// SPRITE RENDERER FOR ICONS
	// ===================================================================================
	[RequireComponent(typeof(SpriteRenderer))]
	public class SpriteRendererIcon : MonoBehaviour {

		[SerializeField] SpriteRenderer source;
		private Sprite myIcon;

		// -------------------------------------------------------------------------------
		// Update
		// -------------------------------------------------------------------------------
		void Update() {
			PaintIcon();
		}
	
		// -------------------------------------------------------------------------------
		// PaintIcon
		// -------------------------------------------------------------------------------
		void PaintIcon() {
			if (myIcon != null) {
				source.sprite = myIcon;
				source.enabled = true;
			} else {
				source.sprite = null;
				source.enabled = false;
			}
		 }
	 
		// -------------------------------------------------------------------------------
		// PaintIcon
		// -------------------------------------------------------------------------------
		public void SetIcon(Sprite newIcon) {
			myIcon = newIcon;
		}
	 
		// -------------------------------------------------------------------------------
		// RemoveIcon
		// -------------------------------------------------------------------------------
		public void RemoveIcon() {
			myIcon = null;
		}
	 
		// -------------------------------------------------------------------------------
	 
	}

}
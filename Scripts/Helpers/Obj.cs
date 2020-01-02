// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace woco_urogue {

	// ===================================================================================
	// OBJECT FINDER UTILITY
	// ===================================================================================
	public class Obj {
	
		protected static Player 			instance_player;
		protected static InputController 	instance_input;
		protected static GameController 	instance_game;
		
		// -------------------------------------------------------------------------------
		// GetPlayer
		// -------------------------------------------------------------------------------
		public static Player GetPlayer {
			get {
				if (instance_player == null) {
					instance_player = GameObject.FindObjectOfType<Player>();
				}
				return instance_player;
			}
		}
		
		// -------------------------------------------------------------------------------
		// GetInput
		// -------------------------------------------------------------------------------
		public static InputController GetInput {
			get {
				if (instance_input == null) {
					instance_input = GameObject.FindObjectOfType<InputController>();
				}
				return instance_input;
			}
		}		
		
		// -------------------------------------------------------------------------------
		// GetGame
		// -------------------------------------------------------------------------------
		public static GameController GetGame {
			get {
				if (instance_game == null) {
					instance_game = GameObject.FindObjectOfType<GameController>();
				}
				return instance_game;
			}
		}		
		
		// -------------------------------------------------------------------------------
	
	}

}
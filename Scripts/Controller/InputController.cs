// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;

using UnityEngine;

namespace woco_urogue {

	// ===================================================================================
	// INPUT CONTROLLER
	// ===================================================================================
	public class InputController : MonoBehaviour {
	
		public float mouseSensitivity = 1f;

		public  float ForwardBack { get; private set; }
		public  float LeftRight { get; private set; }
		public  float Horizontal { get; private set; }
		public  float Vertical { get; private set; }

		public  bool Weapon { get; private set; }
		public  bool Escape { get; private set; }
		public  bool Inventory { get; private set; }
		public  bool Stats { get; private set; }
		public  bool Skills { get; private set; }
		
		public  bool Item1 { get; private set; }
		public  bool Item2 { get; private set; }
		public  bool Item3 { get; private set; }
		public  bool Item4 { get; private set; }
		public  bool Item5 { get; private set; }
		public  bool Item6 { get; private set; }
		public  bool Item7 { get; private set; }
		public  bool Item8 { get; private set; }
		public  bool Item9 { get; private set; }
	   
		private  bool use;

		public  bool Use {
			get {
				bool b = use;
				use = false;
				return b;
			}
		}

		void Update() {
			ForwardBack = 0;
			LeftRight = 0;
		
			if (Input.GetKey(KeyCode.W)) ForwardBack = 1;
			if (Input.GetKey(KeyCode.S)) ForwardBack = -1;
			if (Input.GetKey(KeyCode.A)) LeftRight = -1;
			if (Input.GetKey(KeyCode.D)) LeftRight = 1;
		
			Horizontal 	= Input.GetAxis("Mouse X") * mouseSensitivity;
			Vertical 	= -Input.GetAxis("Mouse Y") * mouseSensitivity;

			use 		= Input.GetKeyDown(KeyCode.E);
			Weapon 		= Input.GetKey(KeyCode.Mouse0);
			Escape 		= Input.GetKeyDown(KeyCode.Escape);
			Inventory 	= Input.GetKeyDown(KeyCode.I);
			Stats 		= Input.GetKeyDown(KeyCode.C);
 			Skills 		= Input.GetKeyDown(KeyCode.T);
 
			Item1 = Input.GetKeyDown(KeyCode.Alpha1);
			Item2 = Input.GetKeyDown(KeyCode.Alpha2);
			Item3 = Input.GetKeyDown(KeyCode.Alpha3);
			Item4 = Input.GetKeyDown(KeyCode.Alpha4);
			Item5 = Input.GetKeyDown(KeyCode.Alpha5);
			Item6 = Input.GetKeyDown(KeyCode.Alpha6);
			Item7 = Input.GetKeyDown(KeyCode.Alpha7);
			Item8 = Input.GetKeyDown(KeyCode.Alpha8);
			Item9 = Input.GetKeyDown(KeyCode.Alpha9);
		
		}
	}

}
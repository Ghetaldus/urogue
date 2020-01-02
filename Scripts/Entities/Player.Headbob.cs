
// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;

using UnityEngine;

namespace woco_urogue {

	// ===================================================================================
	// PLAYER HEADBOB
	// ===================================================================================
	[System.Serializable]
	public class PlayerHeadbobber {

		[Header("-=- Head Bobbing -=-")]
		public bool activeHeadbob		= true;
		public float transitionSpeed 	= 20f;
		public float bobSpeed 			= 4.8f;
		public float bobAmount 			= 0.5f;
		
		float timer						= Mathf.PI / 2;
		Vector3 restPosition;
		//Vector3 camPos;
		
		protected Camera camera;
		
		// -----------------------------------------------------------------------------------
		// Initalize
		// -----------------------------------------------------------------------------------
		public void Initalize(Camera mainCamera) {
			if (!activeHeadbob) return;
			
			camera = mainCamera;
			restPosition = camera.transform.localPosition;
			
		}

		// -----------------------------------------------------------------------------------
		// Update
		// -----------------------------------------------------------------------------------
		public void Update() {
			
			if (!activeHeadbob) return;
			
			if (Obj.GetInput.LeftRight != 0 || Obj.GetInput.ForwardBack != 0) {

				 timer += bobSpeed * Time.deltaTime;
				 Vector3 newPosition = new Vector3(Mathf.Cos(timer) * bobAmount, restPosition.y + Mathf.Abs((Mathf.Sin(timer) * bobAmount)), restPosition.z);
				 //camPos = newPosition;
				 camera.transform.localPosition = newPosition;
			 } else {
				 timer = Mathf.PI / 2;
 
				 Vector3 newPosition = new Vector3(Mathf.Lerp(camera.transform.localPosition.x, restPosition.x, transitionSpeed * Time.deltaTime), Mathf.Lerp(camera.transform.localPosition.y, restPosition.y, transitionSpeed * Time.deltaTime), Mathf.Lerp(camera.transform.localPosition.z, restPosition.z, transitionSpeed * Time.deltaTime));
				 //camPos = newPosition;
				 camera.transform.localPosition = newPosition;
			 }
 
			 if (timer > Mathf.PI * 2) //completed a full cycle on the unit circle. Reset to 0 to avoid bloated values.
				 timer = 0;

		}
	
		// -----------------------------------------------------------------------------------

	}

}
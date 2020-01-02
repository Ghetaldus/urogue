
// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;

using UnityEngine;

namespace woco_urogue {

	// ===================================================================================
	// PLAYER
	// ===================================================================================
	public class Player : Character, IHitable {

		public InventoryController inventory;
		public ArchetypeController archetype;
	
		public Camera mainCamera;
   
		public WeaponController weaponController;
		public PlayerHeadbobber headbobber 				= new PlayerHeadbobber();
	
		public float interactionRange 				= 4f;
		public float baseRotationSpeed 				= 4f;
	
		public float rotationSpeed 					{ get; private set; }
		public IInteractive InteractiveObject 		{ get; private set; }
	
		// -----------------------------------------------------------------------------------
		// Awake
		// -----------------------------------------------------------------------------------
		protected override void Awake() {
		
			base.Awake();
	
			hitTime = -10;
			rotationSpeed = Mathf.Round(statistics.Speed/2) + baseRotationSpeed;
			
			inventory.Initalize(this);
			skills.Initalize(this);
			archetype.Initalize(this);
		
			inventory.onSetWeapon += weaponController.OnSetWeapon;
		
			headbobber.Initalize(mainCamera);
					
			DontDestroyOnLoad(this.gameObject);
		
		}

		// -----------------------------------------------------------------------------------
		// Update
		// -----------------------------------------------------------------------------------
		protected override void Update() {
	
			if (!Obj.GetGame.ingame) return;
		
			UpdateMovement();
			UpdateObjectInteraction();
			
			inventory.Update();
			
			headbobber.Update();
			
			base.Update();
  
		}
	
		// -----------------------------------------------------------------------------------
		// UpdateMovement
		// -----------------------------------------------------------------------------------
		protected void UpdateMovement() {
			
			
			
			// Movement
			transform.Rotate(0, Obj.GetInput.Horizontal * rotationSpeed * Time.deltaTime, 0);
			Vector3 moveDirection = new Vector3(Obj.GetInput.LeftRight, -1, Obj.GetInput.ForwardBack);
			moveDirection = transform.TransformDirection(moveDirection);
			moveController.Move(moveDirection * statistics.Speed * Time.deltaTime);

			// Camera rotation
			Quaternion targetCameraRotation = mainCamera.transform.localRotation;
			targetCameraRotation *= Quaternion.Euler(Obj.GetInput.Vertical * rotationSpeed * Time.deltaTime, 0f, 0f);
			targetCameraRotation = ClampRotationAroundXAxis(targetCameraRotation);
			mainCamera.transform.localRotation = targetCameraRotation;
	
		}
	
		// -----------------------------------------------------------------------------------
		// UpdateObjectInteraction
		// -----------------------------------------------------------------------------------
		protected void UpdateObjectInteraction() {
	
			InteractiveObject = null;
			RaycastHit hit;
			if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.TransformDirection(Vector3.forward), out hit, interactionRange)) {
				InteractiveObject = hit.collider.gameObject.GetComponent(typeof(IInteractive)) as IInteractive;
				if (InteractiveObject != null && !InteractiveObject.Useable()) InteractiveObject = null;
			}
			if ((InteractiveObject != null) && (Obj.GetInput.Use)) {
				InteractiveObject.Use();
			}
		
		}
	
		// -----------------------------------------------------------------------------------
		// Death
		// -----------------------------------------------------------------------------------
		public override void Death() {
			weaponController.OnSetWeapon(null);
			Obj.GetGame.GameState = GameStates.GameOver;
		}

		// -----------------------------------------------------------------------------------
		// Hit
		// -----------------------------------------------------------------------------------
		public override void Hit(HitInfo hitInfo) {
		
			if (statistics.Health <= 0) return;
		
			DamageResult damageResult = Utl.DamageFormula(hitInfo.damage, resistances.resistances, hitInfo.source, this, hitInfo.baseAmount);
		
			hitInfo.amount = damageResult.amount;
		
			base.Hit(hitInfo);

			inventory.DamageEquipment((int)Mathf.Round(hitInfo.amount * hitInfo.damage.equipmentDamage));

		}

		// -----------------------------------------------------------------------------------
		// ClampRotationAroundXAxis
		// -----------------------------------------------------------------------------------
		private Quaternion ClampRotationAroundXAxis(Quaternion q) {
	
			q.x /= q.w;
			q.y /= q.w;
			q.z /= q.w;
			q.w = 1.0f;

			float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

			angleX = Mathf.Clamp(angleX, -75, 75);

			q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

			return q;
		}
	
		// -----------------------------------------------------------------------------------

	}

}
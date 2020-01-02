// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;

using UnityEngine;
using UnityEngine.Events;

namespace woco_urogue {

	// ===================================================================================
	// ENTITY
	// ===================================================================================
	public abstract class Entity : EntityBase {
	
		[Header("-=- Entity Options -=-")]
		
		public SpriteRenderer spriteRenderer;
		
		public bool faceCamera;
		public bool lockAxisY = false;
   
		public bool shakeOnHit;
		protected float currentIntensity;
		protected float shakeDecay = .01f;
		protected Vector3 originPosition;
		protected Quaternion originRotation;

		public bool floatOnIdle;
		protected float degreesPerSecond = 15.0f;
		protected float amplitude = 0.25f;
		protected float frequency = 0.5f;
	
		protected Vector3 posOffset;
		protected Vector3 tempPos;
		[HideInInspector] public int nextWayPoint;
		
		// -----------------------------------------------------------------------------------
		// Start
		// -----------------------------------------------------------------------------------
		protected virtual void Start () {
		
			if (spriteRenderer != null)
				posOffset = spriteRenderer.transform.localPosition;
		
			
		}
	
		// -----------------------------------------------------------------------------------
		// LateUpdate
		// -----------------------------------------------------------------------------------
		protected void LateUpdate() {
			FaceCamera();
			ContinueShake();
			ContinueFloat();
		}
	
		// -----------------------------------------------------------------------------------
		// FaceCamera
		// -----------------------------------------------------------------------------------
		protected void FaceCamera() {
			if (!faceCamera) return;
	
			Vector3 lookDir = Obj.GetPlayer.transform.position - spriteRenderer.transform.position;
			if (lockAxisY)  lookDir.y = 0;
			spriteRenderer.transform.rotation = Quaternion.LookRotation(lookDir);
		
		}
	
		// -----------------------------------------------------------------------------------
		// StartShake
		// -----------------------------------------------------------------------------------
		protected void ContinueFloat() {
			if (!floatOnIdle) return;
		
			if (!faceCamera)
				spriteRenderer.transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);

			tempPos = posOffset;
			tempPos.y += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency) * amplitude;
		
			spriteRenderer.transform.localPosition = tempPos;
		
		}
	
		// -----------------------------------------------------------------------------------
		// StartShake
		// -----------------------------------------------------------------------------------
		protected void StartShake(float shakeIntensity = .1f) {
			if (shakeOnHit && shakeIntensity > 0) {
				originPosition = transform.position;
				originRotation = transform.rotation;
				currentIntensity = shakeIntensity;
			}
		}
	
		// -----------------------------------------------------------------------------------
		// ContinueShake
		// -----------------------------------------------------------------------------------
		protected void ContinueShake() {
			if (shakeOnHit && currentIntensity > 0){
				transform.position = originPosition + Random.insideUnitSphere * currentIntensity;
				transform.rotation = new Quaternion(
					originRotation.x + Random.Range (-currentIntensity,currentIntensity) * .2f,
					originRotation.y + Random.Range (-currentIntensity,currentIntensity) * .2f,
					originRotation.z + Random.Range (-currentIntensity,currentIntensity) * .2f,
					originRotation.w + Random.Range (-currentIntensity,currentIntensity) * .2f);
				currentIntensity -= shakeDecay;
			}
		}

		// -----------------------------------------------------------------------------------
	
	}

}
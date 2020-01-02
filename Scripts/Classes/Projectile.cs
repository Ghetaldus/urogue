// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;

using UnityEngine;

namespace woco_urogue {

	// ===================================================================================
	// PROJECTILE
	// ===================================================================================
	public class Projectile : Entity {
		
		[Header("-=- Projectile Options -=-")]
		public DamageTemplate damage;
		public float speed = 10;
		
		public GameObject corpse;
		//public HitInfo.HitSources hitSource;
		
		public float destroyAfterSeconds;
		public bool destroyAfterDistance;
		public float attackDistance;
		
		[HideInInspector] public Character owner = null;
		[HideInInspector] public float level;
		protected float travelledDistance;
		
		// -----------------------------------------------------------------------------------
		// Start
		// -----------------------------------------------------------------------------------
		protected override void Start() {
			
			if (destroyAfterSeconds > 0)
				Destroy(gameObject, destroyAfterSeconds);
			
		}
	
		// -----------------------------------------------------------------------------------
		// Update
		// -----------------------------------------------------------------------------------
		void Update() {
			Move();
			CheckHit();
			CheckDeath();
		}
	
		// -----------------------------------------------------------------------------------
		// Move
		// -----------------------------------------------------------------------------------
		protected virtual void Move() {
			travelledDistance += speed * Time.deltaTime;
			transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
		}
	
		// -----------------------------------------------------------------------------------
		// CheckHit
		// -----------------------------------------------------------------------------------
		protected virtual void CheckHit()
		{
			RaycastHit hit;
			if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1f, LayersHelper.DefaultEnemyPlayer)) {
				IHitable hitable = hit.collider.gameObject.GetComponent(typeof(IHitable)) as IHitable;
				if (hitable != null) {
					hitable.Hit(new HitInfo(damage, hit.point, true, owner));
				}
				Death();
			}
		}
	
		// -----------------------------------------------------------------------------------
		// CheckDeath
		// -----------------------------------------------------------------------------------
		protected virtual void CheckDeath() {
			if (destroyAfterDistance) {
				if (travelledDistance >= attackDistance) {
					Death();
				}
			}
		}
	
		// -----------------------------------------------------------------------------------
		// Death
		// -----------------------------------------------------------------------------------
		protected virtual void Death() {
			if (corpse != null) Instantiate(corpse, transform.position, Quaternion.identity);
			Destroy(this.gameObject);
		}

		// -----------------------------------------------------------------------------------

	}

}
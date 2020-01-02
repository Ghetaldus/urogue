// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;

using UnityEngine;

namespace woco_urogue {

	// ===================================================================================
	// WEAPON CONTROLLER
	// ===================================================================================
	public class WeaponController : MonoBehaviour {

		public GameObject weaponHand;
		public GameObject shieldHand;
		
		public DescriptionTemplate outOfAmmo;
		
		private Item weapon;
		private Animator animator;
		private GameObject weaponModel;
		
		protected Character owner;
	
		// -----------------------------------------------------------------------------------
		// Awake
		// -----------------------------------------------------------------------------------
		void Awake() {
			animator = GetComponent<Animator>();
			owner = Obj.GetPlayer;
		}
	
		// -----------------------------------------------------------------------------------
		// Update
		// -----------------------------------------------------------------------------------
		void Update() {
			if (!Obj.GetGame.ingame) return;
			animator.SetBool("IsAttack", Obj.GetInput.Weapon);
		}

		// -----------------------------------------------------------------------------------
		// OnAttack
		// by animation event of players weapon
		// -----------------------------------------------------------------------------------
		public void OnAttack() {
	
			if (weapon == null) return;
			Player player = Obj.GetPlayer;
		
			if (weapon.canUse()) {
		
				// -- Ranged Attack
				if (weapon.template is ItemWeaponRangedTemplate) {
			
					ItemWeaponRangedTemplate rangedWeapon = (ItemWeaponRangedTemplate)weapon.template;
					GameObject go = Instantiate(rangedWeapon.projectile, Obj.GetPlayer.mainCamera.transform.position, Obj.GetPlayer.mainCamera.transform.rotation);
					go.GetComponent<Projectile>().owner = owner;
					go.GetComponent<Projectile>().level = weapon.level;
					weapon.depleteUsage();
		
				// -- Melee Attack
				} else if (weapon.template is ItemWeaponMeleeTemplate) {
		
					RaycastHit hit;
					if (Physics.Raycast(Obj.GetPlayer.mainCamera.transform.position, Obj.GetPlayer.mainCamera.transform.TransformDirection(Vector3.forward), out hit, 4)) {
						IHitable hitable = hit.collider.gameObject.GetComponent(typeof(IHitable)) as IHitable;
				
						if (hitable != null) {
							ItemWeaponMeleeTemplate meleeWeapon = (ItemWeaponMeleeTemplate)weapon.template;
							HitInfo tmp_hit = new HitInfo(meleeWeapon.damage, hit.point, true, player, weapon.level);
							hitable.Hit(tmp_hit);
							weapon.depleteUsage();
						}
					}
		
				 }
			 
			 } else {
				UIController.Instance.ShowError(outOfAmmo);
			}
	   
		}
	
		// -----------------------------------------------------------------------------------
		// OnSetWeapon
		// -----------------------------------------------------------------------------------
		public void OnSetWeapon(Item weapon) {
			this.weapon = weapon;
			animator.SetBool("IsAttack", false);
			if (weaponModel != null) Destroy(weaponModel);
			if (weapon == null) {
				animator.SetInteger("WeaponType", -1);
			} else {
				ItemWeaponTemplate tmpl = (ItemWeaponTemplate)weapon.template;
				weaponModel = (GameObject)Instantiate(tmpl.model);
				weaponModel.transform.parent = weaponHand.transform;
				weaponModel.transform.localPosition = Vector3.zero;
				
				//fhiz: if i remove this and weapon models are not perfectly aligned,
				//      they end up upside down etc.
			    // weaponModel.transform.localRotation = Quaternion.identity;
			    
				weaponModel.transform.localScale = Vector3.one;
				animator.SetInteger("WeaponType", GetAnimationId(tmpl));
			}
		}
	
		// -----------------------------------------------------------------------------------
		// GetAnimationId
		// -----------------------------------------------------------------------------------    
		public static int GetAnimationId(ItemWeaponTemplate item) {
			if (item is ItemWeaponRangedTemplate) return 0;
			if (item is ItemWeaponMeleeTemplate) {
				ItemWeaponMeleeTemplate weapon = (ItemWeaponMeleeTemplate)item;
				return weapon.animationId;
			}
			return 0;
		}
	
		// -----------------------------------------------------------------------------------
	
	}

}
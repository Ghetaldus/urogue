// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;

using UnityEngine;

namespace woco_urogue {

	// ===================================================================================
	// DAMAGE FLOOR
	// ===================================================================================
	[RequireComponent(typeof(BoxCollider))]
	public class DamageFloor : MonoBehaviour {
	
		[Header("-=- Floor Effects -=-")]
		public EffectTemplate enterEffect;
		public EffectTemplate exitEffect;
		public EffectTemplate stayEffect;
		
		[Header("-=- Sounds & Messages -=-")]
		
		public SoundTemplate enterSound;
		public SoundTemplate exitSound;
		
		public DescriptionTemplate enterMessage;
		public DescriptionTemplate exitMessage;
		
		protected float updateInterval;
		protected float updateTime;
		
		// -------------------------------------------------------------------------------
		// Start
		// -------------------------------------------------------------------------------
		void Start() {
			updateInterval = Obj.GetGame.configuration.updateTime;
		}
		
		// -------------------------------------------------------------------------------
		// OnTriggerEnter
		// -------------------------------------------------------------------------------
		void OnTriggerEnter(Collider co) {
			if (enterEffect != null) {
				Character Character = co.GetComponent<Character>();
				if (Character != null) {
					UIController.Instance.ShowError(enterMessage);
					SoundController.Play(enterSound, transform.position);
					enterEffect.Activate(Character);
				}
			}
		}
	
		// -------------------------------------------------------------------------------
		// OnTriggerExit
		// -------------------------------------------------------------------------------
		void OnTriggerExit(Collider co) {
			if (exitEffect != null) {
				Character Character = co.GetComponent<Character>();
				if (Character != null) {
					UIController.Instance.ShowText(exitMessage);
					SoundController.Play(exitSound, transform.position);
					exitEffect.Activate(Character);
				}
			}
		}
	
		// -------------------------------------------------------------------------------
		// OnTriggerStay
		// -------------------------------------------------------------------------------
		void OnTriggerStay(Collider co) {
			if (stayEffect != null) {
				Character Character = co.GetComponent<Character>();
				if (Character != null) {
					if (checkStayTime()) {
						stayEffect.Activate(Character);
						updateTime = Time.time;
					}
				}
			}	
		}
		
		// -------------------------------------------------------------------------------
		// checkStayTime
		// -------------------------------------------------------------------------------
		private bool checkStayTime() {
			return (updateTime <= 0 || (Time.time - updateTime > updateInterval));
	
		}
		// -------------------------------------------------------------------------------

	}

}
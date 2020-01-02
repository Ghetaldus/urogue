// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;
using UnityEngine;


namespace woco_urogue {

	// ===================================================================================
	// TELEPORTER
	// ===================================================================================
	[HelpURL("http://example.com/docs/MyComponent.html")]
	[RequireComponent(typeof(BoxCollider))]
	public class Teleporter : MonoBehaviour {
		
		[Header("-=- Visibility Options -=-")]
		public bool IsVisible;
		public StatusTemplate[] seeStatus;
		public SkillTemplate seeSkill;
		public int seeSkillLevel;
		
		private SpriteRenderer spriterenderer;
					
		public SoundTemplate activateSound;
		public Transform targetTransform;
		
		
		// -------------------------------------------------------------------------------
		// Awake
		// -------------------------------------------------------------------------------
		void Awake () {
			spriterenderer = GetComponent<SpriteRenderer>();
			spriterenderer.enabled = IsVisible;
		}
		
		// -------------------------------------------------------------------------------
		// Update
		// Check if player can see the trap and if trap is visible to the player or not
		// -------------------------------------------------------------------------------
		public void Update () {		
			RaycastHit hit;
			if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Obj.GetPlayer.statistics.Sight, LayersHelper.DefaultEnemyPlayer)) {
				if (hit.collider.gameObject.tag == "Player") {
					if (Obj.GetPlayer.states.getHasStates(seeStatus) ||
						Obj.GetPlayer.skills.getHasSkill(seeSkill, seeSkillLevel) ) {
						IsVisible = true;
						spriterenderer.enabled = IsVisible;
					} else {
						IsVisible = false;
						spriterenderer.enabled = IsVisible;
					}
				}
			}
		}

		// -------------------------------------------------------------------------------
		// OnTriggerEnter
		// -------------------------------------------------------------------------------
		void OnTriggerEnter(Collider co) {
			if (targetTransform != null) {
				Character character = co.GetComponent<Character>();
				if (character != null) {
					character.transform.position = targetTransform.position;
					character.transform.rotation = targetTransform.rotation;
				}
			}
		}

		// -------------------------------------------------------------------------------
		
	}

}
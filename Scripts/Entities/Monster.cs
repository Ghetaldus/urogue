// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace woco_urogue {

	// ===================================================================================
	// MONSTER
	// ===================================================================================
	public class Monster : Character, IHitable {
	
		public enum FSMTypes {
			Dead = 0,
			Idle = 1,
			Move = 2,
			Attack = 3,
			Melee = 4,
			Ranged = 5
		}
	
		public UnityEvent onDeath;

		public SpriteRendererIcon spriteRendererIcon;
		public GameObject bulletInstancePosition;
		
		[Header("-=- Behaviour & Attacks -=-")]
		public MonsterAttackTemplate[] attackTemplates;
		[Range(0,1)]public float moveProbability;
		public float moveCooldown;
	
		[Header("-=- Death & Corpse -=-")]
		public SoundTemplate deathSound;
		public GameObject corpse;
		public float corpseChance;
	
		[Header("-=- Hits -=-")]
		public GameObject hitEffectObject;

		public PropertyModifier[] rewardStatisticBoost;
		public StatusChance[] startStatus;
		
		

		protected FSMTypes FSMType = FSMTypes.Idle;    
		protected float seePlayerTime = -10;
		protected bool prevSeePlayer;
		protected int currentAttack = -1;
		protected bool seePlayer = false;
		protected float distanceToPlayer;
		protected float attackTime;
		protected List<MonsterAttack> attacks;
	
		// -----------------------------------------------------------------------------------
		// 
		// -----------------------------------------------------------------------------------
		protected override void Awake() {
	
			base.Awake();
			
			// -- Initalize starting statusses (stati? statusi? states - states!)
			foreach (StatusChance status in startStatus) {
				states.TryApplyStatus(status.template, status.probability);
			}
			
			// -- Initalize Attacks
			attacks = new List<MonsterAttack>(new MonsterAttack[attackTemplates.Length]);
        	for (int i = 0; i < attacks.Count; ++i) {
    			attacks[i] = new MonsterAttack(attackTemplates[i]);
    		}

		}
	
		// -----------------------------------------------------------------------------------
		// Update
		// -----------------------------------------------------------------------------------
		protected override void Update() {
		
			if (Obj.GetGame.paused) return;
		
			UpdateSight();

			//Hit Effect
			if (Time.time - hitTime < 0.5) {
				spriteRenderer.color = Color.Lerp(Color.red, Color.white, (Time.time - hitTime) * 2);
			}

			//Status Effect Icon
			StatusTemplate tmp_status = states.GetLatestStatus();
			if (tmp_status != null) {
				spriteRendererIcon.SetIcon(tmp_status.icon);
			} else {
				spriteRendererIcon.RemoveIcon();
			}
		
			UpdateBehaviour();
		
			base.Update();

		}

		// -----------------------------------------------------------------------------------
		// Death
		// -----------------------------------------------------------------------------------
		public override void Death() {
			
			SoundController.Play(deathSound, transform.position);
	
			if (corpse != null && (UnityEngine.Random.value <= corpseChance)  ) {
				Instantiate(corpse, transform.position, transform.rotation);
			}

			CancelInvoke();
			onDeath.Invoke();
			Destroy(gameObject);

		}
	
		// -----------------------------------------------------------------------------------
		// UpdateSight
		// -----------------------------------------------------------------------------------
		protected void UpdateSight() {
	
			if (Obj.GetPlayer.statistics.Health > 0) {
			
				RaycastHit hit;
				seePlayer = false;
			
				distanceToPlayer = Vector3.Distance(transform.position, Obj.GetPlayer.transform.position);
				transform.LookAt(Obj.GetPlayer.transform);

				if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, statistics.Sight, LayersHelper.DefaultEnemyPlayer)) {
					if (hit.collider.gameObject.tag == "Player") {
						seePlayerTime = Time.time;
						seePlayer = true;
					}
				}
				
			}
	
		}
	
		// -----------------------------------------------------------------------------------
		// UpdateBehaviour
		// -----------------------------------------------------------------------------------
		protected void UpdateBehaviour() {
		
			int iterations = 0;
		
			// ------------ Select Attack
			if ( FSMType == FSMTypes.Idle && attacks.Count > 0 && Obj.GetPlayer.statistics.Health > 0) {
			
				while (FSMType == FSMTypes.Idle) {
	
					currentAttack = Random.Range(0, attacks.Count);

					if (seePlayer &&
						UnityEngine.Random.value <= attacks[currentAttack].template.attackProbability &&
						attacks[currentAttack].template.manaCost <= statistics.Mana &&
						distanceToPlayer <= attacks[currentAttack].template.attackDistance &&
						Time.time - attacks[currentAttack].currentCooldown > attacks[currentAttack].template.attackCooldown
						) {
					
						FSMType = FSMTypes.Attack;
					}
				
					iterations++;
					if (FSMType != FSMTypes.Attack && iterations > attacks.Count*2) {
						FSMType = FSMTypes.Move;
						currentAttack = -1;
					}
				
				}
			
			}

			// ---------- Activate Attack
			if (FSMType == FSMTypes.Attack) {
			
				// ------------ Pay Attack Costs
				attacks[currentAttack].currentCooldown = Time.time;
				statistics.Mana -= attacks[currentAttack].template.manaCost;
		
				// ------------ Activate Attack
				if (attacks[currentAttack].template is MonsterAttackMeleeTemplate) {
				
					Invoke("ActivateMeleeAttack", attacks[currentAttack].template.attackDelayTime); 
				
				} else if (attacks[currentAttack].template is MonsterAttackRangedTemplate) {
				
					Invoke("ActivateRangedAttack", attacks[currentAttack].template.attackDelayTime); 
		
				}
				
				/* Fhiz: Todo:
					add special and area attacks here later
				*/
			
			// ---------- Activate Movement
			} else if (FSMType == FSMTypes.Move) {

				if (seePlayer &&
					UnityEngine.Random.value <= moveProbability) {
					Invoke("ActivateMovement", moveCooldown);
				} else {
					FSMType = FSMTypes.Idle;
				}

			}

		}

		// -----------------------------------------------------------------------------------
		// UpdateMeleeBehaviour
		// OBSOLETE - VERWENDET JETZT FINITE STATE MACHINE
		// -----------------------------------------------------------------------------------
		protected void UpdateMeleeBehaviour() {
			/*
			transform.LookAt(Obj.GetPlayer.transform);
			if (seePlayer) seePlayerTime = Time.time;
		
			if (seePlayer && distanceToPlayer < attackDistance && Time.time - attackTime > attackCooldown) {
			
				//animator.SetTrigger("IsAttack");
			
				Invoke("OnMeleeAttack", attackDelayTime); 
				attackTime = Time.time;
			} else {
		
				if (Time.time - seePlayerTime < 5 && distanceToPlayer > attackDistance) {
				
					//animator.SetBool("IsWalk", true);
					moveController.Move((transform.forward - transform.up) * statistics.ActualSpeed * Time.deltaTime);
				} else {
					//animator.SetBool("IsWalk", false);
				}
			}
			*/
		}
	
		// -----------------------------------------------------------------------------------
		// UpdateRangedBehaviour
		// OBSOLETE - VERWENDET JETZT FINITE STATE MACHINE
		// -----------------------------------------------------------------------------------
		protected void UpdateRangedBehaviour() {
			/*
			if (prevSeePlayer == false && seePlayer == true)
			{
				attackTime = Time.time;
			}
			if (seePlayer == true && Time.time - attackTime > attackCooldown)
			{
				//animator.SetTrigger("IsAttack");
				attackTime = Time.time;
			}
			prevSeePlayer = seePlayer;
			*/
		}
	
		// -----------------------------------------------------------------------------------
		// ActivateRangedAttack
		// -----------------------------------------------------------------------------------
		protected virtual void ActivateRangedAttack() {
			

			
			var attack = (MonsterAttackRangedTemplate)attacks[currentAttack].template;
			
			//animator.SetTrigger("IsAttack");
			
			GameObject go = Instantiate(attack.projectile, bulletInstancePosition.transform.position, transform.rotation);
			go.GetComponent<Projectile>().owner = this;
			
			FSMType = FSMTypes.Idle;
		
		}
	
		// -----------------------------------------------------------------------------------
		// ActivateMeleeAttack
		// -----------------------------------------------------------------------------------
		public void ActivateMeleeAttack() {
		

			MonsterAttackMeleeTemplate attack = (MonsterAttackMeleeTemplate)attacks[currentAttack].template;
			RaycastHit hit;
		
			//animator.SetTrigger("IsAttack");
		
			if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, attack.attackDistance + 0.5f)) {
				IHitable hitable = hit.collider.gameObject.GetComponent(typeof(IHitable)) as IHitable;
				if (hitable != null) {
					SoundController.Play(attack.attackSound, transform.position);
					hitable.Hit(new HitInfo(attack.damage, hit.point, false, this));
				}
			}
		
			FSMType = FSMTypes.Idle;
		
		}
		
		// -----------------------------------------------------------------------------------
		// ActivateMovement
		// -----------------------------------------------------------------------------------
		protected void ActivateMovement() {
			moveController.Move((transform.forward - transform.up) * statistics.Speed * Time.deltaTime);
			FSMType = FSMTypes.Idle;
		}

		// -----------------------------------------------------------------------------------
		// Hit
		// -----------------------------------------------------------------------------------
		public override void Hit(HitInfo hitInfo) {
			
			DamageResult damageResult = Utl.DamageFormula(hitInfo.damage, resistances.resistances, hitInfo.source, this, hitInfo.baseAmount);
		
    		hitInfo.amount = damageResult.amount;
			
			base.Hit(hitInfo);
			
			StartShake();
			
			if (hitInfo.source is Player)
				Obj.GetPlayer.AdjustProperty(rewardStatisticBoost);
			
			if (hitInfo.isShowEffect && hitEffectObject != null) {
				GameObject hitEffect = Instantiate(hitEffectObject, hitInfo.point, Quaternion.identity) as GameObject;
				Destroy(hitEffect, 3);
			}

		}
	
		// -----------------------------------------------------------------------------------
	
	}

}
// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;
using UnityEngine;

namespace woco_urogue {

	// =======================================================================================
	// ATTACK 
	// =======================================================================================
	public abstract class MonsterAttackTemplate : BaseTemplate {

		public SoundTemplate attackSound;
		
		[Range(0,1)] public float attackProbability = 1;
		public int manaCost;
		public float attackDelayTime = 0;
		public float attackCooldown = 1;
		public float _attackDistance = 1;
		
		[HideInInspector] public float currentCooldown = 0;
		
		public virtual void Activate() { }
		public virtual void Deactivate() { }

		public virtual float attackDistance {
			get { return _attackDistance; }
			set { _attackDistance = value; }
		}
		
		

		public MonsterAttackTemplate() { }

		public override string ToString()
		{
			return name;
		}
		
	}

}
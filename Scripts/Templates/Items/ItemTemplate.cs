// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;
using UnityEngine;

namespace woco_urogue {

	// =======================================================================================
	// ITEM TEMPLATE
	// =======================================================================================
	public abstract class ItemTemplate : BaseTemplate {
		
		[Header("-=- Item Options -=-")]
		public DescriptionTemplate description;
		
		
		public SoundTemplate activateSound;
		public SoundTemplate deactivateSound;

		public Sprite icon;
		
		public float maxDurability 	= 1;
		public float maxCharges 	= 1;
		public float maxLevel		= 0;
	   	
		[Range(0,1)]public float rarity;
		
    	
     	[Header("-=- Carry Modifiers -=-")]
     	public PropertyModifier[] carryModifiers;
		
		public virtual void Use(int level) {}
		public virtual void Activate(Character owner, int level) {}
		public virtual void Deactivate(Character owner, int level) {}		
		public virtual bool canUse(Character owner, float charges, float durability) { return true; }
		public virtual int usageDurability { get; set; }
		public virtual int usageCharges { get; set; }
		public virtual int updateCharges { get; set; }
		public virtual void depleteUsage(Character owner) {}
		public virtual int depleteUpdate(float updateInterval) { return 0; }
		
		
	}
	
	// =======================================================================================

}
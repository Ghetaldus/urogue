// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace woco_urogue {

	// ===================================================================================
	// UTILITIES
	// ===================================================================================
	//[HelpURL("http://example.com/docs/MyComponent.html")]
	public class Utl {
		
		// ===================================================================================
		// 	DAMAGE
		// ===================================================================================

		// -----------------------------------------------------------------------------------
		// DamageFormula
		// -----------------------------------------------------------------------------------
		public static DamageResult DamageFormula(DamageTemplate damage, List<Element> defence, Character source=null, Character target=null, float baseDamage=0) {
			
			DamageResult damageResult = new DamageResult();
			int index;
			float bonus_damage = 0;
			float bonus_probability = 0;
			float element_damage = 0;
			float bonus_critical;
			
			float total_damage = baseDamage;
			
			// -- summarize all bonus damage
			
			if (source != null)
				bonus_damage += source.CalculateProperties(damage.attackModifiers, PropertyTypes.TotalValue);
			
			// -- summarize all base damage and base defence

			foreach (Element element1 in damage.elements) {
				
				element_damage = (element1.TotalValue + bonus_damage) * 4;							// factor ?
				
				// -- randomize damage by variance
				element_damage = randomizeDamage(element_damage, damage.damageVariance);
				
				index = defence.FindIndex(e => e.template == element1.template);
				if (index != -1)
					element_damage -= (defence[index].TotalValue * 2);								// factor ?
				
				total_damage += element_damage;
			
			}
			
			// -- check for critical or glancing hit
			
			if (source != null)
				bonus_probability = source.CalculateProperties(damage.probabilityModifiers, PropertyTypes.TotalValue);
			
			if (Random.value <= damage.criticalProbability + bonus_probability) {
				
				bonus_critical = damage.criticalPower;
				
				if (source != null)
					bonus_critical += source.CalculateProperties(damage.powerModifiers, PropertyTypes.TotalValue);
				
				total_damage *= bonus_critical;
			
				damageResult.type = DamageTypes.Critical;
			}

			//damageResult.type = DamageTypes.Glancing;
			
			// -- Target Dodge???
			
			damageResult.amount = (int)total_damage;
				
			return damageResult;
		}
		
		// -------------------------------------------------------------------------------
		// randomizeDamage
		// -------------------------------------------------------------------------------
		public static float randomizeDamage(float amount, float variance) {
			if (variance == 0) return amount;
			var min = (int)(amount * (1f-variance));
			var max = (int)(amount * (1f+variance));
			return Random.Range(min, max);
		}
		
		
		
		
		
		// ===================================================================================
		// SPAWNING
		// ===================================================================================
		
		
		// -------------------------------------------------------------------------------
		// SpawnItem
		// -------------------------------------------------------------------------------
		public static void SpawnItem(ItemDropProbability tmpl, float probability, Vector3 position, GameObject dropPrefab) {	
		
			if (tmpl != null && Random.value >= probability ) {
				position.x += Random.Range(-1.0f, 1.0f);
				position.z += Random.Range(-1.0f, 1.0f);
				LootDrop drop = ((GameObject) GameObject.Instantiate(dropPrefab, position, Quaternion.identity)).GetComponent<LootDrop>();
				drop.item = tmpl;
			}
		
		}
		
		// -------------------------------------------------------------------------------
		// SpawnItems
		// -------------------------------------------------------------------------------
		public static void SpawnItems(ItemDropProbability[] tmpls, Vector3 position, GameObject dropPrefab) {
		
			if (tmpls.Length <= 0) return;
			foreach (ItemDrop tmpl in tmpls) {
				if (tmpl != null && Random.value >= tmpl.template.rarity ) {
					position.x += Random.Range(-1.0f, 1.0f);
					position.z += Random.Range(-1.0f, 1.0f);
					LootDrop drop = ((GameObject) GameObject.Instantiate(dropPrefab, position, Quaternion.identity)).GetComponent<LootDrop>();
					drop.item = tmpl;
				}
			}
		
		}
		
		// -------------------------------------------------------------------------------
	
	
	}

}
// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;

using UnityEngine;
using UnityEngine.UI;

namespace woco_urogue {

// ===================================================================================
// GAME SCREEN
// ===================================================================================
public class GameScreen : Panel {
    
    public Text useText;
    public Image crosshair;
    public BuffSlot[] buffSlots;
    public ItemSlot[] slots;
    
    
    public ItemSlot weaponSlot;
    public ItemSlot shieldSlot;
    
    public Text errorText;
    public Image screenEffect;
    
    public Color hitEffectColor;
	public Color warpEffectColor;
	
    private float errorShowTime = -1;
    
	// -----------------------------------------------------------------------------------
	// Update
	// -----------------------------------------------------------------------------------
    void Update() {

        if (IsShown == false) return;
        
		UpdateScreenEffect();
		UpdateInteraction();
		UpdateHotkeys();
		UpdateBuffs();
        
        if (Time.time - errorShowTime > 0.5f) errorText.gameObject.SetActive(false);
        
    }
    
   	// -----------------------------------------------------------------------------------
	// UpdateScreenEffect
	// -----------------------------------------------------------------------------------
    protected void UpdateScreenEffect() {
		
		// -- Player HitEffect
        if (Time.time - Obj.GetPlayer.hitTime < 0.5f) {
            screenEffect.gameObject.SetActive(true);
            hitEffectColor.a = Mathf.Lerp(0.5f, 0f, (Time.time - Obj.GetPlayer.hitTime)*2);
            screenEffect.color = hitEffectColor;
        } else {
            screenEffect.gameObject.SetActive(false);
        }
        
        // -- Player WarpEffect
        if (Time.time - Obj.GetPlayer.warpTime < 0.5f) {
            screenEffect.gameObject.SetActive(true);
            warpEffectColor.a = Mathf.Lerp(0.5f, 0f, (Time.time - Obj.GetPlayer.warpTime)*2);
            screenEffect.color = warpEffectColor;
        } else {
            screenEffect.gameObject.SetActive(false);
        }

    
    }
    
    
  	// -----------------------------------------------------------------------------------
	// UpdateHotkeys
	// -----------------------------------------------------------------------------------
    protected void UpdateHotkeys() {
    	
        weaponSlot.SetItem(Obj.GetPlayer.inventory.equipment[(int)EquipmentSlots.WeaponHand].item);
        
        shieldSlot.SetItem(Obj.GetPlayer.inventory.equipment[(int)EquipmentSlots.ShieldHand].item);
        
        /*
        foreach (ItemSlot slot in slots) {
            slot.SetItem(Obj.GetPlayer.inventory.items[slot.id]);
        }
        */
    }
    
 	// -----------------------------------------------------------------------------------
	// UpdateBuffs
	// -----------------------------------------------------------------------------------
	protected void UpdateBuffs() {
   	
        var tmp_states = Obj.GetPlayer.states.states;
        var i = 0;
        foreach (Status tmp_status in tmp_states) {
    		if (tmp_status.isValid) {
    			buffSlots[i].gameObject.SetActive(true);
           		buffSlots[i].SetStatus(tmp_status);
    		} else {
    			buffSlots[i].gameObject.SetActive(false);
    		}
    		if (i == buffSlots.Length)
    			return;
    		i++;
    	}

   }
   
	// -----------------------------------------------------------------------------------
	// UpdateInteraction
	// -----------------------------------------------------------------------------------
	protected void UpdateInteraction() {
	
		Player player = Obj.GetPlayer;

        useText.gameObject.SetActive(Obj.GetPlayer.InteractiveObject != null);
        
        if (Obj.GetPlayer.InteractiveObject != null) {
        	useText.text 		= Obj.GetPlayer.InteractiveObject.getName; 
        }
	
	}

	// -----------------------------------------------------------------------------------
	// 
	// -----------------------------------------------------------------------------------
    public void ShowError(string text) {
        errorText.gameObject.SetActive(true);
        errorText.text = text;
        errorShowTime = Time.time;
    }
    
    // -----------------------------------------------------------------------------------
    
}

}
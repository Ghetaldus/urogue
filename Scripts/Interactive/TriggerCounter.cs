// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;

using UnityEngine;
using UnityEngine.Events;

namespace woco_urogue {

public class TriggerCounter : MonoBehaviour
{
    public int currentValue = 0;
    public int targetValue = 5;

    public UnityEvent action;

    public void Increment()
    {
        currentValue++;
        if (currentValue == targetValue) action.Invoke();
    }

    public void Decrement()
    {
        currentValue++;
        if (currentValue == targetValue) action.Invoke();
    }
}

}
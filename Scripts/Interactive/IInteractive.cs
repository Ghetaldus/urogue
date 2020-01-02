// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;

namespace woco_urogue {

	// ===================================================================================
	// IINTERACTIVE (ALL OBJECTS THAT CAN BE USED BY PRESSING E)
	// ===================================================================================
	public interface IInteractive {
    
    	string getName { get; }
    	string getDescription { get; }
    
    	bool Useable();
    	void Use();
    
	}

}
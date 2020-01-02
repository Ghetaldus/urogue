// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;
using UnityEngine;
using System;
using System.Collections.Generic;

namespace woco_urogue {

	// ===================================================================================
	// STATUS LIBRARY
	//
	// Fhiz:
	// Library class for quick access of scriptable objects throughout the code, without
	// direct access to the object.
	//
	// I wanted to derive all libraries from a base class but was not able to, due to subtle
	// changes in each library
	// ===================================================================================
	public class StatusLibrary {

		static private Dictionary<string, StatusTemplate> Dict;
		static private bool isInstanced = false;
	
		// -------------------------------------------------------------------------------
		// InstantiateLibrary
		// -------------------------------------------------------------------------------
		static private void InstantiateLibrary() {
			if (Dict == null) Dict = new Dictionary<string, StatusTemplate>();
			if (!isInstanced) LoadLibrary();
		}
	
		// -------------------------------------------------------------------------------
		// LoadLibrary
		// -------------------------------------------------------------------------------
		static public void LoadLibrary() {
			if (isInstanced) return;
			isInstanced = true;
			LoadLibraryForce();
		}
	
		// -------------------------------------------------------------------------------
		// LoadLibraryForce
		// -------------------------------------------------------------------------------
		static public void LoadLibraryForce()
		{
			InstantiateLibrary();
			StatusTemplate[] resources = Resources.LoadAll<StatusTemplate>(@"Status");
			foreach (StatusTemplate status in resources) {
				if (!Dict.ContainsKey(status.name)) {
					Dict.Add(status.name, status);
				}
			}
		}
	
		// -------------------------------------------------------------------------------
		// ClearLibrary
		// -------------------------------------------------------------------------------
		static public void ClearLibrary() {
			isInstanced = false;
			Dict.Clear();
		}
	
		// -------------------------------------------------------------------------------
		// GetStatus
		// -------------------------------------------------------------------------------
		static public StatusTemplate Get(string id) {
			InstantiateLibrary();
			StatusTemplate status;
			if (Dict.TryGetValue(id, out status)) {
				return status;
			}
			return null;
		}
		
		// -------------------------------------------------------------------------------
		// Count
		// -------------------------------------------------------------------------------
		static public int Count {
			get { 
				InstantiateLibrary();
				return Dict.Count;
			}
		}

		// -------------------------------------------------------------------------------
  
	}

}
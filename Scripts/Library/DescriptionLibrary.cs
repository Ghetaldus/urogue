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
	// DESCRIPTION LIBRARY
	//
	// Fhiz:
	// Library class for quick access of scriptable objects throughout the code, without
	// direct access to the object.
	//
	// I wanted to derive all libraries from a base class but was not able to, due to subtle
	// changes in each library
	// ===================================================================================
	public class DescriptionLibrary {

		static private Dictionary<string, DescriptionTemplate> Dict;
		static private bool isInstanced = false;
	
		// -------------------------------------------------------------------------------
		// InstantiateLibrary
		// -------------------------------------------------------------------------------
		static private void InstantiateLibrary() {
			if (Dict == null) Dict = new Dictionary<string, DescriptionTemplate>();
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
			DescriptionTemplate[] resources = Resources.LoadAll<DescriptionTemplate>(@"Descriptions");
			foreach (DescriptionTemplate tmpl in resources) {
				if (!Dict.ContainsKey(tmpl.name)) {
					Dict.Add(tmpl.name, tmpl);
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
		// GetName
		// -------------------------------------------------------------------------------
		static public string GetName(string uniqueid) {
			InstantiateLibrary();
			DescriptionTemplate tmpl;
			if (Dict.TryGetValue(uniqueid, out tmpl)) {
				for (int i = 0; i < tmpl.description.Length; i++) {
					if (tmpl.description[i].language == Obj.GetGame.language) {
						return tmpl.description[i].name;
					}
				}
			}
			return uniqueid;
		}

		// -------------------------------------------------------------------------------
		// GetDescription
		// -------------------------------------------------------------------------------
		static public string GetDescription(string uniqueid) {
			InstantiateLibrary();
			DescriptionTemplate tmpl;
			if (Dict.TryGetValue(uniqueid, out tmpl)) {
				for (int i = 0; i < tmpl.description.Length; i++) {
					if (tmpl.description[i].language == Obj.GetGame.language) {
						return tmpl.description[i].description;
					}
				}
			}
			return uniqueid;
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
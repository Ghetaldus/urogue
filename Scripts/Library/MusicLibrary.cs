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
	// MUSIC LIBRARY
	//
	// Fhiz:
	// Library class for quick access of scriptable objects throughout the code, without
	// direct access to the object.
	//
	// I wanted to derive all libraries from a base class but was not able to, due to subtle
	// changes in each library
	// ===================================================================================
	public class MusicLibrary {

		static private Dictionary<string, MusicTemplate> Dict;
		static private bool isInstanced = false;
	
		// -------------------------------------------------------------------------------
		// InstantiateLibrary
		// -------------------------------------------------------------------------------
		static private void InstantiateLibrary() {
			if (Dict == null) Dict = new Dictionary<string, MusicTemplate>();
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
			MusicTemplate[] resources = Resources.LoadAll<MusicTemplate>(@"Music");
			foreach (MusicTemplate tmpl in resources) {
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
		// Gettmpl
		// -------------------------------------------------------------------------------
		static public MusicTemplate Get(string id) {
			InstantiateLibrary();
			MusicTemplate tmpl;
			if (Dict.TryGetValue(id, out tmpl)) {
				return tmpl;
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
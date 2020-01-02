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
	// BASE LIBRARY
	//
	// Fhiz:
	// Library class for quick access of scriptable objects throughout the code, without
	// direct access to the object.
	//
	// I wanted to derive all libraries from a base class but was not able to, due to subtle
	// changes in each library
	//
	// THIS ONE IS JUST A PLACEHOLDER TO CREATE FUTURE LIBRARY FILES (would be better if
	// all libraries could be derived from this one)
	// ===================================================================================
	public abstract class BaseLibrary {

		static public string folderName;
		static protected Dictionary<string, BaseTemplate> Dict;
		static protected bool isInstanced = false;
		
		// -------------------------------------------------------------------------------
		// InstantiateLibrary
		// -------------------------------------------------------------------------------
		
		static protected void InstantiateLibrary() {
			if (Dict == null) Dict = new Dictionary<string, BaseTemplate>();
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
			Debug.Log(typeof(BaseLibrary).Name);
			BaseTemplate[] resources = Resources.LoadAll<BaseTemplate>(@folderName);
			foreach (BaseTemplate status in resources) {
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
		// GetStringValue
		// -------------------------------------------------------------------------------
		static public string GetStringValue(string tmplName, string fieldName) {
			InstantiateLibrary();
			BaseTemplate tmpl;
			if (Dict.TryGetValue(tmplName, out tmpl)) {
				return (string)tmpl.GetType().GetField(fieldName).GetValue(tmpl);
			}
			return "";
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
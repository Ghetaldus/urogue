// =======================================================================================
// UROGUE - FIRST PERSON DUNGEON CRAWLER TOOLKIT (Copyright by wovencode.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using woco_urogue;
using UnityEngine;
using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace woco_urogue {

	// ===================================================================================
	// CHARACTER CLASS LIBRARY
	//
	// Fhiz:
	// Library class for quick access of scriptable objects throughout the code, without
	// direct access to the object.
	//
	// I wanted to derive all libraries from a base class but was not able to, due to subtle
	// changes in each library
	// ===================================================================================
	public class CharacterClassLibrary {
		
		static protected Dictionary<string, CharacterClassTemplate> Dict;
		static protected bool isInstanced = false;
		
		// -------------------------------------------------------------------------------
		// InstantiateLibrary
		// -------------------------------------------------------------------------------
		static protected void InstantiateLibrary() {
			if (Dict == null) Dict = new Dictionary<string, CharacterClassTemplate>();
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
		static public void LoadLibraryForce() {
			InstantiateLibrary();
			CharacterClassTemplate[] resources = Resources.LoadAll<CharacterClassTemplate>(@"Classes");
			foreach (CharacterClassTemplate tmpl in resources) {
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
		// GetText
		// -------------------------------------------------------------------------------
		static public string GetText(string fieldName) {
			if (fieldName[0].ToString() != "_") return "["+fieldName+"]";
			InstantiateLibrary();
			CharacterClassTemplate tmpl;
			if (Dict.TryGetValue(Obj.GetGame.language.name, out tmpl)) {
				string str = (string)tmpl.GetType().GetField(fieldName).GetValue(tmpl);
				if (!string.IsNullOrEmpty(str)) return str;
			}
			return "["+fieldName+"]";
		}
		
		// -------------------------------------------------------------------------------
		// GetLanguage
		// -------------------------------------------------------------------------------
		static public CharacterClassTemplate GetTemplate(string tmplName) {
			InstantiateLibrary();
			CharacterClassTemplate tmpl;
			Dict.TryGetValue(tmplName, out tmpl);
			return tmpl;
		}
		
		// -------------------------------------------------------------------------------
		// GetAllTemplates
		// -------------------------------------------------------------------------------
		static public List<CharacterClassTemplate> GetAllTemplates() {
			InstantiateLibrary();
			CharacterClassTemplate[] tmpl = new CharacterClassTemplate[Dict.Count];
			Dict.Values.CopyTo(tmpl, 0);
			return tmpl.ToList();
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityDatabase : MonoBehaviour {

	private static Dictionary<int, AbilityData> abilityDatabase;
	private static bool isLoaded = false;

	static void LoadAbilityDatabase () {

		abilityDatabase = new Dictionary<int, AbilityData> ();

		// Load from Asset Bundle
		//AbilityData [] abilities = AssetManager.LoadBundle ("abilities").LoadAllAssets <AbilityData> ();

		// Load from Resources
		AbilityData [] abilities = Resources.LoadAll<AbilityData> ("Abilities");

		// Add each inventory item to the database
		for (int i = 0; i < abilities.Length; i++) {
			abilityDatabase.Add (abilities [i].abilityID, abilities [i]);
		}

		isLoaded = true;
	}

	public static AbilityData GetAbility (int abilityID) {

		if (isLoaded == false) {
			LoadAbilityDatabase ();
		}

		if (abilityDatabase.ContainsKey (abilityID))
			return abilityDatabase [abilityID];

		Debug.Log ("No ability was found in the Ability Database with abilityID: " + abilityID);
		return null;
	}
}

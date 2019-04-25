using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSetupManager : MonoBehaviour {

	[SerializeField] private string characterName;
	[SerializeField] private int level;
	[SerializeField] private int[] equipment;

	private void Start () {
		LoadCharacterData ();
	}


	private void LoadCharacterData () {

		SGD_Character characterData = SavedGameManager.instance.LoadCharacterData ();

		if (characterData != null) {

			// Set the SGD_Character object in SavedGameManager to the one loaded in
			SavedGameManager.instance.characterData = characterData;
		}

		else {

			// Use the default SGD_Character object created in SavedGameManager
			characterData = SavedGameManager.instance.characterData;
		}

		// If no characterData is found, it will use the default values set in SGD_Character.SetDefaultValues ();

		// LOAD DATA - Set loaded in (or default) character data in CharacterManager
		CharacterManager.instance.playerName = characterData.playerName;
		CharacterManager.instance.level = characterData.level;
		GUIManager.instance.characterMenu.SetLoadedInventory (characterData.inventory);
		GUIManager.instance.characterMenu.SetLoadedEquipment (characterData.equipment);

		// DISPLAY DATA - Update displays after data is loaded
		GUIManager.instance.playerUnitFrame.SetPlayerName ();
		GUIManager.instance.playerUnitFrame.SetPlayerLevel ();




		// SAVE DATA - Save values initally
		characterData.playerName = CharacterManager.instance.playerName;
		characterData.level = CharacterManager.instance.level;

		SavedGameManager.instance.SaveCharacterData ();

	}
}

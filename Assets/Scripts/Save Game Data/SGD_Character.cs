using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SGD_Character {

	private string fileName;

	// Save data
	public string playerName;
	public int level;
	public int [] equipment;
	public int [] inventory;

	public SGD_Character (string _fileName) {
		fileName = _fileName;

		equipment = new int [8];
		inventory = new int [36];

		SetDefaultValues ();
	}

	private void SetDefaultValues () {
		playerName = "Default Name";
		level = 1;

		for (int i = 0; i < inventory.Length; i++) {
			inventory [i] = -1;
		}

		for (int i = 0; i < equipment.Length; i++) {
			equipment [i] = -1;
		}
	}

	public string GetFileName () {
		return fileName;
	}

	public void SaveEquipmentItem (int itemID, int slotIndex) {
		equipment [slotIndex] = itemID;
		SavedGameManager.instance.SaveCharacterData ();
	}

	public void SaveInventory (InventoryItem[] currentInventory) {
		for (int i = 0; i < currentInventory.Length; i++) {
			if (currentInventory[i].sourceData == null) {
				inventory [i] = -1;
			} else { 
				inventory [i] = currentInventory [i].sourceData.itemID;
			} 
		}
		SavedGameManager.instance.SaveCharacterData ();
	}
}

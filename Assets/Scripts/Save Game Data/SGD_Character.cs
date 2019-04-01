using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SGD_Character {

	private string fileName;

	// Save data
	public int [] equipment;
	public int [] inventory;

	public SGD_Character (string _fileName) {
		fileName = _fileName;

		equipment = new int [8];
		inventory = new int [36];
	}

	public string GetFileName () {
		return fileName;
	}

	public void SaveEquipmentItem (int itemID, int slotIndex) {
		Debug.Log ("Saving item: " + itemID + " to slot: " + slotIndex);
		equipment [slotIndex] = itemID;
	}

	public void SaveInventoryItem (int itemID, int inventoryIndex) {
		inventory [inventoryIndex] = itemID;
	}
}

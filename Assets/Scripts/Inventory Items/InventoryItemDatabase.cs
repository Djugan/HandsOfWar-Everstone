using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class InventoryItemDatabase : MonoBehaviour {

	private static Dictionary<int, InventoryItemData> itemDatabase;
	private static Dictionary<int, ItemSet> itemSetDatabase;

	void Awake () {
		LoadItemDatabase ();
	}

	static void LoadItemDatabase () {

		itemDatabase = new Dictionary<int, InventoryItemData> ();

		// Load from Asset Bundle
		//InventoryItemData [] items = AssetManager.LoadBundle ("inventoryitems").LoadAllAssets <InventoryItemData> ();

		// Load from Resources
		InventoryItemData [] items = Resources.LoadAll<InventoryItemData> ("Inventory Items");

		// Add each inventory item to the database
		for (int i = 0; i < items.Length; i++) {
			itemDatabase.Add (items [i].itemID, items [i]);
		}
	}

	static void LoadItemSetDatabase () {

		itemSetDatabase = new Dictionary<int, ItemSet> ();

		// Load from Asset Bundle
		//ItemSet [] items = AssetManager.LoadBundle ("inventoryitems").LoadAllAssets <ItemSet> ();

		// Load from Resources
		ItemSet [] items = Resources.LoadAll<ItemSet> ("Item Sets");


		// Add each inventory item to the database
		for (int i = 0; i < items.Length; i++) {
			itemSetDatabase.Add (items [i].setID, items [i]);
		}
	}


	public static InventoryItemData GetItem (int itemID) {
		if (itemDatabase.ContainsKey (itemID))
			return itemDatabase [itemID];

		Debug.Log ("No item was found in the Inventory Item Database with itemID: " + itemID);
		return null;
	}

	public static ItemSet GetItemSet (int setID) {
		if (itemDatabase.ContainsKey (setID))
			return itemSetDatabase [setID];

		Debug.Log ("No item was found in the Item Set Database with setID: " + setID);
		return null;
	}
}
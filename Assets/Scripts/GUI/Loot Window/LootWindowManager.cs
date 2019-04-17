using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootWindowManager : MonoBehaviour {

	[SerializeField] private GameObject mainWindow;
	[SerializeField] private LootItemSlot [] lootItemSlots;

	public void ShowWindow () {
		mainWindow.SetActive (true);
	}
	public void HideWindow () {
		mainWindow.SetActive (false);
	}

	public void SetLoot (int gold, int healthPot, int energyPot, List<InventoryItemData> items) {

		int slotIndex = 0;

		if (gold > 0) {
			lootItemSlots [slotIndex].SetGold (gold);
			slotIndex++;
		}
		if (healthPot > 0) {
			lootItemSlots [slotIndex].SetHealthPotions (healthPot);
			slotIndex++;
		}
		if (energyPot > 0) {
			lootItemSlots [slotIndex].SetEnergyPotions (energyPot);
			slotIndex++;
		}

		for (int i = 0; i < items.Count; i++) {
			lootItemSlots [slotIndex].SetItem (items [i]);
			slotIndex++;
		}

		ShowWindow ();
	}
}

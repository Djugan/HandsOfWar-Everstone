using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootWindowManager : MonoBehaviour {

	[SerializeField] private GameObject mainWindow;
	[SerializeField] private LootItemSlot [] lootItemSlots;

	[HideInInspector] public EnemyNPCManager enemyBeingLooted;

	public void ShowWindow () {
		mainWindow.SetActive (true);
	}
	public void HideWindow () {
		mainWindow.SetActive (false);
	}

	public void SetLoot (EnemyNPCManager enemy) {

		enemyBeingLooted = enemy;

		int slotIndex = 0;

		if (enemy.goldDropped > 0) {
			lootItemSlots [slotIndex].SetGold (enemy.goldDropped);
			slotIndex++;
		}
		if (enemy.healthPotionsDropped > 0) {
			lootItemSlots [slotIndex].SetHealthPotions (1);
			slotIndex++;
		}
		if (enemy.energyPotionsDropped > 0) {
			lootItemSlots [slotIndex].SetEnergyPotions (1);
			slotIndex++;
		}

		for (int i = 0; i < enemy.itemsDropped.Count; i++) {
			lootItemSlots [slotIndex].SetItem (enemy.itemsDropped [i]);
			slotIndex++;

			// In the rare case where we run out of loot item slots
			if (slotIndex == lootItemSlots.Length - 1)
				break;
		}

		// Hide the unused loot item slots
		for (int i = slotIndex; i < lootItemSlots.Length; i++) {
			lootItemSlots [i].HideSlot ();
		}

		ShowWindow ();
	}
}

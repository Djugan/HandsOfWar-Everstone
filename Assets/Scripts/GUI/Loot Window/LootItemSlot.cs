using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LootItemSlot : MonoBehaviour {

	[SerializeField] private GameObject itemSlot_GO;
	[SerializeField] private Image itemIcon_Img;
	[SerializeField] private TextMeshProUGUI itemName_Txt;

	private int gold, healthPotions, energyPotions;
	private InventoryItemData itemData;

	public void HideSlot () {
		itemSlot_GO.SetActive (false);
	}
	public void ShowSlot () {
		itemSlot_GO.SetActive (true);
	}

	public void SetGold (int goldValue) {
		itemIcon_Img.sprite = SharedGraphics.instance.goldIcon;
		itemName_Txt.text = goldValue + " Gold";
		itemName_Txt.color = HandsOfWarColors.white;

		ClearItems ();
		gold = goldValue;
	}

	public void SetHealthPotions (int amount) {
		itemIcon_Img.sprite = SharedGraphics.instance.healthPotionIcon;
		itemName_Txt.text = "Health Potion";
		itemName_Txt.color = HandsOfWarColors.white;

		ClearItems ();
		healthPotions = amount;
	}

	public void SetEnergyPotions (int amount) {
		itemIcon_Img.sprite = SharedGraphics.instance.energyPotionIcon;
		itemName_Txt.text = "Energy Potion";
		itemName_Txt.color = HandsOfWarColors.white;

		ClearItems ();
		energyPotions = amount;
	}

	public void SetItem (InventoryItemData item) {
		itemIcon_Img.sprite = item.icon;
		itemName_Txt.text = item.itemName;

		if (item.isEquippable) {
			if (item.rarity == Rarity.Common)
				itemName_Txt.color = HandsOfWarColors.green;
			else if (item.rarity == Rarity.Rare)
				itemName_Txt.color = HandsOfWarColors.blue;
			else if (item.rarity == Rarity.Epic)
				itemName_Txt.color = HandsOfWarColors.purple;
		}
		else {
			if (item.isQuestItem)
				itemName_Txt.color = HandsOfWarColors.yellow;
			else
				itemName_Txt.color = HandsOfWarColors.white;
		}

		ClearItems ();
		itemData = item;
	}

	public void LootItem () {

	}

	void ClearItems () {
		gold = 0;
		healthPotions = 0;
		energyPotions = 0;
		itemData = null;
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class LootItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	[SerializeField] private GameObject itemSlot_GO;
	[SerializeField] private Image itemIconBackground_Img;
	[SerializeField] private Image itemIcon_Img;
	[SerializeField] private TextMeshProUGUI itemName_Txt;
	[SerializeField] private RectTransform trans;

	private int gold, healthPotions, energyPotions;
	private InventoryItemData itemData;


	public void HideSlot () {
		itemSlot_GO.SetActive (false);
	}
	public void ShowSlot () {
		itemSlot_GO.SetActive (true);
	}

	public void OnPointerEnter (PointerEventData eventData) {
		if (itemData != null) {
			GUIManager.instance.itemStatsWindow.ShowWindow (itemData, trans);
		}
	}

	public void OnPointerExit (PointerEventData eventData) {
		GUIManager.instance.itemStatsWindow.HideWindow ();
	}

	public void SetGold (int goldValue) {
		itemIcon_Img.sprite = SharedGraphics.instance.goldIcon;
		itemIconBackground_Img.sprite = SharedGraphics.instance.inventoryItem_Empty;
		itemName_Txt.text = goldValue + " Gold";
		itemName_Txt.color = HandsOfWarColors.white;

		ClearItems ();
		gold = goldValue;
		ShowSlot ();
	}

	public void SetHealthPotions (int amount) {
		itemIcon_Img.sprite = SharedGraphics.instance.healthPotionIcon;
		itemIconBackground_Img.sprite = SharedGraphics.instance.inventoryItem_Empty;
		itemName_Txt.text = "Health Potion";
		itemName_Txt.color = HandsOfWarColors.white;

		ClearItems ();
		healthPotions = amount;
		ShowSlot ();
	}

	public void SetEnergyPotions (int amount) {
		itemIcon_Img.sprite = SharedGraphics.instance.energyPotionIcon;
		itemIconBackground_Img.sprite = SharedGraphics.instance.inventoryItem_Empty;
		itemName_Txt.text = "Energy Potion";
		itemName_Txt.color = HandsOfWarColors.white;


		ClearItems ();
		energyPotions = amount;
		ShowSlot ();
	}

	public void SetItem (InventoryItemData item) {
		itemIcon_Img.sprite = item.icon;
		itemName_Txt.text = item.itemName;

		if (item.isEquippable) {
			if (item.rarity == Rarity.Common) {
				itemName_Txt.color = HandsOfWarColors.green;
				itemIconBackground_Img.sprite = SharedGraphics.instance.inventoryItem_Common;
			}
			else if (item.rarity == Rarity.Rare) {
				itemName_Txt.color = HandsOfWarColors.blue;
				itemIconBackground_Img.sprite = SharedGraphics.instance.inventoryItem_Rare;
			}
			else if (item.rarity == Rarity.Epic) {
				itemName_Txt.color = HandsOfWarColors.purple;
				itemIconBackground_Img.sprite = SharedGraphics.instance.inventoryItem_Epic;
			}
			else {
				if (item.isQuestItem) {
					itemName_Txt.color = HandsOfWarColors.yellow;
					itemIconBackground_Img.sprite = SharedGraphics.instance.inventoryItem_Quest;
				}
				else {
					itemName_Txt.color = HandsOfWarColors.white;
					itemIconBackground_Img.sprite = SharedGraphics.instance.inventoryItem_Basic;
				}
			}

			ClearItems ();
			itemData = item;
			ShowSlot ();
		}
	}

	public void LootItem () {

		// Determine what item is being looted
		if (gold > 0) {
			GUIManager.instance.characterMenu.gold += gold;
			GUIManager.instance.lootWindow.enemyBeingLooted.goldDropped = 0;
		}else if (healthPotions > 0) {
			GUIManager.instance.characterMenu.healthPotions += gold;
			GUIManager.instance.lootWindow.enemyBeingLooted.healthPotionsDropped = 0;
		}
		else if (energyPotions > 0) {
			GUIManager.instance.characterMenu.energyPotions += gold;
			GUIManager.instance.lootWindow.enemyBeingLooted.energyPotionsDropped = 0;
		}
		else if (itemData != null) {
			GUIManager.instance.characterMenu.AddItemToInventory (itemData.itemID);
		}
		HideSlot ();

	}

	void ClearItems () {
		gold = 0;
		healthPotions = 0;
		energyPotions = 0;
		itemData = null;
	}

}

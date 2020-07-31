using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Reflection;
using UnityEngine.EventSystems;

public class QuestMenuLoot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

	[SerializeField] private GameObject rewardSlot;
	[SerializeField] private Image itemIconBackground_Img;
	[SerializeField] private Image itemIcon_Img;
	[SerializeField] private TextMeshProUGUI itemName_Txt;
	[SerializeField] private RectTransform trans;

	private int gold, exp;
	private InventoryItemData itemData;

	public void HideSlot()
	{
		rewardSlot.SetActive(false);
	}
	public void ShowSlot()
	{
		rewardSlot.SetActive(true);
	}
	public bool IsVisible()
	{
		return rewardSlot.activeInHierarchy;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (itemData != null)
		{
			GUIManager.instance.itemStatsWindow.ShowWindow(itemData, trans);
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		GUIManager.instance.itemStatsWindow.HideWindow();
	}

	public void SetGold(QuestData quest)
	{
		int goldValue = quest.goldReward;
		itemIcon_Img.sprite = SharedGraphics.instance.goldIcon;
		itemIconBackground_Img.sprite = SharedGraphics.instance.inventoryItem_Empty;
		itemName_Txt.text = goldValue + " Gold";
		itemName_Txt.color = HandsOfWarColors.white;

		//ClearItems();
		gold = goldValue;
		ShowSlot();
	}

	public void SetExp(QuestData quest)
	{
		int expValue = quest.expReward;
		itemIcon_Img.sprite = SharedGraphics.instance.goldIcon;
		itemIconBackground_Img.sprite = SharedGraphics.instance.inventoryItem_Empty;
		itemName_Txt.text = expValue + " XP";
		itemName_Txt.color = HandsOfWarColors.white;

		//ClearItems();
		exp = expValue;
		ShowSlot();
	}

	public void SetItem(QuestData quest, int index)
	{
		
		itemIcon_Img.sprite = quest.guaranteedRewards[index].icon;
		itemName_Txt.text = quest.guaranteedRewards[index].itemName;

			if (quest.guaranteedRewards[index].isEquippable)
			{
				if (quest.guaranteedRewards[index].rarity == Rarity.Common)
				{
					itemName_Txt.color = HandsOfWarColors.green;
					itemIconBackground_Img.sprite = SharedGraphics.instance.inventoryItem_Common;
				}
				else if (quest.guaranteedRewards[index].rarity == Rarity.Rare)
				{
					itemName_Txt.color = HandsOfWarColors.blue;
					itemIconBackground_Img.sprite = SharedGraphics.instance.inventoryItem_Rare;
				}
				else if (quest.guaranteedRewards[index].rarity == Rarity.Epic)
				{
					itemName_Txt.color = HandsOfWarColors.purple;
					itemIconBackground_Img.sprite = SharedGraphics.instance.inventoryItem_Epic;
				}
				else
				{
					if (quest.guaranteedRewards[index].isQuestItem)
					{
						itemName_Txt.color = HandsOfWarColors.yellow;
						itemIconBackground_Img.sprite = SharedGraphics.instance.inventoryItem_Quest;
					}
					else
					{
						itemName_Txt.color = HandsOfWarColors.white;
						itemIconBackground_Img.sprite = SharedGraphics.instance.inventoryItem_Basic;
					}
				}

				//ClearItems();
				itemData = quest.guaranteedRewards[index];
				ShowSlot();
			
		}
	}

	public void SetSelectedItem(QuestData quest, int index)
	{
		itemIcon_Img.sprite = quest.selectedRewards[index].icon;
		itemName_Txt.text = quest.selectedRewards[index].itemName;

		if (quest.selectedRewards[index].isEquippable)
		{
			if (quest.selectedRewards[index].rarity == Rarity.Common)
			{
				itemName_Txt.color = HandsOfWarColors.green;
				itemIconBackground_Img.sprite = SharedGraphics.instance.inventoryItem_Common;
			}
			else if (quest.selectedRewards[index].rarity == Rarity.Rare)
			{
				itemName_Txt.color = HandsOfWarColors.blue;
				itemIconBackground_Img.sprite = SharedGraphics.instance.inventoryItem_Rare;
			}
			else if (quest.selectedRewards[index].rarity == Rarity.Epic)
			{
				itemName_Txt.color = HandsOfWarColors.purple;
				itemIconBackground_Img.sprite = SharedGraphics.instance.inventoryItem_Epic;
			}
			else
			{
				if (quest.selectedRewards[index].isQuestItem)
				{
					itemName_Txt.color = HandsOfWarColors.yellow;
					itemIconBackground_Img.sprite = SharedGraphics.instance.inventoryItem_Quest;
				}
				else
				{
					itemName_Txt.color = HandsOfWarColors.white;
					itemIconBackground_Img.sprite = SharedGraphics.instance.inventoryItem_Basic;
				}
			}

			//ClearItems();
			itemData = quest.selectedRewards[index];
			ShowSlot();

		}
	}


	void ClearItems()
	{
		gold = 0;
		exp = 0;
		itemData = null;
	}
}

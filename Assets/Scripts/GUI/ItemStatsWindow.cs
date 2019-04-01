using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemStatsWindow : MonoBehaviour {

	[SerializeField] private GameObject mainWindow;
	[SerializeField] private TextMeshProUGUI itemTitle_Txt;
	[SerializeField] private GameObject itemHeader_GO;
	[SerializeField] private TextMeshProUGUI itemHeader_Txt;
	[SerializeField] private TextMeshProUGUI itemStats_Txt;
	[SerializeField] private GameObject flavorText_GO;
	[SerializeField] private TextMeshProUGUI flavorText_Txt;
	[SerializeField] private GameObject itemSet_GO;
	[SerializeField] private TextMeshProUGUI itemSet_Txt;
	[SerializeField] private RectTransform trans;

	private InventoryItemData sourceData;

	public void ShowWindow (InventoryItemData _sourceData, RectTransform _trans) {

		sourceData = _sourceData;

		// Item Title
		string title = sourceData.itemName;

		if		(title.Length < 10) 	title += "\t\t\t";
		else if (title.Length < 14)		title += "\t\t";
		else if (title.Length < 18)		title += "\t";

		itemTitle_Txt.text = GetTitleColorTag () + title;
		

		// Item Stats
		if (sourceData.isEquippable) {
			itemHeader_GO.SetActive (true);
			itemHeader_Txt.text = GetItemHeaderText ();
			itemStats_Txt.text = GetFormattedStatsText ();
		} else {
			itemHeader_GO.SetActive (false);
		}

		// Flavor Text
		if (sourceData.flavorText == null) {
			flavorText_GO.SetActive (false);
		} else {
			flavorText_GO.SetActive (true);
			flavorText_Txt.text = GetFlavorTextColorTag () + sourceData.flavorText;
		}

		// Item Sets
		if (sourceData.itemSet == null) {
			itemSet_GO.SetActive (false);
		} else {
			itemSet_GO.SetActive (true);
			itemSet_Txt.text = GetItemSetText ();
		}
		
		SetLocation (_trans);

		mainWindow.SetActive (true);
	}
	public void HideWindow () {
		mainWindow.SetActive (false);
	}

	public void SetLocation (RectTransform _trans) {
		float xOffset = 25f;
		float yOffset = 35f;
		Vector3 position = new Vector3 (_trans.position.x + xOffset, _trans.position.y + yOffset, 0f);

		trans.position = position;
	}

	#region Item Header
	string GetItemHeaderText () {

		string itemStats = "";

		// Item Level
		itemStats += GetItemLevelColorTag () + "Item Level: " + sourceData.itemLevel + "\n";

		// Slot
		itemStats += GetItemSlotColorTag () + GetSlotString () + "\n";

		// Weapons and Shields
		itemStats += GetWeaponColorTag ();

		if (sourceData.slot == Slot.Weapon1H || sourceData.slot == Slot.Weapon2H) {
			itemStats += "Damage: " + sourceData.damage + "\t\tDelay: " + sourceData.delay + "\n";

			float attacksPerSecond = 30f / sourceData.delay;
			itemStats += "DPS: " + (attacksPerSecond * sourceData.damage).ToString ("F2") + "\n";
		}
		else if (sourceData.slot == Slot.OffHand) {
			itemStats += "Block Value: " + sourceData.block;
		}

		// Armor values
		itemStats += GetArmorColorTag ();

		if (sourceData.armor > 0)
			itemStats += sourceData.armor + " Armor \n";
		if (sourceData.magicResist > 0)
			itemStats += sourceData.magicResist + " Magic Resist \n";

		return itemStats;
	}
	#endregion

	#region Item Stats
	string GetFormattedStatsText () {

		string itemStats = GetStatsColorTag ();

		// Primary Stats
		if (sourceData.strength > 0)
			itemStats += " +" + sourceData.strength + " Strength \n";
		if (sourceData.agility > 0)
			itemStats += " +" + sourceData.agility + " Agility \n";
		if (sourceData.intelligence > 0)
			itemStats += " +" + sourceData.intelligence + " Intellect \n";

		// Secondary Stats
		if (sourceData.stamina > 0)
			itemStats += " +" + sourceData.stamina + " Stamina \n";
		if (sourceData.dexterity > 0)
			itemStats += " +" + sourceData.dexterity + " Dexterity \n";
		if (sourceData.focus > 0)
			itemStats += " +" + sourceData.focus + " Focus \n";
		if (sourceData.haste > 0)
			itemStats += " +" + sourceData.haste + " Haste \n";
		if (sourceData.speed > 0)
			itemStats += " +" + sourceData.speed + " Move Speed \n";

		return itemStats;

	}
	#endregion

	#region Item Sets
	string GetItemSetText () {

		string itemSetText = GetItemSetTitleColorTag ();
		int numberOfEquippedPieces = 0;

		itemSetText += sourceData.itemSet.setName + " (" + numberOfEquippedPieces + "/5)\n";

		// List of items in set
		for (int i = 0; i < sourceData.itemSet.setItems.Length; i++) {

			bool isItemEquipped = GUIManager.instance.characterMenu.IsItemEquipped (sourceData.itemSet.setItems [i]);

			if (isItemEquipped) {
				itemSetText += GetEarnedSetBonusColorTag ();
				numberOfEquippedPieces++;
			}
			else {
				itemSetText += GetUnearnedSetBonusColorTag ();
			}

			itemSetText += "  " + sourceData.itemSet.setItems [i].itemName + "\n";
		}

		// 3 Set Bonus
		if (numberOfEquippedPieces == 3)	itemSetText += GetEarnedSetBonusColorTag ();
		else								itemSetText += GetUnearnedSetBonusColorTag ();

		itemSetText += "  (3)";

		if (sourceData.itemSet.armor3P > 0)
			itemSetText += "\t+" + sourceData.itemSet.armor3P + " Armor \n";
		if (sourceData.itemSet.magicResist3P > 0)
			itemSetText += "\t+" + sourceData.itemSet.magicResist3P + " Magic Resist \n";

		if (sourceData.itemSet.strength3P > 0)
			itemSetText += "\t+" + sourceData.itemSet.strength3P + " Strength \n";
		if (sourceData.itemSet.agility3P > 0)
			itemSetText += "\t+" + sourceData.itemSet.agility3P + " Agility \n";
		if (sourceData.itemSet.intelligence3P > 0)
			itemSetText += "\t+" + sourceData.itemSet.intelligence3P + " Intellect \n";

		if (sourceData.itemSet.stamina3P > 0)
			itemSetText += "\t+" + sourceData.itemSet.stamina3P + " Stamina \n";
		if (sourceData.itemSet.dexterity3P > 0)
			itemSetText += "\t+" + sourceData.itemSet.dexterity3P + " Dexterity \n";
		if (sourceData.itemSet.focus3P > 0)
			itemSetText += "\t+" + sourceData.itemSet.focus3P + " Focus \n";
		if (sourceData.itemSet.haste3P > 0)
			itemSetText += "\t+" + sourceData.itemSet.haste3P + " Haste \n";

		// 4 Set Bonus
		if (numberOfEquippedPieces == 4)	itemSetText += GetEarnedSetBonusColorTag ();
		else								itemSetText += GetUnearnedSetBonusColorTag ();
		itemSetText += "  (4)";

		if (sourceData.itemSet.armor4P > 0)
			itemSetText += "\t+" + sourceData.itemSet.armor4P + " Armor \n";
		if (sourceData.itemSet.magicResist4P > 0)
			itemSetText += "\t+" + sourceData.itemSet.magicResist4P + " Magic Resist \n";

		if (sourceData.itemSet.strength4P > 0)
			itemSetText += "\t+" + sourceData.itemSet.strength4P + " Strength \n";
		if (sourceData.itemSet.agility4P > 0)
			itemSetText += "\t+" + sourceData.itemSet.agility4P + " Agility \n";
		if (sourceData.itemSet.intelligence4P > 0)
			itemSetText += "\t+" + sourceData.itemSet.intelligence4P + " Intellect \n";

		if (sourceData.itemSet.stamina4P > 0)
			itemSetText += "\t+" + sourceData.itemSet.stamina4P + " Stamina \n";
		if (sourceData.itemSet.dexterity4P > 0)
			itemSetText += "\t+" + sourceData.itemSet.dexterity4P + " Dexterity \n";
		if (sourceData.itemSet.focus4P > 0)
			itemSetText += "\t+" + sourceData.itemSet.focus4P + " Focus \n";
		if (sourceData.itemSet.haste4P > 0)
			itemSetText += "\t+" + sourceData.itemSet.haste4P + " Haste \n";


		// 5 Set Bonus
		if (numberOfEquippedPieces == 5)	itemSetText += GetEarnedSetBonusColorTag ();
		else								itemSetText += GetUnearnedSetBonusColorTag ();
		itemSetText += "  (5)";

		if (sourceData.itemSet.armor5P > 0)
			itemSetText += "\t+" + sourceData.itemSet.armor5P + " Armor \n";
		if (sourceData.itemSet.magicResist5P > 0)
			itemSetText += "\t+" + sourceData.itemSet.magicResist5P + " Magic Resist \n";

		if (sourceData.itemSet.strength5P > 0)
			itemSetText += "\t+" + sourceData.itemSet.strength5P + " Strength \n";
		if (sourceData.itemSet.agility5P > 0)
			itemSetText += "\t+" + sourceData.itemSet.agility5P + " Agility \n";
		if (sourceData.itemSet.intelligence5P > 0)
			itemSetText += "\t+" + sourceData.itemSet.intelligence5P + " Intellect \n";

		if (sourceData.itemSet.stamina5P > 0)
			itemSetText += "\t+" + sourceData.itemSet.stamina5P + " Stamina \n";
		if (sourceData.itemSet.dexterity5P > 0)
			itemSetText += "\t+" + sourceData.itemSet.dexterity5P + " Dexterity \n";
		if (sourceData.itemSet.focus5P > 0)
			itemSetText += "\t+" + sourceData.itemSet.focus5P + " Focus \n";
		if (sourceData.itemSet.haste5P > 0)
			itemSetText += "\t+" + sourceData.itemSet.haste5P + " Haste \n";

		return itemSetText;
	}
	#endregion

	string GetSlotString () {
		if (sourceData.slot == Slot.Weapon1H) {
			return "Main Hand\t\t1H Weapon";
		} else if (sourceData.slot == Slot.Weapon2H) {
			return "Main Hand\t\t2H Weapon";
		}
		else if (sourceData.slot == Slot.OffHand) {
			return "Off Hand";
		}
		else {
			return sourceData.slot.ToString ();
		}
	}

	#region Color Tags
	string GetTitleColorTag () {

		string tag = "<color=";

		if (sourceData.isEquippable) {
			if (sourceData.rarity == Rarity.Common)
				tag += HandsOfWarColors.greenHex;
			else if (sourceData.rarity == Rarity.Rare)
				tag += HandsOfWarColors.blueHex;
			else if (sourceData.rarity == Rarity.Epic)
				tag += HandsOfWarColors.purpleHex;
		}
		else {
			if (sourceData.isQuestItem)
				tag += HandsOfWarColors.yellowHex;
			else
				tag += HandsOfWarColors.whiteHex;
		}

		tag += ">";

		return tag;
	}

	string GetItemLevelColorTag () {
		// find another color here
		return "<color=" + HandsOfWarColors.lightGrayHex + ">";
	}
	string GetItemSlotColorTag () {
		return "<color=" + HandsOfWarColors.mediumGrayHex + ">";
	}
	string GetArmorColorTag () {
		return "<color=" + HandsOfWarColors.whiteHex + ">";
	}
	string GetStatsColorTag () {
		return "<color=" + HandsOfWarColors.lightBrownHex + ">";
	}
	string GetFlavorTextColorTag () {
		return "<color=" + HandsOfWarColors.lightGrayHex + ">";
	}
	string GetItemSetTitleColorTag () {
		return "<color=" + HandsOfWarColors.whiteHex + ">";
	}
	string GetEarnedSetBonusColorTag () {
		return "<color=" + HandsOfWarColors.yellowHex + ">";
	}
	string GetUnearnedSetBonusColorTag () {
		return "<color=" + HandsOfWarColors.mediumGrayHex + ">";
	}
	string GetWeaponColorTag () {
		return "<color=" + HandsOfWarColors.whiteHex + ">";
	}
	#endregion

}

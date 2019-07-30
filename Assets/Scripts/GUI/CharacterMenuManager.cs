using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterMenuManager : MonoBehaviour {

	[Header ("Main")]
	[SerializeField] private GameObject mainWindow;
	private GUIManager gui;

	[Header ("Attributes")]
	[SerializeField] private TextMeshProUGUI strength_Txt;
	[SerializeField] private TextMeshProUGUI agility_Txt;
	[SerializeField] private TextMeshProUGUI intellect_Txt;
	[SerializeField] private TextMeshProUGUI stamina_Txt;
	[SerializeField] private TextMeshProUGUI dexterity_Txt;
	[SerializeField] private TextMeshProUGUI focus_Txt;
	[SerializeField] private TextMeshProUGUI haste_Txt;

	[SerializeField] private TextMeshProUGUI health_Txt;
	[SerializeField] private TextMeshProUGUI energy_Txt;
	[SerializeField] private TextMeshProUGUI armor_Txt;
	[SerializeField] private TextMeshProUGUI magicResist_Txt;
	[SerializeField] private TextMeshProUGUI healthRegen_Txt;
	[SerializeField] private TextMeshProUGUI energyRegen_Txt;
	[SerializeField] private TextMeshProUGUI moveSpeed_Txt;

	[SerializeField] private TextMeshProUGUI critChance_Txt;
	[SerializeField] private TextMeshProUGUI critBonus_Txt;
	[SerializeField] private TextMeshProUGUI hastePercent_Txt;
	[SerializeField] private TextMeshProUGUI dodge_Txt;
	[SerializeField] private TextMeshProUGUI dps_Txt;

	[Header ("Inventory")]
	[SerializeField] private InventoryItem itemOnMouse;
	[SerializeField] private InventoryItem [] inventory;

	[HideInInspector] public InventoryItem originalItemLocation;
	[HideInInspector] public InventoryItem newItemLocation;


	[Header ("Equipment")]
	[SerializeField] private InventoryItem [] equipment;
	[SerializeField] private TextMeshProUGUI gold_Txt;

	[HideInInspector] public int gold;
	[HideInInspector] public int energyPotions;
	[HideInInspector] public int healthPotions;

	public void Start () {
		InventoryItem.characterMenu = this;
		InventoryItem.gui = GUIManager.instance;
		gui = GUIManager.instance;

		SetupInventory ();
		SetupEquipment ();

		/*
		AddItemToInventory (1);
		AddItemToInventory (2);
		AddItemToInventory (3);
		AddItemToInventory (4);
		AddItemToInventory (5);
		AddItemToInventory (19);
		AddItemToInventory (20);
		AddItemToInventory (20);
		AddItemToInventory (21);
		*/
	}



	#region Main Show, Hide, and Update Functions
	private void Update () {
		if (IsItemOnMouse ()) {
			itemOnMouse.SetPosition (Input.mousePosition);
		}

		if (Input.GetKeyDown (KeyCode.P)) {
			AddItemToInventory (Random.Range (1, 50));
		}
	}
	public bool IsVisible () {
		return mainWindow.activeInHierarchy;
	}
	public void ToggleWindow () {
		if (IsVisible ()) 	HideWindow ();
		else				ShowWindow ();
	}

	public void ShowWindow () {
		GUIManager.instance.HideAllWindows ();

		mainWindow.SetActive (true);

		SetAttributeValues ();
	}

	public void HideWindow () {

		// Hide item stats window in case an item is being shown at the time the window is closed
		GUIManager.instance.itemStatsWindow.HideWindow ();

		mainWindow.SetActive (false);
	}
	#endregion


	#region Loading Inventory & Equipment From Save Data
	/// <summary>
	/// Takes the data loaded in and equips items on the character and puts loaded
	/// inventory items in thier correct position.
	/// </summary>
	public void SetLoadedEquipment (int[] loadedItems) {

		// Add equipment to inventory and equip on character
		for (int i = 0; i < equipment.Length; i++) {

			// Is there an item saved in this slot
			if (loadedItems [i] != -1) {
				InventoryItemData item = InventoryItemDatabase.GetItem ( loadedItems [i] );
				equipment [i].SetData (item);
				EquipItem (item, i, false);
			}
		}
	}
	public void SetLoadedInventory (int[] loadedItems) {
		for (int i = 0; i < loadedItems.Length; i++) {
			if (loadedItems [i] != -1) {
				AddItemToInventory (loadedItems [i], i, false);
			}
		}
	}
	#endregion


	#region Equipment Functions
	void SetupEquipment () {
		for (int i = 0; i < equipment.Length; i++) {
			if (equipment [i].sourceData == null) {
				equipment [i].SetDefaultIcon ();
			}
		}
	}
	void EquipItem (InventoryItemData item, int equipmentSlotIndex, bool autoSave = true) {

		// Add stats from item to character
		CharacterManager.instance.AddEquipmentAttributes (item);

		// Update save data
		if (autoSave) {
			SavedGameManager.instance.characterData.SaveEquipmentItem (item.itemID, equipmentSlotIndex);
		}

		SetAttributeValues ();
	}
	void UnequipItem (InventoryItemData item, int equipmentSlotIndex) {

		// Remove stats from item to character
		CharacterManager.instance.RemoveEquipmentAttributes (item);

		// Update save data
		SavedGameManager.instance.characterData.SaveEquipmentItem (-1, equipmentSlotIndex);
		SavedGameManager.instance.SaveCharacterData ();

		SetAttributeValues ();
	}

	int GetSlotIndex (Slot slot) {
		if		(slot == Slot.Head)		return 0;
		else if (slot == Slot.Jewel)	return 1;
		else if (slot == Slot.Chest)	return 2;
		else if (slot == Slot.Hands)	return 3;
		else if (slot == Slot.Legs)		return 4;
		else if (slot == Slot.Feet)		return 5;
		else if (slot == Slot.Weapon1H)	return 6;
		else if (slot == Slot.OffHand)	return 7;

		Debug.Log ("Unknown slot specified: " + slot);
		return -1;
	}

	/// <summary>
	/// Returns true if the item on the user's mouse goes into the slot they're trying to equip it in
	/// </summary>
	bool DoesItemGoInSlot () {
		if (!itemOnMouse.sourceData.isEquippable)
			return false;

		Slot itemOnMouseSlot = itemOnMouse.sourceData.slot;
		Slot newItemSlot = newItemLocation.equipmentSlot;

		// Generic item matching equipment slot
		if (newItemSlot == itemOnMouseSlot)
			return true;

		// 1H weapon in offhand
		if (itemOnMouseSlot == Slot.Weapon1H && newItemSlot == Slot.OffHand)
			return true;

		// 2H weapon in mainhand
		if (itemOnMouseSlot == Slot.Weapon2H && newItemSlot == Slot.Weapon1H)
			return true;

		return false;
	}

	bool CanItemBeEquippedInThisSlot () {

		// Make sure item goes in the slot the user is trying to put it in
		if (DoesItemGoInSlot () == false) {
			print ("That item doesn't go in that slot");
			return false;
		}

		// Check for a 2H weapon equip with a shield already equipped
		if (itemOnMouse.sourceData.slot == Slot.Weapon2H) {
			if (equipment [7].sourceData != null) {
				print ("You must first unequip your your off hand item.");
				return false;
			}
		}

		// Check for equipping a shield with a 2h already equipped
		if (itemOnMouse.sourceData.slot == Slot.OffHand) {
			if (equipment [6].sourceData != null && equipment [6].sourceData.slot == Slot.Weapon2H) {
				print ("You cannot use a shield with a 2H weapon");
				return false;
			}
		}

		return true;
	}

	bool IsDroppedInEquipmentSlot () {
		return itemOnMouse.sourceData.isEquippable && newItemLocation.equipmentSlot != Slot.None;
	}

	public bool IsItemEquipped (InventoryItemData item) {
		for (int i = 0; i < equipment.Length; i++) {
			if (item == equipment[i].sourceData) {
				return true;
			}
		}
		return false;
	}

	#endregion


	#region Inventory Functions
	void SetupInventory () {
		for (int i = 0; i < inventory.Length; i++) {
			inventory [i].slotNumber = i;

			// Set default display for slots that don't have items that were loaded in
			if (inventory [i].sourceData == null) {
				inventory [i].SetData (null);
			}
		}

		itemOnMouse.SetData (null);
		itemOnMouse.Hide ();
	}

	public void AddItemToInventory (int itemID, int addAtSlot = -1, bool autoSave = true) {

		int nextOpenSlot;

		// Automatically find the next open slot
		if (addAtSlot == -1)
			nextOpenSlot = FindOpenInventorySlot ();
		else
			nextOpenSlot = addAtSlot;


		if (nextOpenSlot == -1) {
			print ("Inventory is full");
		}
		else {

			inventory [nextOpenSlot].SetData (InventoryItemDatabase.GetItem (itemID));

			// Save Inventory Data
			if (autoSave) {
				SavedGameManager.instance.characterData.SaveInventory (inventory);
			}
		}
	}

	public bool IsItemOnMouse () {
		return itemOnMouse.sourceData != null;
	}

	public void BeginItemDrag (InventoryItem item) {

		GUIManager.instance.itemStatsWindow.HideWindow ();

		// Store original location of this item
		originalItemLocation = item;

		if (item.equipmentSlot != Slot.None) {
			UnequipItem (item.sourceData, GetSlotIndex (item.equipmentSlot));
		}

		itemOnMouse.SetData (item.sourceData);
		itemOnMouse.SetPosition (item.trans);
		itemOnMouse.Show ();
	}

	private InventoryItemData _tempSourceData;
	public void EndItemDrag () {

		// Mouse currently is not over another slot
		if (newItemLocation == null) {
			return;
		}

		// Clicking on an empty inventory or equipment slot
		if (newItemLocation.sourceData == null) {

			// Check for equipping item
			if ( IsDroppedInEquipmentSlot () ) {
				if (CanItemBeEquippedInThisSlot () == false) {
					return;
				}
				EquipItem (itemOnMouse.sourceData, GetSlotIndex (newItemLocation.equipmentSlot));
			}

			originalItemLocation.SetData (null);
			newItemLocation.SetData (itemOnMouse.sourceData);

			GUIManager.instance.itemStatsWindow.ShowWindow (itemOnMouse.sourceData, newItemLocation.trans);
			newItemLocation.ShowHover ();

			itemOnMouse.SetData (null);
			itemOnMouse.Hide ();
			SavedGameManager.instance.characterData.SaveInventory (inventory);
		}

		// Clicking on an inventory or equipment slot that already has an item in it -> swap items
		else {

			// Check for equipping item
			if (IsDroppedInEquipmentSlot ()) {
				if (CanItemBeEquippedInThisSlot () == false) {
					return;
				}
				UnequipItem (newItemLocation.sourceData, GetSlotIndex (newItemLocation.equipmentSlot));
				EquipItem (itemOnMouse.sourceData, GetSlotIndex (newItemLocation.equipmentSlot));
			}

			_tempSourceData = newItemLocation.sourceData;
			newItemLocation.SetData (itemOnMouse.sourceData);
			itemOnMouse.SetData (_tempSourceData);
		}
	}

	public bool IsInventoryFull () {
		return FindOpenInventorySlot () == -1;
	}

	int FindOpenInventorySlot () {
		for (int i = 0; i < inventory.Length; i++) {
			if (inventory [i].sourceData == null)
				return i;
		}

		// Inventory is full
		return -1;
	}
	#endregion


	#region Stat Display Functions

	void SetAttributeValues () {
		strength_Txt.text = CharacterManager.instance.GetTotalStrength ().ToString ();
		agility_Txt.text = CharacterManager.instance.GetTotalAgility ().ToString ();
		intellect_Txt.text = CharacterManager.instance.GetTotalIntellect ().ToString ();
		stamina_Txt.text = CharacterManager.instance.GetTotalStamina ().ToString ();
		dexterity_Txt.text = CharacterManager.instance.GetTotalDexterity ().ToString ();
		focus_Txt.text = CharacterManager.instance.GetTotalFocus ().ToString ();
		haste_Txt.text = CharacterManager.instance.GetTotalHaste ().ToString ();

		SetHealthDisplay ();
		SetEnergyDisplay ();
		
		armor_Txt.text = CharacterManager.instance.GetTotalArmor ().ToString ();
		magicResist_Txt.text = CharacterManager.instance.GetTotalMagicResist ().ToString ();
		healthRegen_Txt.text = CharacterManager.instance.GetHealthRegen () + "/s";
		energyRegen_Txt.text = CharacterManager.instance.GetEnergyRegen () + "/s";
		moveSpeed_Txt.text = CharacterManager.instance.GetMoveSpeed ().ToString ();

		critChance_Txt.text = CharacterManager.instance.GetCritChance () + "%";
		critBonus_Txt.text = CharacterManager.instance.GetCritBonus () + "%";
		hastePercent_Txt.text = CharacterManager.instance.GetHastePercent () + "%";
		dodge_Txt.text = CharacterManager.instance.GetDodge () + "%";
		dps_Txt.text = CharacterManager.instance.GetDPS ().ToString ();

		GUIManager.instance.playerUnitFrame.SetHealthBar ();
		GUIManager.instance.playerUnitFrame.SetEnergyBar ();

		// Set gold
		gold_Txt.text = "x " + gold;
	}

	void SetHealthDisplay () {
		health_Txt.text = CharacterManager.instance.currentHealth + "/" + CharacterManager.instance.maxHealth;
	}
	void SetEnergyDisplay () {
		energy_Txt.text = CharacterManager.instance.currentEnergy + "/" + CharacterManager.instance.maxEnergy;
	}
	#endregion

}
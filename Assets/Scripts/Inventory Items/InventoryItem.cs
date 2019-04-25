using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IDropHandler, IPointerClickHandler {

	public RectTransform trans;
	public int slotNumber;
	public Slot equipmentSlot;

	[HideInInspector] public InventoryItemData sourceData;

	[SerializeField] private GameObject main_GO;
	[SerializeField] private Image background_Img;
	[SerializeField] private Image hover_Img;
	[SerializeField] private Image icon_Img;
	[SerializeField] private Sprite defaultIcon_Spr;
	[SerializeField] private RectTransform itemStatsWindowLocation;
	
	public static CharacterMenuManager characterMenu;
	public static GUIManager gui;

	#region Mouse Interactions
	public void OnPointerEnter (PointerEventData eventData) {
	
		// Currently an item attached to the mouse
		if (characterMenu.IsItemOnMouse ()) {

			// Save this item as the place to put the item on the mouse if the user drops it here
			characterMenu.newItemLocation = this;
		}

		// There is an inventory item at this slot -> show stats
		else if (sourceData != null) {
			ShowHover ();
			gui.itemStatsWindow.ShowWindow (sourceData, itemStatsWindowLocation);
		}
	}

	public void OnPointerExit (PointerEventData eventData) {

		// Currently an item attached to the mouse
		if (characterMenu.IsItemOnMouse ()) {

			// Clear the saved item
			characterMenu.newItemLocation = null;
		}

		HideHover ();
		gui.itemStatsWindow.HideWindow ();
		
	}

	public void OnPointerClick (PointerEventData eventData) {

		if (characterMenu.IsItemOnMouse ()) {
			characterMenu.EndItemDrag ();
		}

		// There is an item at this position
		else if (sourceData != null) {
			HideHover ();
			characterMenu.BeginItemDrag (this);
			SetData (null);
		}
	}

	public void OnDrag (PointerEventData eventData) {
		if (sourceData == null)
			return;

		if (characterMenu.IsItemOnMouse ())
			return;

		HideHover ();
		characterMenu.BeginItemDrag (this);
		SetData (null);
	}

	public void OnDrop (PointerEventData eventData) {

		characterMenu.EndItemDrag ();
	}

	#endregion

	public void SetDefaultIcon () {

		if (defaultIcon_Spr == null)
			return;

		icon_Img.sprite = defaultIcon_Spr;
		icon_Img.enabled = true;
	}

	public void SetData (InventoryItemData _sourceData) {

		sourceData = _sourceData;

		// Removing inventory item
		if (sourceData == null) {
			background_Img.sprite = SharedGraphics.instance.inventoryItem_Empty;
			icon_Img.enabled = false;

			// Put default icon back in
			if (equipmentSlot != Slot.None)
				SetDefaultIcon ();

			return;
		}

		// Set border based on item type
		if (sourceData.isEquippable) {
			Rarity r = sourceData.rarity;

			if (r == Rarity.Common) 
				background_Img.sprite = SharedGraphics.instance.inventoryItem_Common;
			else if (r == Rarity.Rare)
				background_Img.sprite = SharedGraphics.instance.inventoryItem_Rare;
			else if (r == Rarity.Epic)
				background_Img.sprite = SharedGraphics.instance.inventoryItem_Epic;
		}

		icon_Img.sprite = sourceData.icon;
		icon_Img.enabled = true;
	}

	public void ShowHover () {
		hover_Img.enabled = true;
	}
	public void HideHover () {
		hover_Img.enabled = false;
	}

	public void SetPosition (RectTransform _trans) {
		trans.position = _trans.position;
	}
	public void SetPosition (Vector3 position) {
		trans.position = position;
	}

	public void Hide () {
		main_GO.SetActive (false);
	}
	public void Show () {
		main_GO.SetActive (true);
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class AbilitySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IDropHandler, IPointerClickHandler {


	[SerializeField] private GameObject slot_GO;
	public Button button;
	[SerializeField] private Image abilityIcon_Img;
	[SerializeField] private GameObject hotkey_GO;
	[SerializeField] private TextMeshProUGUI hotKey_Txt;
	public RectTransform trans;
	public int slotNumber;

	private AbilityData sourceAbility;

	public static ActionBarManager actionBarManager;

	#region Mouse Interactions
	public void OnPointerEnter (PointerEventData eventData) {

		// Currently an ability attached to the mouse
		if (actionBarManager.IsAbilityOnMouse ()) {

			// Save this slot as the place to put the ability if the user drops it here
			actionBarManager.newAbilityLocation = this;
		}
	}

	public void OnPointerExit (PointerEventData eventData) {

		// Hide Ability Information

	}

	public void OnPointerClick (PointerEventData eventData) {

		//print ("Pointer Enter: " + sourceAbility);
		// Use Ability

	}

	public void OnDrag (PointerEventData eventData) {

		if (sourceAbility == null)
			return;

		if (actionBarManager.IsAbilityOnMouse ())
			return;

		actionBarManager.BeginAbilityDrag (this);
	}

	public void OnDrop (PointerEventData eventData) {
		actionBarManager.EndAbilityDrag ();
	}

	#endregion



	#region Setting Values
	public void SetHotkeyText (string t) {
		hotKey_Txt.text = t;
	}

	public void SetAbility (AbilityData ability) {
		sourceAbility = ability;
		abilityIcon_Img.sprite = ability.icon;
		abilityIcon_Img.color = Color.white;
		button.interactable = true;
	}
	public AbilityData GetAbility () {
		return sourceAbility;
	}
	#endregion

	#region Display Functions
	public void ClearSlot () {
		sourceAbility = null;
		abilityIcon_Img.sprite = null;
		abilityIcon_Img.color = Color.clear;
		button.interactable = false;
	}
	public void HideSlot () {
		slot_GO.SetActive (false);
	}
	public void ShowSlot () {
		slot_GO.SetActive (true);
	}

	public void HideHotkeyDisplay () {
		hotkey_GO.SetActive (false);
	}
	public void ShowHotkeyDisplay () {
		hotkey_GO.SetActive (true);
	}
	#endregion


	public void SetPosition (RectTransform _trans) {
		trans.position = _trans.position;
	}
	public void SetPosition (Vector3 position) {
		trans.position = position;
	}
}

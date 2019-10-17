using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class AbilitySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IDropHandler, IPointerClickHandler {


	[SerializeField] private Button button;
	[SerializeField] private Image abilityIcon_Img;
	[SerializeField] private GameObject hotkey_GO;
	[SerializeField] private TextMeshProUGUI hotKey_Txt;

	private AbilityData sourceAbility;


	#region Mouse Interactions
	public void OnPointerEnter (PointerEventData eventData) {

		// Show Ability Information
	}

	public void OnPointerExit (PointerEventData eventData) {

		// Hide Ability Information

	}

	public void OnPointerClick (PointerEventData eventData) {

		print ("Pointer Enter: " + sourceAbility);
		// Use Ability

	}

	public void OnDrag (PointerEventData eventData) {
		

	}

	public void OnDrop (PointerEventData eventData) {

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

	public void ClearSlot () {

		sourceAbility = null;
		abilityIcon_Img.sprite = null;
		abilityIcon_Img.color = Color.clear;
		button.interactable = false;
	}

	public void HideHotkeyDisplay () {
		hotkey_GO.SetActive (false);
	}
	public void ShowHotkeyDisplay () {
		hotkey_GO.SetActive (true);
	}
	#endregion

}

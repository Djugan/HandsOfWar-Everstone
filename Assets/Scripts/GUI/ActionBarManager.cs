using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActionBarManager : MonoBehaviour {

	[SerializeField] private AbilitySlot [] actionBarAbilities;

	public AbilitySlot abilityOnMouse;
	public AbilitySlot newAbilityLocation;

	private void Start () {

		abilityOnMouse.HideSlot ();
		abilityOnMouse.HideHotkeyDisplay ();

		AbilitySlot.actionBarManager = this;
		SetActionBarHotkeys ();
		SetActionBarAbilities ();
	}

	private void Update () {
		if (IsAbilityOnMouse ()) {
			abilityOnMouse.SetPosition (Input.mousePosition);
		}
	}

	public bool IsAbilityOnMouse () {
		return abilityOnMouse.GetAbility () != null;
	}

	private void SetActionBarAbilities () {

		for (int i = 0; i < actionBarAbilities.Length; i++) {
			actionBarAbilities [i].ClearSlot ();
			actionBarAbilities [i].slotNumber = i;
		}
		actionBarAbilities [0].SetAbility (AbilityDatabase.GetAbility (1));
		actionBarAbilities [1].SetAbility (AbilityDatabase.GetAbility (2));
	}

	private void SetActionBarHotkeys () {

		for (int i = 0; i < actionBarAbilities.Length; i++) {
			actionBarAbilities [i].SetHotkeyText (KeybindingManager.GetKeyCodeDisplay (KeybindingManager.actionBarKeys [i]));
		}
	}

	public void SetAbilityInSlot (AbilityData ability, int slot) {
		actionBarAbilities [slot].SetAbility (ability);
	}

	public AbilityData GetAbilityInSlot (int slot) {
		return actionBarAbilities [slot].GetAbility ();
	}

	public void MouseEnter () {
		RPGCamera.rotateCamera = false;
	}
	public void MouseExit () {
		RPGCamera.rotateCamera = true;
	}

	public void BeginAbilityDrag (AbilitySlot abilitySlot) {

		// Store original location of this item
		//originalItemLocation = item;

		abilityOnMouse.SetAbility (abilitySlot.GetAbility ());
		abilityOnMouse.SetPosition (abilitySlot.trans);
		abilityOnMouse.ShowSlot ();
		abilityOnMouse.button.interactable = false;

		abilitySlot.ClearSlot ();
	}

	public void EndAbilityDrag () {

		// Mouse currently is not over another slot
		if (newAbilityLocation == null) {
			return;
		}

		// Dropping on an empty ability slot
		if (newAbilityLocation.GetAbility () == null) {

			newAbilityLocation.SetAbility (abilityOnMouse.GetAbility ());
			abilityOnMouse.ClearSlot ();
			abilityOnMouse.HideSlot ();
		}
	}

}

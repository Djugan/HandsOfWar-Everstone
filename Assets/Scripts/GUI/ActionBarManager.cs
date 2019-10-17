using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActionBarManager : MonoBehaviour {

	private AbilityData [] abilities;
	[SerializeField] private AbilitySlot [] actionBarAbilities;

	private void Awake () {
		abilities = new AbilityData [10];
	}

	private void Start () {

		SetActionBarHotkeys ();
		SetActionBarAbilities ();
	}

	private void SetActionBarAbilities () {

		for (int i = 0; i < abilities.Length; i++) {
			if (abilities [i] != null) {
				actionBarAbilities [i].SetAbility (abilities [i]);
			} else {
				actionBarAbilities [i].ClearSlot ();
			}
		}
	}

	private void SetActionBarHotkeys () {

		for (int i = 0; i < actionBarAbilities.Length; i++) {
			actionBarAbilities [i].SetHotkeyText (KeybindingManager.GetKeyCodeDisplay (KeybindingManager.actionBarKeys [i]));
		}
	}

	public void SetAbilityInSlot (AbilityData ability, int slot) {
		abilities [slot] = ability;
	}

	public AbilityData GetAbilityInSlot (int slot) {
		return abilities [slot];
	}

}

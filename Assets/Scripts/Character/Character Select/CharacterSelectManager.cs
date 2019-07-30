using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectManager : MonoBehaviour {

	[SerializeField] private CharacterSelectSlot [] characterSelectSlots;

	private void Start () {

		SetCharacterSlots ();
	}

	private void SetCharacterSlots () {

		// Unselect all slots initially
		DeselectAllCharacterSlots ();

		// Checks each save slot to see if there's a character saved there
		for (int i = 0; i < characterSelectSlots.Length; i++) {

			characterSelectSlots[i].SetCharacterData ( SavedGameManager.instance.LoadCharacterData (i) );
		}
	}

	public void DeselectAllCharacterSlots () {
		for (int i = 0; i < characterSelectSlots.Length; i++) {
			characterSelectSlots [i].DeselectCharacterSlot ();
		}
	}
}

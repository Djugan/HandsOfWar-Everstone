using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterSelectSlot : MonoBehaviour {

	[SerializeField] private GameObject main_GO;

	[SerializeField] private Image playerPortrait_Img;
	[SerializeField] private TextMeshProUGUI characterName_Txt;
	[SerializeField] private TextMeshProUGUI classAndLevel_Txt;

	[SerializeField] private GameObject selectedIndicator_GO;

	private int saveSlot;	// The save slot that is associated with this character select slot


	public void SetSaveSlot (int s) {
		saveSlot = s;
	}

	// Populate or hide the character select slot
	public void SetCharacterData (SGD_Character characterData) {

		// No save data in this slot
		if (characterData == null) {
			main_GO.SetActive (false);
		}
		else {
		
			main_GO.SetActive (true);
			characterName_Txt.text = characterData.playerName;
			classAndLevel_Txt.text = "Level " + characterData.level.ToString () + " " + characterData.playerClass.ToString ();
			playerPortrait_Img.sprite = null;
		}
	}


	public void SelectCharacterSlot () {
		selectedIndicator_GO.SetActive (true);
		SavedGameManager.instance.saveSlot = saveSlot;
	}

	public void DeselectCharacterSlot () {
		selectedIndicator_GO.SetActive (false);
	}

	public bool IsSelected () {
		return selectedIndicator_GO.activeInHierarchy;
	}

}

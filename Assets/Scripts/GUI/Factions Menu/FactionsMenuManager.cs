using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FactionsMenuManager : MonoBehaviour {

	[Header ("Main")]
	[SerializeField] private GameObject mainWindow;
	[SerializeField] private FactionMenuItem [] factionItems;

	#region Show / Hide Functions
	public bool IsVisible () {
		return mainWindow.activeInHierarchy;
	}

	public void ShowWindow () {
		mainWindow.SetActive (true);
	}

	public void HideWindow () {
		mainWindow.SetActive (false);
	}
	#endregion

	public void Init () {

		// Set the text and logos in the menu
		factionItems [0].SetFactionData ("Alliance", SharedGraphics.instance.allianceLogo_128);
		factionItems [1].SetFactionData ("Blademasters", SharedGraphics.instance.blademastersLogo_128);
		factionItems [2].SetFactionData ("Glorious Legion", SharedGraphics.instance.gloriousLegionLogo_128);
		factionItems [3].SetFactionData ("Keepers of the Red Sand", SharedGraphics.instance.keepersOfTheRedSandLogo_128);
		factionItems [4].SetFactionData ("Magicians", SharedGraphics.instance.magiciansLogo_128);
		factionItems [5].SetFactionData ("Voidbringers", SharedGraphics.instance.voidbringersLogo_128);
		factionItems [6].SetFactionData ("Warders", SharedGraphics.instance.wardersLogo_128);
	}
}

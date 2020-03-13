using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuManager : MonoBehaviour {

	[SerializeField] private GameObject mainWindow;

	public CharacterMenuManager characterMenu;
	public MapMenuManager mapMenu;
	public AbilitiesMenuManager abilitiesMenu;
	public TalentsMenuManager talentsMenu;
	public QuestsMenuManager questsMenu;
	public FactionsMenuManager factionsMenu;

	[SerializeField] private Button characterMenu_Btn;
	[SerializeField] private Button mapMenu_Btn;
	[SerializeField] private Button abilitiesMenu_Btn;
	[SerializeField] private Button talentsMenu_Btn;
	[SerializeField] private Button questsMenu_Btn;
	[SerializeField] private Button factionsMenu_Btn;

	#region Main Window
	public void Init () {
		factionsMenu.Init ();
		questsMenu.Init ();
	}
	public void ShowWindow () {
		mainWindow.SetActive (true);
	}
	public void HideWindow () {
		mainWindow.SetActive (false);
	}
	public bool IsVisible () {
		if (characterMenu.IsVisible () || mapMenu.IsVisible () || abilitiesMenu.IsVisible () ||
			talentsMenu.IsVisible () || questsMenu.IsVisible () || factionsMenu.IsVisible ())
			return true;

		return false;
	}
	public void HideAllSubMenus () {
		characterMenu.HideWindow ();
		mapMenu.HideWindow ();
		abilitiesMenu.HideWindow ();
		talentsMenu.HideWindow ();
		questsMenu.HideWindow ();
		factionsMenu.HideWindow ();

		characterMenu_Btn.interactable = true;
		mapMenu_Btn.interactable = true;
		abilitiesMenu_Btn.interactable = true;
		talentsMenu_Btn.interactable = true;
		questsMenu_Btn.interactable = true;
		factionsMenu_Btn.interactable = true;
	}
	#endregion

	#region Charcter Menu
	public void ShowCharacterMenu () {
		HideAllSubMenus ();
		ShowWindow ();
		characterMenu.ShowWindow ();
		characterMenu_Btn.interactable = false;
	}
	public void HideCharacterMenu () {
		characterMenu.HideWindow ();
	}
	public void ToggleCharacterMenu () {
		if (characterMenu.IsVisible ()) {
			HideWindow ();
		} else {
			ShowCharacterMenu ();
		}
	}
	#endregion

	#region Map Menu
	public void ShowMapMenu () {
		HideAllSubMenus ();
		ShowWindow ();
		mapMenu.ShowWindow ();
		mapMenu_Btn.interactable = false;
	}
	public void HideMapMenu () {
		mapMenu.HideWindow ();
	}
	public void ToggleMapMenu () {
		if (mapMenu.IsVisible ()) {
			HideWindow ();
		}
		else {
			ShowMapMenu ();
		}
	}
	#endregion

	#region Abilities Menu
	public void ShowAbilitiesMenu () {

		HideAllSubMenus ();
		ShowWindow ();
		abilitiesMenu.ShowWindow ();
		abilitiesMenu_Btn.interactable = false;
	}
	public void HideAbilitiesMenu () {
		abilitiesMenu.HideWindow ();
	}
	public void ToggleAbilitiesMenu () {
		if (abilitiesMenu.IsVisible ()) {
			HideWindow ();
		}
		else {
			ShowAbilitiesMenu ();
		}
	}
	#endregion

	#region Talents Menu
	public void ShowTalentsMenu () {
		HideAllSubMenus ();
		ShowWindow ();
		talentsMenu.ShowWindow ();
		talentsMenu_Btn.interactable = false;
	}
	public void HideTalentsMenu () {
		talentsMenu.HideWindow ();
	}
	public void ToggleTalentsMenu () {
		if (talentsMenu.IsVisible ()) {
			HideWindow ();
		}
		else {
			ShowTalentsMenu ();
		}
	}
	#endregion

	#region Quests Menu
	public void ShowQuestsMenu () {
		HideAllSubMenus ();
		ShowWindow ();
		questsMenu.ShowWindow ();
		questsMenu_Btn.interactable = false;
	}
	public void HideQuestsMenu () {
		questsMenu.HideWindow ();
	}
	public void ToggleQuestsMenu () {
		if (questsMenu.IsVisible ()) {
			HideWindow ();
		}
		else {
			ShowQuestsMenu ();
		}
	}
	#endregion

	#region Factions Menu
	public void ShowFactionsMenu () {
		HideAllSubMenus ();
		ShowWindow ();
		factionsMenu.ShowWindow ();
		factionsMenu_Btn.interactable = false;
	}
	public void HideFactionsMenu () {
		factionsMenu.HideWindow ();
	}
	public void ToggleFactionsMenu () {
		if (factionsMenu.IsVisible ()) {
			HideWindow ();
		}
		else {
			ShowFactionsMenu ();
		}
	}
	#endregion
}

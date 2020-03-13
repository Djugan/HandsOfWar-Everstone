using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour {

	public static GUIManager instance;

	[Header ("Menus")]
	public MainMenuManager mainMenu;
	public ActionBarManager actionBar;

	[Header ("Windows")]
	public ItemStatsWindow itemStatsWindow;
	public PlayerFrameManager playerUnitFrame;
	public TargetFrameManager targetFrameManager;
	public LootWindowManager lootWindow;

	[Header ("Misc")]
	public CursorManager cursorManager;


	private void Awake () {
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (gameObject);

		} else {
			Destroy (gameObject);
		}
	}

	#region Startup Functions
	private void Start () {
		HideAllWindows ();
		mainMenu.Init ();
	}
	#endregion


	void Update () {
		CheckForMenuHotkeys ();
	}

	public bool IsMenuVisible () {
		return mainMenu.IsVisible ();
	}

	void CheckForMenuHotkeys () {

		if (Input.GetKeyDown (KeyCode.C)) {
			mainMenu.ToggleCharacterMenu ();
		}
		else if (Input.GetKeyDown (KeyCode.M)) {
			mainMenu.ToggleMapMenu ();
		}

		if (Input.GetKeyDown (KeyCode.Escape)) {
			HandleEscapeKeyPress ();
		}
	}

	private void HandleEscapeKeyPress () {

		if (actionBar.IsAbilityOnMouse ()) {
			actionBar.ClearAbilityOnMouse ();
		}

	}


	#region Menu Open and Close
	public void HideAllWindows () {
		mainMenu.HideWindow ();
		itemStatsWindow.HideWindow ();
		lootWindow.HideWindow ();
	}
	#endregion
}

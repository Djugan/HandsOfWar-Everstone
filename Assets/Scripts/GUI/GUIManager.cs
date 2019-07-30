using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour {

	public static GUIManager instance;

	[Header ("Menus")]
	public CharacterMenuManager characterMenu;
	public MapMenuManager mapMenu;

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
	}
	#endregion


	void Update () {
		CheckForMenuHotkeys ();
	}

	public bool IsMenuVisible () {
		return characterMenu.IsVisible ();
	}

	void CheckForMenuHotkeys () {

		if (Input.GetKeyDown (KeyCode.C)) {
			characterMenu.ToggleWindow ();
		}
		else if (Input.GetKeyDown (KeyCode.M)) {
			mapMenu.ToggleWindow ();
		}
	}


	#region Menu Open and Close
	public void HideAllWindows () {
		characterMenu.HideWindow ();
		mapMenu.HideWindow ();
		itemStatsWindow.HideWindow ();
		lootWindow.HideWindow ();
	}
	#endregion
}

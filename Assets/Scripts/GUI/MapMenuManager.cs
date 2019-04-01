using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MapMenuManager : MonoBehaviour {

	[Header ("Main")]
	[SerializeField] private GameObject mainWindow;


	public void Start () {
		HideWindow ();
	}

	#region Show / Hide Functions
	public bool IsVisible () {
		return mainWindow.activeInHierarchy;
	}
	public void ToggleWindow () {
		if (IsVisible ()) HideWindow ();
		else ShowWindow ();
	}

	public void ShowWindow () {
		mainWindow.SetActive (true);
	}

	public void HideWindow () {
		mainWindow.SetActive (false);
	}
	#endregion
}

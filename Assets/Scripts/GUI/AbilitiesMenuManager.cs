using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesMenuManager : MonoBehaviour {

	[Header ("Main")]
	[SerializeField] private GameObject mainWindow;

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
}

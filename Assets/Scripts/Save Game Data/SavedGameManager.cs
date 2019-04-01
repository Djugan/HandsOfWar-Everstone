using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SavedGameManager : MonoBehaviour {

	public static SavedGameManager instance;
	[HideInInspector] public string saveDataPath;
	[HideInInspector] public int saveSlot;
	[HideInInspector] public string saveFileExt = ".dat";

	[HideInInspector] public SGD_Character characterData;

	private BinaryFormatter binaryFormatter;

	#region Startup
	void Awake () {
		if (instance == null) {
			instance = this;
			saveSlot = 1;
			DontDestroyOnLoad (gameObject);
			CreateSaveDataObjects ();
		}
		else {
			Destroy (gameObject);
		}
	}

	private void Start () {
		LoadAllStartupData ();
	}

	void CreateSaveDataObjects () {

		saveDataPath = Application.persistentDataPath;

		binaryFormatter = new BinaryFormatter ();

		characterData = new SGD_Character ("/Character");
	}


	public void LoadAllStartupData () {

		// Load Character Data
		SGD_Character tempCharacter = LoadCharacterData ();
		if (tempCharacter != null) {
			characterData = tempCharacter;
			GUIManager.instance.characterMenu.SetLoadedItemsInInventory ();
		}
	}
	#endregion

	#region Character Data
	public void SaveCharacterData () {

		print ("writing char data to file");
		string fullPath = saveDataPath + characterData.GetFileName () + saveSlot + saveFileExt;

		print ("Head item: " + characterData.equipment [0]);

		FileStream file = File.Create (fullPath);
		binaryFormatter.Serialize (file, characterData);
		file.Close ();
	}
	public SGD_Character LoadCharacterData () {

		SGD_Character data = null;
		string fullPath = saveDataPath + characterData.GetFileName () + saveSlot + saveFileExt;

		if (File.Exists (fullPath)) {
			//print ("I are loading stuff");
			FileStream file = File.Open (fullPath, FileMode.Open);
			data = (SGD_Character)binaryFormatter.Deserialize (file);
			file.Close ();
		}

		return data;
	}
	public void DeleteCharacterData () {

		string fullPath = saveDataPath + characterData.GetFileName () + saveSlot + saveFileExt;

		// Delete local data from Persistent Data Path
		try {
			File.Delete (fullPath);
		}
		catch (IOException e) {
			Debug.Log ("Error deleing file: " + e.Message + " " + e.ToString ());
		}
	}

	#endregion
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDatabase : MonoBehaviour {

	private static Dictionary<int, QuestData> questDatabase;
	private static bool isLoaded = false;

	static void LoadQuestDatabase () {

		questDatabase = new Dictionary<int, QuestData> ();

		// Load from Asset Bundle
		//QuestData [] quests = AssetManager.LoadBundle ("quests").LoadAllAssets <QuestData> ();

		// Load from Resources
		QuestData [] quests = Resources.LoadAll<QuestData> ("Quests");

		// Add each inventory item to the database
		for (int i = 0; i < quests.Length; i++) {
			questDatabase.Add (quests [i].questID, quests [i]);
		}

		isLoaded = true;
	}

	public static QuestData GetQuest (int questID) {

		if (isLoaded == false) {
			LoadQuestDatabase ();
		}

		if (questDatabase.ContainsKey (questID))
			return questDatabase [questID];

		Debug.Log ("No quest was found in the Quest Database with questID: " + questID);
		return null;
	}
}

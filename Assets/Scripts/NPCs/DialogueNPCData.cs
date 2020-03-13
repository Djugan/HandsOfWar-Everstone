using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueNPCData : NPCData {

	[SerializeField] private QuestData[] quests;

	public QuestData[] GetQuests () {
		return quests;
	}
}

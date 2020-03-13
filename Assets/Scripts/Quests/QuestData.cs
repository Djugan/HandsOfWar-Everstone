using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class QuestData : ScriptableObject {

	public string questName;

	public int questID;
	public string questDescription;
	public string questCompleteDialogue;

	public QuestObjectiveData [] questObjectives;

	public int goldReward;
	public int expReward;

	public InventoryItemData [] guaranteedRewards;
	public InventoryItemData [] selectedRewards;

	public bool isQuestAvailable;
	public bool isRepeatable;
	public bool isComplete;
	public bool isOnQuest;
	public int timeLimit;
	public QuestData [] unlocksWhenComplete;

	public bool hasOnAcceptEvent;
	public bool hasOnCompleteEvent;
}

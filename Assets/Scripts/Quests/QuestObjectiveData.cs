using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum QuestObjectiveType { Kill, Fetch, TalkTo, KillCollect, Explore, Escort, Skill }

public class QuestObjectiveData : ScriptableObject {

	public QuestObjectiveType objectiveType;
	public int requiredAmount;
	public int currentAmount;

	public string updateText;

	[Header ("Only used Fetch objective types")]
	//public QuestObject targetObject;

	[Header ("Only used Explore objective types")]
	//public QuestArea targetObject;

	[Header ("Only used in Kill, KillCollect, GoTalkTo, and Escort objective types")]
	public NPCData [] targetNPCs;

}

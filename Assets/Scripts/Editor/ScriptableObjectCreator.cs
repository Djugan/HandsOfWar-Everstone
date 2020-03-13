using UnityEngine;
using UnityEditor;
using System.Collections;

public class ScriptableObjectCreator {

	[MenuItem ("Assets/Create/Scriptable Object/Inventory Item")]
	static public void CreateInventoryItem () {
		ScriptableObjectUtility.CreateAsset <InventoryItemData> ();
	}

	[MenuItem ("Assets/Create/Scriptable Object/Item Set")]
	static public void CreateItemSet () {
		ScriptableObjectUtility.CreateAsset <ItemSet> ();
	}

	[MenuItem ("Assets/Create/Scriptable Object/Enemy NPC")]
	static public void CreateEnemyNPC () {
		ScriptableObjectUtility.CreateAsset<EnemyNPCData> ();
	}

	[MenuItem ("Assets/Create/Scriptable Object/Dialogue NPC")]
	static public void CreateDialogueNPC () {
		ScriptableObjectUtility.CreateAsset<DialogueNPCData> ();
	}

	[MenuItem ("Assets/Create/Scriptable Object/Loot Table")]
	static public void CreateLootTable () {
		ScriptableObjectUtility.CreateAsset<LootTable> ();
	}

	[MenuItem ("Assets/Create/Scriptable Object/Ability")]
	static public void CreateAbility () {
		ScriptableObjectUtility.CreateAsset<AbilityData> ();
	}

	[MenuItem ("Assets/Create/Scriptable Object/Enemy Ability")]
	static public void CreateEnemyAbility () {
		ScriptableObjectUtility.CreateAsset<EnemyAbilityData> ();
	}

	[MenuItem ("Assets/Create/Scriptable Object/Quest")]
	static public void CreateQuest () {
		ScriptableObjectUtility.CreateAsset<QuestData> ();
	}

	[MenuItem ("Assets/Create/Scriptable Object/Quest Objective")]
	static public void CreateQuestObjective () {
		ScriptableObjectUtility.CreateAsset<QuestObjectiveData> ();
	}
}

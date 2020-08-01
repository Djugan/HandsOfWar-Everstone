using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class QuestsMenuManager : MonoBehaviour {

	[Header ("Main")]
	[SerializeField] private GameObject mainWindow;
	[SerializeField] private QuestMenuItem [] questMenuItems;
	
	[Header ("Quest Description Section")]
	[SerializeField] private GameObject questDescriptionWindow_GO;
	[SerializeField] private TextMeshProUGUI questTitle_Txt;
	[SerializeField] private TextMeshProUGUI [] questObjectives_Txt;
	[SerializeField] private GameObject [] questObjectives_GO;
	[SerializeField] private TextMeshProUGUI questDescription_Txt;
	[SerializeField] private QuestMenuLoot[] rewardSlots_GO;
	[SerializeField] private int[] selectableSlots;
	[SerializeField] private bool itemSelected = false;

	private List<QuestData> activeQuests;

	public void Init () {
		activeQuests = new List<QuestData> ();

		for (int i = 0; i < questMenuItems.Length; i++) {
			questMenuItems [i].SetIndexInList (i);
			questMenuItems [i].Hide ();
		}

		questDescriptionWindow_GO.SetActive (false);
	}

	#region Show / Hide Functions
	public bool IsVisible () {
		return mainWindow.activeInHierarchy;
	}

	public void ShowWindow () {
		questDescriptionWindow_GO.SetActive (false);
		mainWindow.SetActive (true);
	}

	public void HideWindow () {
		mainWindow.SetActive (false);
	}

	#endregion


	#region Accepting & Viewing Quests
	public void AcceptQuest (QuestData quest) {

		// Make sure there is room for the quest in your log
		if (activeQuests.Count >= questMenuItems.Length) {
			print ("Quest log is full");
			return;
		}

		if (quest.isOnQuest)
		{
			if (quest.isComplete)
			{
				GainLoot(quest);
				return;
			}

			print("Already on quest!");
			return;
		}

		print ("Quest Accepted: " + quest.questName);

		// Set all objective current amount values to zero
		for (int i = 0; i < quest.questObjectives.Length; i++) {
			quest.questObjectives [i].currentAmount = 0;
		}


		if (quest.isQuestAvailable)
		{
			activeQuests.Add(quest);
			questMenuItems[activeQuests.Count - 1].SetQuestInformation(quest);
			quest.isOnQuest = true;
			quest.isQuestAvailable = false;
		}
		else
		{
			print("Quest Not Available");
		}
	}

	public void SetQuestDescriptionWindow (int index) {

		QuestData quest = activeQuests [index];

		questTitle_Txt.text = quest.questName;

		// Hide all quest objective displays to start
		for (int i = 0; i < questObjectives_Txt.Length; i++) {
			questObjectives_GO [i].SetActive (false);
		}

		// Show the quest objectives
		QuestObjectiveData objective;
		for (int i = 0; i < quest.questObjectives.Length; i++) {
			objective = quest.questObjectives [i];
			questObjectives_Txt [i].text = objective.updateText + " " + objective.currentAmount + " / " + objective.requiredAmount;
			questObjectives_GO [i].SetActive (true);
		}

		questDescription_Txt.text = quest.questDescription;
		questDescriptionWindow_GO.SetActive (true);

		int slot = 0;

		for (int i = 0; i < quest.guaranteedRewards.Length; i++)
		{
			rewardSlots_GO[i].SetItem(quest, i);
			slot++;
			print(slot);
		}

		selectableSlots = new int[100];

		for (int i = 0; i < quest.selectedRewards.Length; i++)
		{
			selectableSlots[i] = slot;
			Button selectButton = rewardSlots_GO[slot].GetComponent<Button>();
			selectButton.interactable = true;
			rewardSlots_GO[slot].SetSelectedItem(quest, i);
			slot++;
		}

		if (quest.goldReward > 0)
		{
			
			print(slot);
			rewardSlots_GO[slot].SetGold(quest);
			slot++;
		}

		if (quest.expReward > 0)
		{
				rewardSlots_GO[slot].SetExp(quest);
				slot++;
		}

	}
	#endregion


	#region Completing Quests
	/// <summary>
	/// Checks the active quest log to see if the user has any quests related to the enemy that was just killed.
	/// </summary>
	public void CheckForObjective_Kill (EnemyNPCData enemy) {

		QuestObjectiveData objective;

		// Loop through all quests
		for (int i = 0; i < activeQuests.Count; i++) {

			if (activeQuests [i].isComplete)
				continue;
				
			// Loop through all quest objectives
			for (int j = 0; j < activeQuests [i].questObjectives.Length; j++) {

				objective = activeQuests [i].questObjectives [j];

				if (objective.objectiveType == QuestObjectiveType.Kill) {

					// Loop through all valid enemies
					for (int e = 0; e < objective.targetNPCs.Length; e++) {

						// Does the enemy we killed match any valid NPC for this quest
						if (enemy == objective.targetNPCs [e]) {
							objective.currentAmount++;

							print ("We killed the thing. Current amount is: " + objective.currentAmount);

							CheckForCompletedQuest (activeQuests [i]);
						}
					}
				}
			}
		}
	}

	private void CheckForCompletedQuest (QuestData quest) {

		bool isQuestComplete = true;

		// Loop through all quest objectives
		for (int i = 0; i < quest.questObjectives.Length; i++) {

			if (quest.questObjectives [i].currentAmount < quest.questObjectives [i].requiredAmount) {
				isQuestComplete = false;
				break;
			}
		}

		quest.isComplete = isQuestComplete;

		print ("Quest is complete: " + quest.isComplete);

	}

	private void GainLoot(QuestData quest)
	{
		if (quest.selectedRewards.Length != 0)
		{
			InventoryItemData itemData = SelectedLoot(quest);
			if (itemData == null)
			{
				print("Please select item to continue");
				return;
			}
			else
			{
				GUIManager.instance.mainMenu.characterMenu.AddItemToInventory(itemData.itemID);
			}
		}
		if (quest.goldReward > 0)
		{
			GUIManager.instance.mainMenu.characterMenu.gold += quest.goldReward;
		}
		if (quest.expReward > 0)
		{
			//GUIManager.instance.mainMenu.characterMenu.exp += quest.goldReward;
			print("Exp added " + quest.expReward);
		}
		if (quest.guaranteedRewards.Length != 0)
		{
			for (int i = 0; i < quest.guaranteedRewards.Length; i++)
			{
				InventoryItemData itemData = quest.guaranteedRewards[i];
				GUIManager.instance.mainMenu.characterMenu.AddItemToInventory(itemData.itemID);
			}
		}
		
		RemoveCompletedQuest(quest);
	}

	private InventoryItemData SelectedLoot(QuestData quest)
	{
		for (int i = 0; i < selectableSlots.Length; i++)
		{
			if (rewardSlots_GO[selectableSlots[i]].selectedItem == true)
			{
				itemSelected = true;
				InventoryItemData itemData = quest.selectedRewards[i];
				return itemData;
			}
		}


		if (itemSelected == false)
		{
			print("No Item Selected");
			InventoryItemData itemData = null;
			return itemData;
			
		}

		return null;
	}

	
	private void RemoveCompletedQuest(QuestData quest)
	{

		quest.isComplete = false;
		quest.isOnQuest = false;

		activeQuests.Remove(quest);


		if (quest.isRepeatable)
		{
			quest.isQuestAvailable = true;
		}

		Init();
	}
	#endregion
}


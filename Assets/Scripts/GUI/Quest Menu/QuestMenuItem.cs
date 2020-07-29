using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Reflection;

public class QuestMenuItem : MonoBehaviour {

	[SerializeField] private GameObject mainWindow_GO;
	[SerializeField] private TextMeshProUGUI questTitle_Txt;
	[SerializeField] private Image factionLogo_Img;

	private int indexInList;

	public void SetIndexInList (int i) {
		indexInList = i;
	}
	public void Hide () {
		mainWindow_GO.SetActive (false);
	}

	public void Show () {
		mainWindow_GO.SetActive (true);
	}

	public void SetQuestInformation (QuestData quest) {
		questTitle_Txt.text = quest.questName;
		factionLogo_Img.sprite = SharedGraphics.instance.allianceLogo_128;
		Show ();
	}

	public void SetQuestInMainWindow () {
		GUIManager.instance.mainMenu.questsMenu.SetQuestDescriptionWindow (indexInList);
	}
}

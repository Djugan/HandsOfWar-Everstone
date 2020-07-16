using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TestingData : MonoBehaviour
{
    [SerializeField] private GameObject mainWindow;
    [SerializeField] private QuestData quest;
    [SerializeField] private TextMeshProUGUI questName;
    [SerializeField] private TextMeshProUGUI questDescription;
    [SerializeField] private TextMeshProUGUI questObj1;
    [SerializeField] private TextMeshProUGUI questObj2;
    [SerializeField] private TextMeshProUGUI questObj3;
    [SerializeField] private QuestObjectiveData objectives1;
    [SerializeField] private QuestObjectiveData objectives2;
    [SerializeField] private QuestObjectiveData objectives3;


    public void ShowWindow()
    {
        mainWindow.SetActive(true);
        QuestWindow();
    }
    public void HideWindow()
    {
        mainWindow.SetActive(false);
    }

    public void QuestWindow()
    {
        questName.text = quest.questName;
        questDescription.text = quest.questDescription;
        questObj1.text = objectives1.updateText;
        questObj2.text = objectives2.updateText;
        questObj3.text = objectives3.updateText;
    }
}

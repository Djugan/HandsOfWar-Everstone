using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TestingData : MonoBehaviour
{
    [SerializeField] private QuestData quest;
    [SerializeField] private TextMeshProUGUI questName;
    [SerializeField] private TextMeshProUGUI questDescription;
    [SerializeField] private TextMeshProUGUI questObj1;
    [SerializeField] private TextMeshProUGUI questObj2;
    [SerializeField] private TextMeshProUGUI questObj3;
    [SerializeField] private QuestObjectiveData objectives1;
    [SerializeField] private QuestObjectiveData objectives2;
    [SerializeField] private QuestObjectiveData objectives3;


    private void Awake()
    {
        QuestWindow();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TargetFrameManager : MonoBehaviour {
	
	[Header ("Main")]
	[SerializeField] private GameObject mainWindow_GO;

	[Header ("Target Name")]
	[SerializeField] private TextMeshProUGUI targetName_Txt;

	[Header ("Health Bar")]
	[SerializeField] private Image healthBar_Img;
	[SerializeField] private TextMeshProUGUI health_Txt;

	[Header ("Faction Bar")]
	[SerializeField] private Image factionBar_Img;
	[SerializeField] private TextMeshProUGUI faction_Txt;
	[SerializeField] private Sprite [] factionBars;

	[Header ("Faction Icon")]
	[SerializeField] private Image factionIcon_Img;

	[Header ("Portrait")]
 	[SerializeField] private Image portrait_Img;
	[SerializeField] private Image specialNPCIndicator;
	[SerializeField] private Sprite [] specialNPCSprites;

	[Header ("Level")]
	[SerializeField] private TextMeshProUGUI level_Txt;
	[SerializeField] private GameObject bossLevelIndicator_GO;

	[Header ("Ranger Points")]
	[SerializeField] private GameObject rangerPoints_GO;
	[SerializeField] private GameObject [] points;

	private EnemyNPCManager enemyNPCTarget;


	public void Hide () {
		mainWindow_GO.SetActive (false);
	}
	public void Show () {
		mainWindow_GO.SetActive (true);
	}
	public void SetInitalTargetDisplay () {

		NPCManager target = TargetManager.instance.GetTarget ();

		// Set portrait
		portrait_Img.sprite = target.GetSourceData ().portrait;

		// Determine the NPC type

		// EnemyNPC
		if (target is EnemyNPCManager) {

			enemyNPCTarget = (EnemyNPCManager)target;
			targetName_Txt.text = target.GetSourceData ().npcName;
			SetTargetHealth ();
		}
		/*
		// DialogueNPC
		else if (TargetManager.instance.GetTarget () is DialogueNPC) {
			enemyNPCTarget = null;
		}
		// MerchantNPC
		else if (TargetManager.instance.GetTarget () is MerchantNPC) {
			enemyNPCTarget = null;
		}
		*/
		Show ();

	}

	public void SetTargetHealth () {
		health_Txt.text = enemyNPCTarget.GetHealthText ();
		healthBar_Img.fillAmount = enemyNPCTarget.GetHealthPercent ();
	}
}

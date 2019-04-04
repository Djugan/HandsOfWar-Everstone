using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TargetFrameManager : MonoBehaviour {

	[SerializeField] private TextMeshProUGUI targetName_Txt;
 	[SerializeField] private Image portrait_Img;
	[SerializeField] private Image healthBar_Img;
	[SerializeField] private Image factionBar_Img;

	[SerializeField] private TextMeshProUGUI health_Txt;
	[SerializeField] private TextMeshProUGUI faction_Txt;

	[SerializeField] private TextMeshProUGUI npcName_Txt;

	[SerializeField] private TextMeshProUGUI level_Txt;


	private EnemyNPCManager enemyNPCTarget;

	public void SetInitalTargetDisplay () {

		NPCManager target = TargetManager.instance.GetTarget ();

		// Set portrait
		portrait_Img.sprite = target.GetSourceData ().portrait;

		// Determine the NPC type

		// EnemyNPC
		if (target is EnemyNPCManager) {

			enemyNPCTarget = (EnemyNPCManager)target;
			npcName_Txt.text = target.GetSourceData ().npcName;
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


	}

	public void SetTargetHealth () {
		health_Txt.text = enemyNPCTarget.GetHealthText ();
		healthBar_Img.fillAmount = enemyNPCTarget.GetHealthPercent ();
	}
}

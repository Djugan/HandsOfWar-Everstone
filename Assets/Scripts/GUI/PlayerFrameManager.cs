using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerFrameManager : MonoBehaviour {

	[SerializeField] private TextMeshProUGUI playerName_Txt;

	[SerializeField] private Image healthBar_Img;
	[SerializeField] private TextMeshProUGUI health_Txt;

	[SerializeField] private Image energyBar_Img;
	[SerializeField] private TextMeshProUGUI energy_Txt;

	[SerializeField] private TextMeshProUGUI level_Txt;

	public void SetPlayerName () {
		playerName_Txt.text = CharacterManager.instance.playerName;
	}

	public void SetPlayerLevel () {
		level_Txt.text = CharacterManager.instance.level.ToString ();
	}

	public void SetHealthBar () {
		float perc = (float)CharacterManager.instance.currentHealth / CharacterManager.instance.maxHealth;
		healthBar_Img.fillAmount = perc;

		health_Txt.text = CharacterManager.instance.currentHealth + "/" + CharacterManager.instance.maxHealth;
	}

	public void SetEnergyBar () {
		float perc = (float)CharacterManager.instance.currentEnergy / CharacterManager.instance.maxEnergy;
		energyBar_Img.fillAmount = perc;

		energy_Txt.text = CharacterManager.instance.currentEnergy + "/" + CharacterManager.instance.maxEnergy;
	}

}

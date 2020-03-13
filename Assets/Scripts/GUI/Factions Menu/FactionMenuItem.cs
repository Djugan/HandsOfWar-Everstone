using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FactionMenuItem : MonoBehaviour {

	[SerializeField] private TextMeshProUGUI factionName_Txt;
	[SerializeField] private Image factionLogo_Img;
	[SerializeField] private Image fillBar_Img;

	// Change this once faction data is set up
	public void SetFactionData (string factionName, Sprite factionLogo) {
		factionName_Txt.text = factionName;
		factionLogo_Img.sprite = factionLogo;
	}
}

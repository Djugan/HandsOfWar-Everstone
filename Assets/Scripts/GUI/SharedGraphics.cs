using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SharedGraphics : MonoBehaviour {

	public static SharedGraphics instance;

	[Header ("Inventory Item Backgrounds")]
	public Sprite inventoryItem_Empty;
	public Sprite inventoryItem_Basic;
	public Sprite inventoryItem_Common;
	public Sprite inventoryItem_Rare;
	public Sprite inventoryItem_Epic;
	public Sprite inventoryItem_Quest;

	[Header ("Faction Logos")]
	public Sprite allianceLogo_128;
	public Sprite blademastersLogo_128;
	public Sprite gloriousLegionLogo_128;
	public Sprite keepersOfTheRedSandLogo_128;
	public Sprite magiciansLogo_128;
	public Sprite voidbringersLogo_128;
	public Sprite wardersLogo_128;

	[Header ("Misc Icons")]
	public Sprite goldIcon;
	public Sprite healthPotionIcon;
	public Sprite energyPotionIcon;

	private void Awake () {
		instance = this;
	}

}



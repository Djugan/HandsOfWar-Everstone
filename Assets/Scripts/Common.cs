using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Rarity { Common, Rare, Epic }
public enum Slot { None, Head, Chest, Hands, Legs, Feet, Jewel, Weapon1H, Weapon2H, OffHand }

public enum Class { Warrior, Wizard, Ranger }

public class Common : MonoBehaviour {

	public static WaitForSeconds oneSecond_WFS;
	public static WaitForSeconds fiveSeconds_WFS;

	private void Awake () {

		oneSecond_WFS =		new WaitForSeconds (1.0f);
		fiveSeconds_WFS =	new WaitForSeconds (5.0f);
	}
}


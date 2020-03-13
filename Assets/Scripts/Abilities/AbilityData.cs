using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityData : ScriptableObject {

	public int abilityID;
	public string abilityName;
	public Sprite icon;
	public int damage;
	public int range = 99;
	public int castTime = 0;
	public int cooldown = 0;
	public int energyCost = 0;
	public int animationNumber = 0;
	public bool requiresTarget = true;
	public bool harmfulAbility = true;
	public bool isAoE = false;

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAbilityData : ScriptableObject {

	public string abilityName;
	public AbilityType abilityType;
	public int animationNumber;
	public DamageType damageType;
	public int abilityRange;
	public float cooldown;
}

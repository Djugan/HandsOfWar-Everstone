using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAbility {

	private EnemyAbilityData sourceData;
	private float cooldown;
	private int abilityValue;

	public EnemyAbility (EnemyAbilityData _sourceData, int _abilityValue) {
		sourceData = _sourceData;
		abilityValue = _abilityValue;
		cooldown = 0f;
	}

	public EnemyAbilityData GetSourceAbility () {
		return sourceData;
	}

	public int GetAbilityRange () {
		return sourceData.abilityRange;
	}

	public int GetAbilityValue () {
		return abilityValue;
	}

	public int GetAnimationNumber () {
		return sourceData.animationNumber;
	}

	public void SetCooldown () {
		cooldown = sourceData.cooldown;
	}

	public bool IsOnCooldown () {
		return cooldown > 0f;
	}

	public void UpdateCooldown () {
		cooldown -= Time.deltaTime;
	}

	public float GetCooldown () {
		return cooldown;
	}
}

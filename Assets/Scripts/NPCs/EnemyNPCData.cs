using UnityEngine;
using UnityEditor;
using System.Collections;
using System;

public class EnemyNPCData : NPCData {

	public int baseHealth;
	public int numberOfDeathAnimations;
	public LootTable lootTable;
	public float goldDropMultiplier = 1f;
	public float aggroRadius = 7f;

	public EnemyAbilityData ability1;
	public int abilityValue1;

	public EnemyAbilityData ability2;
	public int abilityValue2;

	public EnemyAbilityData ability3;
	public int abilityValue3;

	public EnemyAbilityData ability4;
	public int abilityValue4;
}

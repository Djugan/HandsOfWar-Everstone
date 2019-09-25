using UnityEngine;
using UnityEditor;
using System.Collections;
using System;

public class EnemyNPCData : NPCData {

	public int damage;
	public int baseHealth;
	public int numberOfDeathAnimations;
	public LootTable lootTable;
	public float goldDropMultiplier = 1f;
	public float aggroRadius = 7f;
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNPCManager : NPCManager {

	[SerializeField] private int indexInZone;
	[SerializeField] private Animator animator;

	private EnemyNPCData sourceData;
	private EnemyNPCInstanceLink instanceLink;

	private int damage;
	private int maxHealth;
	private int currentHealth;
	private bool isAlive;
	private int numberOfDeathAnimations;
	private int despawnTimer;
	private int defaultDespawnTime = 50 * 5;        // 50 frames * seconds
	private int respawnTimer;
	private Vector3 spawnPoint;

	private void Start () {

		// Find InstanceLink from zone list in WorldManager
		instanceLink = WorldManager.instance.FindInstanceLink (indexInZone);

		instanceLink.instanceManager = this;
		sourceData = instanceLink.sourceData;

		// This guy should be dead
		if (instanceLink.active == false) {
			Despawn ();
			return;
		}

		spawnPoint = transform.position;
		Spawn ();
		// print ("My name is: " + sourceData.npcName + " and I deal " + sourceData.damage + " damage");
	}

	private void FixedUpdate () {

		if (isAlive == false) {

			// Check for despawning corpse
			despawnTimer--;
			if (despawnTimer <= 0) {
				Despawn ();

				// Add to respawn manager
				// Do that here <-- here
				// ^ right there

				// Also... make HashIDs for the animator parameters

			}
		}
	}

	public override NPCData GetSourceData () {
		return sourceData;
	}

	public void Despawn () {

		// Clear target on despawn
		if (IsThisTheCurrentTarget ()) {
			TargetManager.instance.SetTarget (null);
		}

		// Add to respawn manager
		instanceLink.timeUntilRespawn = respawnTimer;
		RespawnManager.instance.AddEnemyToRespawnList (instanceLink);

		instanceLink.active = false;
		gameObject.SetActive (false);
	}

	public void Spawn () {

		transform.position = spawnPoint;
		isAlive = true;
		damage = sourceData.damage;
		maxHealth = sourceData.baseHealth;
		currentHealth = maxHealth;
		numberOfDeathAnimations = sourceData.numberOfDeathAnimations;
		respawnTimer = instanceLink.respawnTimer;

		gameObject.SetActive (true);
	}

	public override void ReceiveDamage (int amount) {
		currentHealth -= amount;

		// Update target display
		if (IsThisTheCurrentTarget ()) {
			GUIManager.instance.targetFrameManager.SetTargetHealth ();
		}

		if (currentHealth <= 0) {
			KillNPC ();
		}
	}
	public bool IsThisTheCurrentTarget () {
		return TargetManager.instance.GetTarget () == this;
	}

	void KillNPC () {

		if (isAlive == false)
			return;

		isAlive = false;

		// Play dying animation
		animator.SetInteger ("DeathValue", Random.Range (1, numberOfDeathAnimations + 1));
		StartCoroutine (ResetDeathValue ());

		// Start despawn timer
		despawnTimer = defaultDespawnTime;
	}

	IEnumerator ResetDeathValue () {
		yield return null;
		animator.SetInteger ("DeathValue", 0);
	}

	public string GetHealthText () {
		return currentHealth + "/" + maxHealth;
	}
	public float GetHealthPercent () {
		return (float)currentHealth / maxHealth;
	}
}

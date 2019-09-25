using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNPCManager : NPCManager {

	[SerializeField] private int indexInZone;
	[SerializeField] private Animator animator;
	[SerializeField] private Transform trans;

	private EnemyNPCData sourceData;
	private EnemyNPCInstanceLink instanceLink;

	private int damage;
	private int maxHealth;
	private int currentHealth;
	private bool isAlive;
	private bool isAggro;
	private float aggroRadius;
	private int numberOfDeathAnimations;
	private int despawnTimer;
	private static int defaultDespawnTime = 50 * 60;        // 50 frames * seconds
	private static int noLootDespawnTime = 50 * 5;	
	private int respawnTimer;
	private Vector3 spawnPoint;
	private EnemyState currentState;

	private static CharacterManager character;

	// Dropped Items
	[HideInInspector] public int goldDropped;
	[HideInInspector] public List<InventoryItemData> itemsDropped;
	[HideInInspector] public int healthPotionsDropped;
	[HideInInspector] public int energyPotionsDropped;


	#region Start and Update
	private void Start () {

		// Find InstanceLink from zone list in WorldManager
		instanceLink = WorldManager.instance.FindInstanceLink (indexInZone);

		instanceLink.instanceManager = this;
		sourceData = (EnemyNPCData)instanceLink.sourceData;
		itemsDropped = new List<InventoryItemData> ();

		// This guy should be dead
		if (instanceLink.active == false) {
			Despawn ();
			return;
		}

		character = CharacterManager.instance;

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
			}
		}
	}

	private void Update () {

		if (isAlive == false)
			return;

		if (currentState == EnemyState.Patrol) {
			Patrol ();
		}
		else if (currentState == EnemyState.Attack) {
			Attack ();
		}
		else if (currentState == EnemyState.Chase) {
			Chase ();
		} else {
			Debug.Log ("Unknown State Found: " + currentState);
		}

	}
	#endregion

	#region Target & Mouse Interactions
	public override void HandleRightMouseClick () {
		
		if (isAlive == false) {
			GUIManager.instance.lootWindow.SetLoot (this);
		}

	}
	public override void HandleMouseOver () {

		if (isAlive) {
			GUIManager.instance.cursorManager.SetCombatCursor ();
		}
		else {
			GUIManager.instance.cursorManager.SetLootCursor ();
		}
	}
	public override void HandleMouseExit () {
		GUIManager.instance.cursorManager.SetDefaultCursor ();
	}
	public bool IsThisTheCurrentTarget () {
		return TargetManager.instance.GetTarget () == this;
	}
	#endregion

	#region Spawn and Despawn
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
		isAggro = false;
		damage = sourceData.damage;
		maxHealth = sourceData.baseHealth;
		currentHealth = maxHealth;
		numberOfDeathAnimations = sourceData.numberOfDeathAnimations;
		respawnTimer = instanceLink.respawnTimer;
		aggroRadius = sourceData.aggroRadius;

		StartCoroutine (CheckForAggro ());

		SetState (EnemyState.Patrol);
		gameObject.SetActive (true);
	}

	void KillNPC () {

		if (isAlive == false)
			return;

		isAlive = false;

		// Play dying animation
		animator.SetInteger ("DeathValue", Random.Range (1, numberOfDeathAnimations + 1));
		StartCoroutine (ResetDeathValue ());

		// Get drops from loot table
		DetermineDrops ();

		if (HasItemsToLoot ()) {
			despawnTimer = defaultDespawnTime;
		} else {
			despawnTimer = noLootDespawnTime;
		}
	}

	IEnumerator ResetDeathValue () {
		yield return null;
		animator.SetInteger ("DeathValue", 0);
	}


	#endregion

	#region Loot Functions
	void DetermineDrops () {

		goldDropped = Mathf.RoundToInt (Random.Range (sourceData.lootTable.goldDrop_Min, sourceData.lootTable.goldDrop_Max) * sourceData.goldDropMultiplier);

		float rng = Random.Range (0f, 100f);
		if (rng < sourceData.lootTable.healthPotionChance) {
			healthPotionsDropped = 1;
		}
		else {
			healthPotionsDropped = 0;
		}

		rng = Random.Range (0f, 100f);
		if (rng < sourceData.lootTable.energyPotionChance) {
			energyPotionsDropped = 1;
		}
		else {
			energyPotionsDropped = 0;
		}

		itemsDropped.Clear ();
		rng = Random.Range (0f, 100f);
		if (rng < sourceData.lootTable.list1DropChance) {
			itemsDropped.Add (sourceData.lootTable.list1 [Random.Range (0, sourceData.lootTable.list1.Length)]);
		}

		if (rng < sourceData.lootTable.list2DropChance) {
			itemsDropped.Add (sourceData.lootTable.list2 [Random.Range (0, sourceData.lootTable.list2.Length)]);
		}

		if (rng < sourceData.lootTable.list3DropChance) {
			itemsDropped.Add (sourceData.lootTable.list3 [Random.Range (0, sourceData.lootTable.list3.Length)]);
		}
	}

	bool HasItemsToLoot () {
		if (goldDropped > 0)
			return true;
		if (healthPotionsDropped > 0)
			return true;
		if (energyPotionsDropped > 0)
			return true;
		if (itemsDropped.Count > 0)
			return true;

		return false;
	}



	#endregion

	#region Combat Functions
	private void RunCombatSequence () {


	}

	IEnumerator CheckForAggro () {

		while (isAggro == false) {

			yield return Common.oneSecond_WFS;

			// Check distance to aggro
			float d = Vector3.Distance (trans.position, character.trans.position);

			if (d < aggroRadius) {
				Aggro ();
			}
		}
	}

	public void Aggro () {

		isAggro = true;
		character.SetInCombat ();

		SetState (EnemyState.Chase);

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

	public string GetHealthText () {
		return currentHealth + "/" + maxHealth;
	}
	public float GetHealthPercent () {
		return (float)currentHealth / maxHealth;
	}
	#endregion


	#region State Functions
	private void Patrol () {

	}
	private void Chase () {

		// Check to see if we're in attack range

	}
	private void Attack () {

	}

	#endregion

	#region Misc
	public void SetState (EnemyState state) {
		currentState = state;
	}

	public override NPCData GetSourceData () {
		return sourceData;
	}
	#endregion
}

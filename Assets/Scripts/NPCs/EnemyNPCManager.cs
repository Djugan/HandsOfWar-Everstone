using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNPCManager : NPCManager {

	[SerializeField] private int indexInZone;
	[SerializeField] private Animator animator;
	[SerializeField] private Transform trans;
	[SerializeField] private NavMeshAgent navMeshAgent;

	private EnemyNPCData sourceData;
	private EnemyNPCInstanceLink instanceLink;

	private int maxHealth;
	private int currentHealth;
	private bool isAlive;
	private bool isAggro;
	private bool isAttacking;
	private float aggroRadius;
	private int numberOfDeathAnimations;
	private int despawnTimer;
	private EnemyAbility nextAbility;   // The ability the character wants to use next     
	private EnemyAbility savedAbility;  // The ability that the character just used

	private EnemyAbility ability1, ability2, ability3, ability4;
   
	private float ability1Cooldown, ability2Cooldown, ability3Cooldown, ability4Cooldown;

	private static int defaultDespawnTime = 50 * 60;        // 50 frames * seconds
	private static int noLootDespawnTime = 50 * 5;
	private static float turnSpeed = 5f;
	private static float moveSpeed = 4f;
	private static float leashDistance = 30f;
	private static float defaultStoppingDistance = 5f;

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
		instanceLink = WorldManager.instance.FindInstanceLink_EnemyNPC (indexInZone);

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

		if (isAlive) {
			UpdateCooldowns ();
		}
		else {

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
		}
		else if (currentState == EnemyState.ReturnHome) {
			ReturnHome ();
		}
		else {
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

		// Create EnemyAbility objects from the EnemyAbilityData source objects
		if (sourceData.ability1 != null) {
			ability1 = new EnemyAbility (sourceData.ability1, sourceData.abilityValue1);
		}
		if (sourceData.ability2 != null) {
			ability2 = new EnemyAbility (sourceData.ability2, sourceData.abilityValue2);
		}
		if (sourceData.ability3 != null) {
			ability3 = new EnemyAbility (sourceData.ability3, sourceData.abilityValue3);
		}
		if (sourceData.ability4 != null) {
			ability4 = new EnemyAbility (sourceData.ability4, sourceData.abilityValue4);
		}

		nextAbility = ability1;

		transform.position = spawnPoint;
		isAlive = true;
		SetAggro (false);
		maxHealth = sourceData.baseHealth;
		currentHealth = maxHealth;
		numberOfDeathAnimations = sourceData.numberOfDeathAnimations;
		respawnTimer = instanceLink.respawnTimer;
		aggroRadius = sourceData.aggroRadius;
		spawnPoint = transform.position;
		navMeshAgent.speed = moveSpeed;
		navMeshAgent.stoppingDistance = nextAbility.GetAbilityRange ();
		isAttacking = false;
		animator.SetInteger (HashIDs.deathValue_int, 0);

		gameObject.SetActive (true);

		SetState (EnemyState.Patrol);
		StartCoroutine (SetNextAbility ());
	}

	void KillNPC () {

		if (isAlive == false)
			return;

		isAlive = false;

		// Play dying animation
		int deathAnimationNumber = Random.Range (1, numberOfDeathAnimations + 1);
		animator.SetInteger (HashIDs.deathValue_int, deathAnimationNumber);

		// Get drops from loot table
		DetermineDrops ();

		if (HasItemsToLoot ()) {
			despawnTimer = defaultDespawnTime;
		} else {
			despawnTimer = noLootDespawnTime;
		}

		GUIManager.instance.mainMenu.questsMenu.CheckForObjective_Kill (sourceData);
	}

	public override bool IsDead () {
		return !isAlive;
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

		SetAggro (true);
		character.SetInCombat ();

		SetState (EnemyState.Chase);

	}
	private void SetAggro (bool value) {

		animator.SetBool (HashIDs.isAggro_bool, value);
		isAggro = value;
	}

	public override void ReceiveDamage (int amount) {

		if (currentState == EnemyState.ReturnHome)
			return;

		if (currentState == EnemyState.Patrol)
			Aggro ();

		currentHealth -= amount;
		currentHealth = Mathf.Clamp (currentHealth, 0, maxHealth);

		ScrollingTextManager.instance.ShowScrollingText (amount, HandsOfWarColors.yellow, CharacterManager.instance.trans.position, trans.position);

		// Update target display
		if (IsThisTheCurrentTarget ()) {
			GUIManager.instance.targetFrameManager.SetTargetHealth ();
		}

		if (currentHealth <= 0) {

			KillNPC ();
		}
	}

	/// <summary>
	/// Sets the next ability that this agent intends to use
	/// </summary>
	private WaitForSeconds oneSecond = new WaitForSeconds (1f);
	IEnumerator SetNextAbility () {

		while (isAlive) {

			yield return oneSecond;

			// Figure out what ability to use next
			// Called once per second regardless of the state the enemy is in
			if (isAggro) {

				// Get the abilities this enemy has access to currently
				EnemyAbility directHeal = GetDirectHealAbility ();
				EnemyAbility directDamage = GetDirectDamageAbility ();


				// Heal if low health
				if (GetHealthPercent () < 0.4f && directHeal != null) {
					nextAbility = directHeal;
				}

				// Deal direct damage
				else if (directDamage != null){
					nextAbility = directDamage;
				}
			
				navMeshAgent.stoppingDistance = nextAbility.GetAbilityRange ();

			}
		}
	}

	private EnemyAbility GetDirectHealAbility () {
		if (ability1 != null && 
			ability1.GetSourceAbility().abilityType == AbilityType.DirectHeal &&
			ability1.IsOnCooldown () == false) {
			return ability1;
		}
		if (ability2 != null 
			&& ability2.GetSourceAbility ().abilityType == AbilityType.DirectHeal &&
			ability2.IsOnCooldown () == false) {
			return ability2;
		}
		if (ability3 != null && 
			ability3.GetSourceAbility ().abilityType == AbilityType.DirectHeal &&
			ability3.IsOnCooldown () == false) {
			return ability3;
		}

		if (ability4 != null && 
			ability4.GetSourceAbility ().abilityType == AbilityType.DirectHeal &&
			ability4.IsOnCooldown () == false) {
			return ability4;
		}

		return null;
	}

	private EnemyAbility GetDirectDamageAbility () {

		EnemyAbility bestAbility = null;
		int bestDamage = 0;

		if (ability1 != null &&
			ability1.GetSourceAbility ().abilityType == AbilityType.DirectDamage &&
			ability1.IsOnCooldown () == false &&
			ability1.GetAbilityValue () > bestDamage) {

			bestAbility = ability1;
			bestDamage = bestAbility.GetAbilityValue ();
		}

		if (ability2 != null &&
			ability2.GetSourceAbility ().abilityType == AbilityType.DirectDamage &&
			ability2.IsOnCooldown () == false &&
			ability2.GetAbilityValue () > bestDamage) {

			bestAbility = ability2;
			bestDamage = bestAbility.GetAbilityValue ();
		}

		if (ability3 != null &&
			ability3.GetSourceAbility ().abilityType == AbilityType.DirectDamage &&
			ability3.IsOnCooldown () == false &&
			ability3.GetAbilityValue () > bestDamage) {

			bestAbility = ability3;
			bestDamage = bestAbility.GetAbilityValue ();
		}

		if (ability4 != null &&
			ability4.GetSourceAbility ().abilityType == AbilityType.DirectDamage &&
			ability4.IsOnCooldown () == false &&
			ability4.GetAbilityValue () > bestDamage) {

			bestAbility = ability4;
			bestDamage = bestAbility.GetAbilityValue ();
		}

		return bestAbility;
	}

	private void UpdateCooldowns () {

		if (ability1 != null && ability1.IsOnCooldown ()) {
			ability1.UpdateCooldown ();
		}
		if (ability2 != null && ability2.IsOnCooldown ()) {
			ability2.UpdateCooldown ();
		}
		if (ability3 != null && ability3.IsOnCooldown ()) {
			ability3.UpdateCooldown ();
		}
		if (ability4 != null && ability4.IsOnCooldown ()) {
			ability4.UpdateCooldown ();
		}
	}

	public string GetHealthText () {
		return currentHealth + "/" + maxHealth;
	}
	public float GetHealthPercent () {
		return (float)currentHealth / maxHealth;
	}

	/// <summary>
	/// Called from the animation when the attack/animation should register/trigger the effect
	/// </summary>
	public void TriggerDamage () {

		int damage = savedAbility.GetAbilityValue () * -1;

		// Apply armor, magic resist, etc to damage
		CharacterManager.instance.ChangeHealth (damage);
	}

	/// <summary>
	/// Called from the animation when the attack/animation is finished
	/// </summary>
	public void FinishAttack () {
		isAttacking = false;
		animator.SetInteger (HashIDs.attackValue_int, 0);


	}
	#endregion

	#region State Functions
	private void Patrol () {

	}
	private void Chase () {


		Vector3 towardsPlayer = character.trans.position - trans.position;
		Vector3 towardsSpawnPoint = trans.position - spawnPoint;
		//FacePlayer ();

		// Are we close enough to attack -> Attack state
		if (towardsPlayer.magnitude < nextAbility.GetAbilityRange ()) {
			animator.SetBool (HashIDs.isMoving_bool, false);
			SetState (EnemyState.Attack);
		}

		// Are we too far from our original spawn point -> Return Home state
		else if (towardsSpawnPoint.magnitude > leashDistance) {
			ReceiveDamage (-1000000);
			SetState (EnemyState.ReturnHome);
		}

		// Move closer to player
		else {

			animator.SetBool (HashIDs.isMoving_bool, true);

			navMeshAgent.SetDestination (character.trans.position);
		}
	}

	private void Attack () {

		Vector3 towardsPlayer = character.trans.position - trans.position;
		FacePlayer ();

		// Is the player too far away -> Chase state
		if (towardsPlayer.magnitude > nextAbility.GetAbilityRange ()) {
			SetState (EnemyState.Chase);
			return;
		}

		if (isAttacking == false) {

			savedAbility = nextAbility;

			// Put the abililty on cooldown
			savedAbility.SetCooldown ();

			// Begin playing attack animation
			animator.SetInteger (HashIDs.attackValue_int, nextAbility.GetAnimationNumber ());
			isAttacking = true;
		}
	}


	private void ReturnHome () {

		// Face our destination
		Vector3 towardsHome = spawnPoint - trans.position;

		// Have we reached our original spawn point -> Patrol state
		if (towardsHome.magnitude < 2f) {
			animator.SetBool (HashIDs.isMoving_bool, false);
			SetState (EnemyState.Patrol);
			return;
		}

		navMeshAgent.SetDestination (spawnPoint);
	}

	#endregion

	#region Misc
	public void SetState (EnemyState state) {
		currentState = state;

		if (currentState == EnemyState.Patrol) {
			SetAggro (false);

			StartCoroutine (CheckForAggro ());
		}
	}

	public override NPCData GetSourceData () {
		return sourceData;
	}

	private void FacePlayer () {
		Vector3 towardsPlayer = character.trans.position - trans.position;
		towardsPlayer.y = 0f;

		Quaternion targetRotation = Quaternion.LookRotation (towardsPlayer);
		trans.rotation = Quaternion.Lerp (trans.rotation, targetRotation, turnSpeed * Time.deltaTime);
	}
	#endregion
}

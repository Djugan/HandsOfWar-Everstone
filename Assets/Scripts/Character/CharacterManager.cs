using UnityEngine;
using System.Collections;

public class CharacterManager : MonoBehaviour
{
	public static CharacterManager instance;

	public CharacterMovementManager movementManager;
	public int weaponType;
	public bool inCombat;
	public Transform trans;

	private RuntimeAnimatorController runtimeAnimController;
	[SerializeField] private AnimationClip [] combatIdleAnims;
	[SerializeField] private AnimationClip [] meleeAttack1_Anims;
	[SerializeField] private AnimationClip [] meleeAttack2_Anims;
	[SerializeField] private AnimationClip [] meleeAttack3_Anims;
	[SerializeField] private AnimationClip [] meleeAttack4_Anims;
	[SerializeField] private AnimationClip [] meleeAttack5_Anims;
	[SerializeField] private GameObject [] weapons;

	[Header ("Attributes")]
	[HideInInspector]
	public string playerName;
	public PlayerClass playerClass;

	public int maxHealth, currentHealth;
	public int maxEnergy, currentEnergy;

	private int strength_base, strength_equipment;
	private int intellect_base, intellect_equipment;
	private int agility_base, agility_equipment;
	private int stamina_base, stamina_equipment;
	private int dexterity_base, dexterity_equipment;
	private int focus_base, focus_equipment;
	private int haste_base, haste_equipment;
	private int speed_base, speed_equipment;
	private int armor_base, armor_equipment;
	private int magicResist_base, magicResist_equipment;

	private int healthRegen, energyRegen;
	private int moveSpeed;

	private AbilityData activeAbility;

	// Statistics
	[HideInInspector]
	public int currentExp, expToNextLevel;
	public int level;

	// Misc
	private int regenTimer;
	private AnimatorOverrideController animationOverrideController;

	private void Awake () {

		// Has the singleton not been created yet
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (gameObject);

			// Initialize Key Bindings
			KeybindingManager.Init ();
		}
		else {
			Destroy (gameObject);
		}
	}

	void Start () {
		movementManager = GetComponent<CharacterMovementManager> ();
		inCombat = false;
		weaponType = 1;
		regenTimer = 0;

		runtimeAnimController = movementManager.animator.runtimeAnimatorController;
		animationOverrideController = new AnimatorOverrideController ();
		animationOverrideController.runtimeAnimatorController = runtimeAnimController;

		SetInitialAttributeValues ();


		GUIManager.instance.actionBar.SetAbilityInSlot (AbilityDatabase.GetAbility (1), 0);
		GUIManager.instance.actionBar.SetAbilityInSlot (AbilityDatabase.GetAbility (2), 1);
	}

	#region Get/Set Attributes
	void SetInitialAttributeValues () {
		strength_base = 10;
		intellect_base = 10;
		agility_base = 10;
		stamina_base = 10;
		dexterity_base = 10;
		focus_base = 10;
		haste_base = 10;
		speed_base = 10;

		SetMaxHealth ();
		currentHealth = maxHealth;

		SetMaxEnergy ();
		currentEnergy = maxEnergy;

		SetHealthRegen ();
	}

	public int GetTotalStrength () {
		return strength_base + strength_equipment;
	}
	public int GetTotalIntellect () {
		return intellect_base + intellect_equipment;
	}
	public int GetTotalAgility () {
		return agility_base + agility_equipment;
	}
	public int GetTotalStamina () {
		return stamina_base + stamina_equipment;
	}
	public int GetTotalDexterity () {
		return dexterity_base + dexterity_equipment;
	}
	public int GetTotalFocus () {
		return focus_base + focus_equipment;
	}
	public int GetTotalHaste () {
		return haste_base + haste_equipment;
	}
	public int GetTotalSpeed () {
		return speed_base + speed_equipment;
	}
	public int GetTotalArmor () {
		return armor_base + armor_equipment;
	}
	public int GetTotalMagicResist () {
		return magicResist_base + magicResist_equipment;
	}
	public int GetHealthRegen () {
		return healthRegen;
	}
	public int GetEnergyRegen () {
		return energyRegen;
	}
	public int GetMoveSpeed () {
		return 10;
	}
	public int GetCritChance () {
		return 10;
	}
	public int GetCritBonus () {
		return 10;
	}
	public int GetHastePercent () {
		return 10;
	}
	public int GetDodge () {
		return 10;
	}
	public int GetDPS () {
		return 10;
	}

	public void AddEquipmentAttributes (InventoryItemData item) {
		strength_equipment += item.strength;
		intellect_equipment += item.intelligence;
		agility_equipment += item.agility;
		stamina_equipment += item.stamina;
		dexterity_equipment += item.dexterity;
		focus_equipment += item.focus;
		haste_equipment += item.haste;
		speed_equipment += item.speed;

		SetAllVitals ();
	}
	public void RemoveEquipmentAttributes (InventoryItemData item) {
		strength_equipment -= item.strength;
		intellect_equipment -= item.intelligence;
		agility_equipment -= item.agility;
		stamina_equipment -= item.stamina;
		dexterity_equipment -= item.dexterity;
		focus_equipment -= item.focus;
		haste_equipment -= item.haste;
		speed_equipment -= item.speed;

		SetAllVitals ();
	}
	#endregion

	#region Stats Formulas
	void SetAllVitals () {
		SetMaxHealth ();
		SetMaxEnergy ();
		SetHealthRegen ();
		SetEnergyRegen ();
		SetMoveSpeed ();
		SetCrit ();
		SetDPS ();
	}

	void SetMaxHealth () {

		int health = 30 + (10 * level);

		health += GetTotalStamina () * 2;

		maxHealth = health;

		if (currentHealth > maxHealth)
			currentHealth = maxHealth;
	}

	void SetMaxEnergy () {

		int energy = 30 + (10 * level);
		energy += GetTotalFocus () * 2;

		maxEnergy = energy;

		if (currentEnergy > maxEnergy)
			currentEnergy = maxEnergy;
	}

	void SetHealthRegen () { 
		float amount = 2 + (0.2f * level);
		amount += GetTotalStamina () * 0.5f;

		healthRegen = Mathf.RoundToInt (amount);
	}
	void SetEnergyRegen () {
		float amount = 2 + (0.2f * level);
		amount += GetTotalFocus () * 0.5f;

		energyRegen = Mathf.RoundToInt (amount);
	}
	void SetMoveSpeed () {
		moveSpeed = 5;

		movementManager.WalkSpeed = moveSpeed;
	}
	void SetCrit () {

	}
	void SetDPS () {

	}
	#endregion

	#region Combat Functions
	public void CheckForAbilities () {

		// Checks for pressing 1-0
		for (int i = 0; i < 10; i++) {
			if (Input.GetKeyDown (KeybindingManager.actionBarKeys [i])) {
				BeginAbility (i);
			}
		}

	}
	/// <summary>
	/// Adds the amount to the current health
	/// </summary>
	public void ChangeHealth (int amount, bool showScrollingText = true) {

		currentHealth += amount;

		if (currentHealth < 0) {
			currentHealth = 0;
		}
		else if (currentHealth > maxHealth) {
			currentHealth = maxHealth;
		}

		GUIManager.instance.playerUnitFrame.SetHealthBar ();

		if (showScrollingText) { 

			Color textColor = HandsOfWarColors.red;
			if (amount > 0) {
				textColor = HandsOfWarColors.green;
			}
			ScrollingTextManager.instance.ShowScrollingText (amount, textColor, Vector3.zero, trans.position);
		}
	}

	public void SetInCombat () {
		inCombat = true;
		movementManager.animator.SetBool (HashIDs.inCombat_bool, inCombat);
	}

	/// <summary>
	/// Adds the amount to the current energy
	/// </summary>
	public void ChangeEnergy (int amount) {

		currentEnergy += amount;

		if (currentEnergy < 0) {
			currentEnergy = 0;
		} else if (currentEnergy > maxEnergy) {
			currentEnergy = maxEnergy;
		}

		GUIManager.instance.playerUnitFrame.SetEnergyBar ();
	}

	public void BeginAbility (int actionBarSlotNumber) {

		activeAbility = GUIManager.instance.actionBar.GetAbilityInSlot (actionBarSlotNumber);

		if (activeAbility == null) {
			print ("No ability in that slot");
			return;
		}

		if (activeAbility.requiresTarget) { 
			if (TargetManager.instance.HasTarget () == false) {
				print ("You need to target something");
				return;
			}
			float distanceToTarget = TargetManager.instance.GetDistanceToTarget ();
			if (distanceToTarget > activeAbility.range) {
				print ("Target out of range");
				return;
			}
		}

		if (activeAbility.harmfulAbility) { 
			if (TargetManager.instance.IsTargetAnEnemy () == false) {
				print ("You need to target an enemy");
				return;
			}

			if (TargetManager.instance.IsTargetDead ()) {
				print ("You can't attack a dead thing");
				return;
			}
		}

		// Send the attack number value to the animator
		movementManager.animator.SetInteger (HashIDs.attackNumber_int, activeAbility.animationNumber);

		StartCoroutine (ResetAttackNumber ());
	}

	IEnumerator ResetAttackNumber () {

		// Wait a frame and set the attack number back to 0
		yield return null;
		movementManager.animator.SetInteger (HashIDs.attackNumber_int, 0);
	}

	public void TriggerDamage () {

		print ("Called Trigger Damage. Active Ability is: " + activeAbility.abilityName);
		TargetManager.instance.DamageTarget (activeAbility.damage);

	}

	#endregion

	void Update () {

		if (Input.GetKeyDown (KeyCode.T)) {
			TargetManager.instance.DamageTarget (500);
		}

		// Regen


		#region Weapon Switch Testing
		//Set weapon type to 1
		if (Input.GetKeyDown (KeyCode.Keypad1)) {        //1h & shield
			SetWeaponType (1);
			weapons [0].SetActive (true);
			weapons [1].SetActive (true);
		}
		else if (Input.GetKeyDown (KeyCode.Keypad2)) {   //2h
			SetWeaponType (2);
			weapons [2].SetActive (true);
		}
		else if (Input.GetKeyDown (KeyCode.Keypad3)) {   //dual wield
			SetWeaponType (3);
			weapons [3].SetActive (true);
			weapons [4].SetActive (true);
		}
		else if (Input.GetKeyDown (KeyCode.Keypad4)) {   //bow
			SetWeaponType (4);
			weapons [5].SetActive (true);
		}
		else if (Input.GetKeyDown (KeyCode.Keypad5)) {   //staff
			SetWeaponType (5);
			weapons [6].SetActive (true);
		}

		
		
		#endregion
		CheckForAbilities ();
	}

	private void FixedUpdate () {
		regenTimer++;
		if (regenTimer == 50) { 
			regenTimer = 0;
			ChangeHealth (Mathf.RoundToInt (healthRegen), false);
			ChangeEnergy (Mathf.RoundToInt (energyRegen));
		}
	}

	void SetWeaponType (int t) {

		//hide all weapons
		for (int i = 0; i < weapons.Length; i++) {
			weapons [i].SetActive (false);
		}

		weaponType = t;

		// Switch Combat Idle Animation
		animationOverrideController ["1H_COMBAT_mode"] = combatIdleAnims [weaponType - 1];

		// Switch Melee Attack 1-6 Animations
		animationOverrideController ["1H_sword_swing_high_right"] =			meleeAttack1_Anims [weaponType - 1];
		animationOverrideController ["1H_sword_thrust_mid"] =				meleeAttack2_Anims [weaponType - 1];
		animationOverrideController ["1H_sword_swing_high_straight_down"] = meleeAttack3_Anims [weaponType - 1];
		animationOverrideController ["1H_sword_swing_low_left"] =			meleeAttack4_Anims [weaponType - 1];
		animationOverrideController ["1H_Jump_Swing"] =						meleeAttack5_Anims [weaponType - 1];

		movementManager.animator.runtimeAnimatorController = animationOverrideController;
	}
}

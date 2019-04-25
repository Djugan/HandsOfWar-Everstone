using UnityEngine;
using System.Collections;

public class CharacterManager : MonoBehaviour
{

	public static CharacterManager instance;

	public CharacterMovementManager movementManager;
	public int weaponType;
	public bool inCombat;

	private RuntimeAnimatorController runtimeAnimController;
	public AnimationClip [] combatIdleAnims;
	public GameObject [] weapons;

	[Header ("Attributes")]
	[HideInInspector]
	public string playerName;
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

	// Statistics
	[HideInInspector]
	public int currentExp, expToNextLevel;
	public int level;

	// Misc
	private int regenTimer;

	private void Awake () {

		// Has the singleton not been created yet
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (gameObject);
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

		SetInitialAttributeValues ();
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
	/// <summary>
	/// Adds the amount to the current health
	/// </summary>
	public void ChangeHealth (int amount) {

		currentHealth += amount;

		if (currentHealth < 0) {
			currentHealth = 0;
		} else if (currentHealth > maxHealth) {
			currentHealth = maxHealth;
		}

		GUIManager.instance.playerUnitFrame.SetHealthBar ();
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

	#endregion

	void Update () {

		if (Input.GetKeyDown (KeyCode.T)) {
			TargetManager.instance.DamageTarget (500);
		}

		// Regen


		#region Weapon Switch Testing
		//Set weapon type to 1
		if (Input.GetKeyDown (KeyCode.Alpha1)) {        //1h & shield
			SetWeaponType (1);
			weapons [0].SetActive (true);
			weapons [1].SetActive (true);
		}
		else if (Input.GetKeyDown (KeyCode.Alpha2)) {   //2h
			SetWeaponType (2);
			weapons [2].SetActive (true);
		}
		else if (Input.GetKeyDown (KeyCode.Alpha3)) {   //dual wield
			SetWeaponType (3);
			weapons [3].SetActive (true);
			weapons [4].SetActive (true);
		}
		else if (Input.GetKeyDown (KeyCode.Alpha4)) {   //bow
			SetWeaponType (4);
			weapons [5].SetActive (true);
		}
		else if (Input.GetKeyDown (KeyCode.Alpha5)) {   //staff
			SetWeaponType (5);
			weapons [6].SetActive (true);
		}

		if (Input.GetKeyDown (KeyCode.C)) {
			if (inCombat) {
				inCombat = false;
			}
			else {
				inCombat = true;
			}
			movementManager.animator.SetBool ("InCombat", inCombat);
		}
		#endregion

	}

	private void FixedUpdate () {
		regenTimer++;
		if (regenTimer == 50) { 
			regenTimer = 0;
			ChangeHealth (Mathf.RoundToInt (healthRegen));
			ChangeEnergy (Mathf.RoundToInt (energyRegen));
		}
	}

	void SetWeaponType (int t) {

		//hide all weapons
		for (int i = 0; i < weapons.Length; i++) {
			weapons [i].SetActive (false);
		}

		weaponType = t;
		movementManager.animator.SetInteger ("WeaponType", weaponType);

		AnimatorOverrideController myOverrideController = new AnimatorOverrideController ();
		myOverrideController.runtimeAnimatorController = runtimeAnimController;
		myOverrideController ["1H_COMBAT_mode"] = combatIdleAnims [weaponType - 1];

		movementManager.animator.runtimeAnimatorController = myOverrideController;
	}
}

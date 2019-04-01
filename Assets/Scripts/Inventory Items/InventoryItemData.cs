using UnityEngine;
using UnityEditor;
using System.Collections;
using System;

[Serializable]
public class InventoryItemData : ScriptableObject {

	public string itemName;
	public int itemID;
	public Sprite icon;

	//Equipment Specific Variables
	public bool isEquippable = true;
	public Rarity rarity;
	public Slot slot;
	public int itemLevel;
	public int armor;
	public int magicResist;
	public int strength;
	public int agility;
	public int intelligence;
	public int stamina;
	public int dexterity;
	public int focus;
	public int haste;
	public int speed;
	public int damage;
	public int delay;
	public int block;
	public ItemSet itemSet;
	public string flavorText = "";

	//Quest Items
	public bool isQuestItem;

	//Misc Items
	public bool isStackable = false;
	public bool isConsumable = false;
}
	

#if UNITY_EDITOR
[CustomEditor (typeof (InventoryItemData))]
public class InventoryItemEditor : Editor
{

	SerializedProperty itemName;
	SerializedProperty itemID;
	SerializedProperty icon;
	SerializedProperty isEquippable;
	SerializedProperty rarity;
	SerializedProperty slot;
	SerializedProperty itemLevel;
	SerializedProperty armor;
	SerializedProperty magicResist;
	SerializedProperty strength;
	SerializedProperty agility;
	SerializedProperty intelligence;
	SerializedProperty stamina;
	SerializedProperty dexterity;
	SerializedProperty focus;
	SerializedProperty haste;
	SerializedProperty speed;
	SerializedProperty damage;
	SerializedProperty delay;
	SerializedProperty block;
	SerializedProperty itemSet;
	SerializedProperty flavorText;
	SerializedProperty isQuestItem;
	SerializedProperty isStackable;
	SerializedProperty isConsumable;

	void OnEnable () {
		itemName = serializedObject.FindProperty ("itemName");
		itemID = serializedObject.FindProperty ("itemID");
		icon = serializedObject.FindProperty ("icon");
		isEquippable = serializedObject.FindProperty ("isEquippable");
		rarity = serializedObject.FindProperty ("rarity");
		slot = serializedObject.FindProperty ("slot");
		itemLevel = serializedObject.FindProperty ("itemLevel");
		armor = serializedObject.FindProperty ("armor");
		magicResist = serializedObject.FindProperty ("magicResist");
		strength = serializedObject.FindProperty ("strength");
		agility = serializedObject.FindProperty ("agility");
		intelligence = serializedObject.FindProperty ("intelligence");
		stamina = serializedObject.FindProperty ("stamina");
		dexterity = serializedObject.FindProperty ("dexterity");
		focus = serializedObject.FindProperty ("focus");
		haste = serializedObject.FindProperty ("haste");
		speed = serializedObject.FindProperty ("speed");
		damage = serializedObject.FindProperty ("damage");
		delay = serializedObject.FindProperty ("delay");
		block = serializedObject.FindProperty ("block");
		itemSet = serializedObject.FindProperty ("itemSet");
		flavorText = serializedObject.FindProperty ("flavorText");
		isQuestItem = serializedObject.FindProperty ("isQuestItem");
		isStackable = serializedObject.FindProperty ("isStackable");
		isConsumable = serializedObject.FindProperty ("isConsumable");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update ();
		EditorGUI.BeginChangeCheck();
		InventoryItemData item = target as InventoryItemData;

		//test1.intValue = EditorGUILayout.IntField ("Item ID", item.itemID);

		itemName.stringValue = EditorGUILayout.TextField("Item Name", item.itemName);
		itemID.intValue = EditorGUILayout.IntField ("Item ID", item.itemID);
		icon.objectReferenceValue = EditorGUILayout.ObjectField("Icon", item.icon, typeof (Sprite), true) as Sprite;
		isEquippable.boolValue = GUILayout.Toggle(item.isEquippable, "Is Equippable");
		isQuestItem.boolValue = GUILayout.Toggle(item.isQuestItem, "Is Quest Item");
		isStackable.boolValue = GUILayout.Toggle(item.isStackable, "Is Stackable");
		isConsumable.boolValue = GUILayout.Toggle(item.isConsumable, "Is Consumable");
		

		if (isEquippable.boolValue) {
			SetNonQuestItem (item);		//Equipment can't be used in quests
			SetNonStackable (item);		//Equipment can't be stackable
			SetNonConsumable (item);	//Equipment can't be consumed

			EditorGUILayout.PropertyField (rarity);
			EditorGUILayout.PropertyField (slot);

			itemLevel.intValue = EditorGUILayout.IntField ("Item Level", item.itemLevel);
			armor.intValue = EditorGUILayout.IntField ("Armor", item.armor);
			magicResist.intValue = EditorGUILayout.IntField ("Magic Resist", item.magicResist);
			strength.intValue = EditorGUILayout.IntField ("Strength", item.strength);
			agility.intValue = EditorGUILayout.IntField ("Agility", item.agility);
			intelligence.intValue = EditorGUILayout.IntField ("Intelligence", item.intelligence);
			stamina.intValue = EditorGUILayout.IntField ("Stamina", item.stamina);
			dexterity.intValue = EditorGUILayout.IntField ("Dexterity", item.dexterity);
			focus.intValue = EditorGUILayout.IntField ("Focus", item.focus);
			haste.intValue = EditorGUILayout.IntField ("Haste", item.haste);
			speed.intValue = EditorGUILayout.IntField ("Speed", item.speed);

			if (item.slot == Slot.Weapon1H || item.slot == Slot.Weapon2H) {
				damage.intValue = EditorGUILayout.IntField ("Damage", item.damage);
				delay.intValue = EditorGUILayout.IntField ("Delay", item.delay);
			} 
			if (item.slot == Slot.OffHand) {
				block.intValue = EditorGUILayout.IntField ("Block", item.block);
			}

			itemSet.objectReferenceValue = EditorGUILayout.ObjectField ("Item Set", item.itemSet, typeof (ItemSet), true) as ItemSet;
			flavorText.stringValue = EditorGUILayout.TextField ("Flavor Text", item.flavorText);

		}
		if (isQuestItem.boolValue) {
			SetNonEquippable (item);	//Quest items can't be equipped
			SetNonConsumable (item);	//Quest items can't be consumed

			//item.usedInQuest = EditorGUILayout.ObjectField ("Quest", item.usedInQuest, typeof (Quest), true) as Quest;
		}
		
		AssetDatabase.RenameAsset (AssetDatabase.GetAssetPath (Selection.activeObject), item.itemID + "_" + item.itemName);
		
		if(EditorGUI.EndChangeCheck())
			serializedObject.ApplyModifiedProperties();
	}
	void SetNonEquippable (InventoryItemData item) {
		item.isEquippable = false;
	}

	void SetNonQuestItem (InventoryItemData item) {
		item.isQuestItem = false;
	}

	void SetNonStackable (InventoryItemData item) {
		item.isStackable = false;
	}

	void SetNonConsumable (InventoryItemData item) {
		item.isConsumable = false;
	}
}
#endif


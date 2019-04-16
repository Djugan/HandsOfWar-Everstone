using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootTable : ScriptableObject {

	public int goldDrop_Min;
	public int goldDrop_Max;

	public int healthPotionChance;
	public int energyPotionChance;

	public float list1DropChance;
	public InventoryItemData [] list1;

	public float list2DropChance;
	public InventoryItemData [] list2;

	public float list3DropChance;
	public InventoryItemData [] list3;

	
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ItemSet : ScriptableObject {

	public int setID;
	public string setName;

	public InventoryItemData[] setItems = new InventoryItemData[5];

	public int armor3P, magicResist3P, strength3P, agility3P, intelligence3P,
				stamina3P, dexterity3P, focus3P, haste3P;

	public int armor4P, magicResist4P, strength4P, agility4P, intelligence4P,
				stamina4P, dexterity4P, focus4P, haste4P;

	public int armor5P, magicResist5P, strength5P, agility5P, intelligence5P,
				stamina5P, dexterity5P, focus5P, haste5P;

}

#if UNITY_EDITOR
[CustomEditor (typeof (ItemSet))]
public class ItemSetEditor : Editor
{
	bool show3P, show4P, show5P;

	SerializedProperty setID, setName, setItems;

	SerializedProperty armor3P, magicResist3P, strength3P, agility3P, intelligence3P,
	stamina3P, dexterity3P, focus3P, haste3P;
	
	SerializedProperty armor4P, magicResist4P, strength4P, agility4P, intelligence4P,
	stamina4P, dexterity4P, focus4P, haste4P;
	
	SerializedProperty armor5P, magicResist5P, strength5P, agility5P, intelligence5P,
	stamina5P, dexterity5P, focus5P, haste5P;

	void OnEnable () {

		setName = serializedObject.FindProperty ("setName");
		setID = serializedObject.FindProperty ("setID");
		armor3P = serializedObject.FindProperty ("armor3P"); 
		magicResist3P = serializedObject.FindProperty ("magicResist3P"); 
		strength3P = serializedObject.FindProperty ("strength3P"); 
		agility3P = serializedObject.FindProperty ("agility3P"); 
		intelligence3P = serializedObject.FindProperty ("intelligence3P");
		stamina3P = serializedObject.FindProperty ("stamina3P"); 
		dexterity3P = serializedObject.FindProperty ("dexterity3P"); 
		focus3P = serializedObject.FindProperty ("focus3P"); 
		haste3P = serializedObject.FindProperty ("haste3P");

		armor4P = serializedObject.FindProperty ("armor4P"); 
		magicResist4P = serializedObject.FindProperty ("magicResist4P"); 
		strength4P = serializedObject.FindProperty ("strength4P"); 
		agility4P = serializedObject.FindProperty ("agility4P"); 
		intelligence4P = serializedObject.FindProperty ("intelligence4P");
		stamina4P = serializedObject.FindProperty ("stamina4P"); 
		dexterity4P = serializedObject.FindProperty ("dexterity4P"); 
		focus4P = serializedObject.FindProperty ("focus4P"); 
		haste4P = serializedObject.FindProperty ("haste4P");

		armor5P = serializedObject.FindProperty ("armor5P"); 
		magicResist5P = serializedObject.FindProperty ("magicResist5P"); 
		strength5P = serializedObject.FindProperty ("strength5P"); 
		agility5P = serializedObject.FindProperty ("agility5P"); 
		intelligence5P = serializedObject.FindProperty ("intelligence5P");
		stamina5P = serializedObject.FindProperty ("stamina5P"); 
		dexterity5P = serializedObject.FindProperty ("dexterity5P"); 
		focus5P = serializedObject.FindProperty ("focus5P"); 
		haste5P = serializedObject.FindProperty ("haste5P");

	}
	public override void OnInspectorGUI()
	{
		
		serializedObject.Update ();
		EditorGUI.BeginChangeCheck();

		ItemSet itemSet = target as ItemSet;

		setName.stringValue = EditorGUILayout.TextField("Set Name", itemSet.setName);
		setID.intValue = EditorGUILayout.IntField ("Item ID", itemSet.setID);

		EditorGUIUtility.LookLikeInspector();
		SerializedProperty tps = serializedObject.FindProperty ("setItems");

		EditorGUILayout.PropertyField(tps, true);

		EditorGUIUtility.LookLikeControls();

		
		show3P = EditorGUILayout.Foldout(show3P, "3 Piece Bonuses");
		if (show3P) {
			armor3P.intValue = EditorGUILayout.IntField ("Armor", itemSet.armor3P);
			magicResist3P.intValue = EditorGUILayout.IntField ("Magic Resist", itemSet.magicResist3P);

			strength3P.intValue = EditorGUILayout.IntField ("Strength", itemSet.strength3P);
			agility3P.intValue = EditorGUILayout.IntField ("Agility", itemSet.agility3P);
			intelligence3P.intValue = EditorGUILayout.IntField ("Intelligence", itemSet.intelligence3P);

			stamina3P.intValue = EditorGUILayout.IntField ("Stamina", itemSet.stamina3P);
			dexterity3P.intValue = EditorGUILayout.IntField ("Dexterity", itemSet.dexterity3P);
			focus3P.intValue = EditorGUILayout.IntField ("Focus", itemSet.focus3P);
			haste3P.intValue = EditorGUILayout.IntField ("Haste", itemSet.haste3P);
		}

		show4P = EditorGUILayout.Foldout(show4P, "4 Piece Bonuses");
		if (show4P) {
			armor4P.intValue = EditorGUILayout.IntField ("Armor", itemSet.armor4P);
			magicResist4P.intValue = EditorGUILayout.IntField ("Magic Resist", itemSet.magicResist4P);
			
			strength4P.intValue = EditorGUILayout.IntField ("Strength", itemSet.strength4P);
			agility4P.intValue = EditorGUILayout.IntField ("Agility", itemSet.agility4P);
			intelligence4P.intValue = EditorGUILayout.IntField ("Intelligence", itemSet.intelligence4P);
			
			stamina4P.intValue = EditorGUILayout.IntField ("Stamina", itemSet.stamina4P);
			dexterity4P.intValue = EditorGUILayout.IntField ("Dexterity", itemSet.dexterity4P);
			focus4P.intValue = EditorGUILayout.IntField ("Focus", itemSet.focus4P);
			haste4P.intValue = EditorGUILayout.IntField ("Haste", itemSet.haste4P);
		}

		show5P = EditorGUILayout.Foldout(show5P, "5 Piece Bonuses");
		if (show5P) {
			armor5P.intValue = EditorGUILayout.IntField ("Armor", itemSet.armor5P);
			magicResist5P.intValue = EditorGUILayout.IntField ("Magic Resist", itemSet.magicResist5P);
			
			strength5P.intValue = EditorGUILayout.IntField ("Strength", itemSet.strength5P);
			agility5P.intValue = EditorGUILayout.IntField ("Agility", itemSet.agility5P);
			intelligence5P.intValue = EditorGUILayout.IntField ("Intelligence", itemSet.intelligence5P);
			
			stamina5P.intValue = EditorGUILayout.IntField ("Stamina", itemSet.stamina5P);
			dexterity5P.intValue = EditorGUILayout.IntField ("Dexterity", itemSet.dexterity5P);
			focus5P.intValue = EditorGUILayout.IntField ("Focus", itemSet.focus5P);
			haste5P.intValue = EditorGUILayout.IntField ("Haste", itemSet.haste5P);
		}

		
		AssetDatabase.RenameAsset (AssetDatabase.GetAssetPath (Selection.activeObject), itemSet.setID + "_" + itemSet.setName);

		if(EditorGUI.EndChangeCheck())
			serializedObject.ApplyModifiedProperties();

	}

}
#endif

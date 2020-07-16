using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueNPCManager : NPCManager {

	[SerializeField] private int indexInZone;
	[SerializeField] private Animator animator;
	[SerializeField] private Transform trans;

	private DialogueNPCData sourceData;
	private DialogueNPCInstanceLink instanceLink;

	private CharacterManager character;

	#region Start and Update
	private void Start () {

		// Find InstanceLink from zone list in WorldManager
		instanceLink = WorldManager.instance.FindInstanceLink_DialogueNPC (indexInZone);

		instanceLink.instanceManager = this;
		sourceData = (DialogueNPCData)instanceLink.sourceData;

		// This guy should be dead
		if (instanceLink.active == false) {
			Despawn ();
			return;
		}

		character = CharacterManager.instance;

		if (instanceLink.active)
			Spawn ();
		else
			Despawn ();
	}
	public override NPCData GetSourceData () {
		return sourceData;
	}
	#endregion

	#region Target & Mouse Interactions
	public override void HandleMouseOver () {
		GUIManager.instance.cursorManager.SetQuestCursor ();
	}
	public override void HandleMouseExit () {
		GUIManager.instance.cursorManager.SetDefaultCursor ();
	}
	public bool IsThisTheCurrentTarget () {
		return TargetManager.instance.GetTarget () == this;
	}
	public override void HandleLeftMouseClick () {
		TargetManager.instance.SetTarget (this);
	}
	public override void HandleRightMouseClick () {
		GUIManager.instance.mainMenu.questsMenu.AcceptQuest (sourceData.GetQuests () [2]);
	}
	#endregion

	#region Spawn and Despawn
	public void Spawn () {
		gameObject.SetActive (true);
	}
	public void Despawn () {
		gameObject.SetActive (false);
	}
	#endregion

	

}

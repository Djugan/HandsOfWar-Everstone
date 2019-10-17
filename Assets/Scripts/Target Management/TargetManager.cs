using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour {

	public static TargetManager instance;

	private NPCManager target;

	private void Awake () {
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (gameObject);
		}
		else {
			Destroy (gameObject);
		}
	}

	private void Start () {
		SetTarget (null);
	}

	public void SetTarget (NPCManager _target) {

		target = _target;

		if (target == null) {
			GUIManager.instance.targetFrameManager.Hide ();
		} else {
			GUIManager.instance.targetFrameManager.SetInitalTargetDisplay ();
		}
		
	}

	public NPCManager GetTarget () {
		return target;
	}

	public void DamageTarget (int damage) {
		if (HasTarget () == false)
			return;

		// Trying to attack a non-combat NPC
		if (IsTargetAnEnemy () == false) 
			return;

		target.ReceiveDamage (damage);
	}

	public bool HasTarget () {
		return target != null;
	}

	public bool IsTargetAnEnemy () {
		if (target is EnemyNPCManager)
			return true;

		return false;
	}

	public bool IsTargetDead () {
		return target.IsDead ();
	}


	
}

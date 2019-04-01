using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour {

	//private NPCInstanceLink instanceLink;

	private void OnMouseDown () {
		TargetManager.instance.SetTarget (this);
	}

	public virtual NPCData GetSourceData () {
		return null;
	}

	public virtual void ReceiveDamage (int amount) {}
}

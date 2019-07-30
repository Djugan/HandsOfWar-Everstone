using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour {

	//private NPCInstanceLink instanceLink;

	private void OnMouseOver () {

		HandleMouseOver ();

		// Left Mouse Click
		if (Input.GetMouseButtonDown (0)) {

			HandleLeftMouseClick ();
		}

		// Right Mouse Click
		else if (Input.GetMouseButtonDown (1)) {
			HandleRightMouseClick ();
		}
	}

	private void OnMouseExit () {

		HandleMouseExit ();
	}

	public virtual void HandleMouseOver () {

	}

	public virtual void HandleMouseExit () {

	}
	public virtual void HandleLeftMouseClick () {
		TargetManager.instance.SetTarget (this);
	}
	public virtual void HandleRightMouseClick () {

	}

	public virtual NPCData GetSourceData () {
		return null;
	}

	public virtual void ReceiveDamage (int amount) {}
}

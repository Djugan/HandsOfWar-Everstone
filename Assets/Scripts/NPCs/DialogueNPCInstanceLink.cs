using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueNPCInstanceLink : NPCInstanceLink {

	public new DialogueNPCManager instanceManager;

	public DialogueNPCInstanceLink (bool _active, int _zone, DialogueNPCData _sourceData): 
		base (_active, _sourceData, _zone) {
	}
}

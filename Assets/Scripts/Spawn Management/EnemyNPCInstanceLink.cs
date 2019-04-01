using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNPCInstanceLink : MonoBehaviour {

	public bool active;
	public EnemyNPCData sourceData;
	public EnemyNPCManager instanceManager;

	public EnemyNPCInstanceLink (bool _active, EnemyNPCData _sourceData) {
		active = _active;
		sourceData = _sourceData;
	}
}

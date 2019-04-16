using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNPCInstanceLink : NPCInstanceLink {

	public new EnemyNPCManager instanceManager;
	public int respawnTimer;
	public int timeUntilRespawn;

	public EnemyNPCInstanceLink (bool _active, int _zone, EnemyNPCData _sourceData, int _respawnTimer): 
		base (_active, _sourceData, _zone) {

		respawnTimer = _respawnTimer;
	}
}

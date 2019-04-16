using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNPCInstanceLink {

	public bool active;
	public EnemyNPCData sourceData;
	public EnemyNPCManager instanceManager;
	public int respawnTimer;
	public int timeUntilRespawn;
	public int zone;

	public EnemyNPCInstanceLink (bool _active, int _zone, EnemyNPCData _sourceData, int _respawnTimer) {
		active = _active;
		sourceData = _sourceData;
		zone = _zone;
		respawnTimer = _respawnTimer;
	}
}

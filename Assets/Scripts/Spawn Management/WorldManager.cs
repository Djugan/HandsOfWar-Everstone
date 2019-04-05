using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour {

	public static WorldManager instance;


	public List<Zone> zones;
	public int activeZone;
	public int defaultRespawnTimer;

	#region EnemyNPCs
	[SerializeField] private EnemyNPCData weakRatWarrior;
	[SerializeField] private EnemyNPCData ratWarrior;
	#endregion

	private void Awake () {
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (gameObject);
			Init ();
		}
		else {
			Destroy (instance);
		}
	}

	private void Init () {

		defaultRespawnTimer = 1500;
		activeZone = 1;

		zones = new List<Zone> ();
		zones.Add (CreateZone1 ());
	}

	#region Finding NPCs
	public EnemyNPCInstanceLink FindInstanceLink (int index) {
		return zones [activeZone - 1].enemyNPCList [index];
	}
	public void SpawnEnemy (int index) {
		zones [activeZone - 1].enemyNPCList [index].instanceManager.Spawn ();
	}
	public void DespawnEnemy (int index) {
		zones [activeZone - 1].enemyNPCList [index].instanceManager.Despawn ();
	}
	#endregion

	private Zone CreateZone1 () {

		int zone = 1;
		Zone z = new Zone ();

		List<EnemyNPCInstanceLink> enemyNPCList = new List<EnemyNPCInstanceLink> ();



		// Enemy NPCs
		enemyNPCList.Add (new EnemyNPCInstanceLink (true, zone, weakRatWarrior,		300));						// 0
		enemyNPCList.Add (new EnemyNPCInstanceLink (true, zone, ratWarrior,			400));		// 1

		z.enemyNPCList = enemyNPCList;

		return z;
	}
}

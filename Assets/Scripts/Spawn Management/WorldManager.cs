using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour {

	public static WorldManager instance;


	public List<Zone> zones;
	public int activeZone;
	public int defaultRespawnTimer;

	#region EnemyNPCs
	[Header ("Enemy NPCs")]
	[SerializeField] private EnemyNPCData weakRatWarrior;
	[SerializeField] private EnemyNPCData ratWarrior;
	#endregion

	#region
	[Header ("Dialogue NPCs")]
	[SerializeField] private DialogueNPCData questGiver000;
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
	public EnemyNPCInstanceLink FindInstanceLink_EnemyNPC (int index) {
		return zones [activeZone - 1].enemyNPCList [index];
	}
	public void SpawnEnemy_EnemyNPC (int index) {
		zones [activeZone - 1].enemyNPCList [index].instanceManager.Spawn ();
	}
	public void DespawnEnemy_EnemyNPC (int index) {
		zones [activeZone - 1].enemyNPCList [index].instanceManager.Despawn ();
	}
	#endregion

	#region
	public DialogueNPCInstanceLink FindInstanceLink_DialogueNPC (int index) {
		return zones [activeZone - 1].dialogueNPCList [index];
	}
	public void SpawnEnemy_DialogueNPC (int index) {
		zones [activeZone - 1].dialogueNPCList [index].instanceManager.Spawn ();
	}
	public void DespawnEnemy_DialogueNPC (int index) {
		zones [activeZone - 1].dialogueNPCList [index].instanceManager.Despawn ();
	}
	#endregion

	private Zone CreateZone1 () {

		int zone = 1;
		Zone z = new Zone ();

		List<EnemyNPCInstanceLink> enemyNPCList = new List<EnemyNPCInstanceLink> ();
		List<DialogueNPCInstanceLink> dialogueNPCList = new List<DialogueNPCInstanceLink> ();


		// Enemy NPCs																			// Index
		enemyNPCList.Add (new EnemyNPCInstanceLink (true, zone, weakRatWarrior,		300));		// 0
		enemyNPCList.Add (new EnemyNPCInstanceLink (true, zone, ratWarrior,			400));      // 1

		// Dialogue NPCs																		// Index
		dialogueNPCList.Add (new DialogueNPCInstanceLink (true, zone, questGiver000));			// 0

		z.enemyNPCList = enemyNPCList;
		z.dialogueNPCList = dialogueNPCList;

		return z;
	}
}

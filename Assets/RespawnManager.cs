using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour {

	public static RespawnManager instance;

	private List<EnemyNPCInstanceLink> respawnList;

	private void Awake () {
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (gameObject);
			Init ();
		} else {
			Destroy (gameObject);
		}
	}

	private void Init () {
		respawnList = new List<EnemyNPCInstanceLink> ();
	}

	public void AddEnemyToRespawnList (EnemyNPCInstanceLink e) {
		respawnList.Add (e);
	}

	private void FixedUpdate () {
		for (int i = 0; i < respawnList.Count; i++) {
			
			respawnList [i].timeUntilRespawn--;
			if (respawnList [i].timeUntilRespawn <= 0) {

				respawnList [i].active = true;
				
				// Respawn if player is in the zone where this enemy belongs
				if (respawnList [i].zone == WorldManager.instance.activeZone) {
					respawnList [i].instanceManager.Spawn ();
				}

				respawnList.RemoveAt (i);
			}
		}
	}
}
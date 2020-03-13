using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour {

	[Header ("Cursor Icons")]
	//[SerializeField] private Texture2D defaultCursor;
	[SerializeField] private Texture2D combatCursor;
	[SerializeField] private Texture2D lootCursor;
	[SerializeField] private Texture2D dialogueCursor;
	[SerializeField] private Texture2D questCursor;

	public void SetDefaultCursor () {
		Cursor.SetCursor (null, Vector2.zero, CursorMode.Auto);
	}

	public void SetLootCursor () {
		Cursor.SetCursor (lootCursor, Vector2.zero, CursorMode.Auto);
	}

	public void SetCombatCursor () {
		Cursor.SetCursor (combatCursor, Vector2.zero, CursorMode.Auto);
	}

	public void SetQuestCursor () {
		Cursor.SetCursor (questCursor, Vector2.zero, CursorMode.Auto);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeybindingManager : MonoBehaviour {

	public static KeyCode[] actionBarKeys;
	private static Dictionary<KeyCode, char> keyCodeToChar;

	public static void Init () {

		actionBarKeys = new KeyCode [12];
		SetDefaultKeys ();
		SetKeycodeToChar ();

	}

	private static void SetDefaultKeys () {

		actionBarKeys [0] = KeyCode.Alpha1;
		actionBarKeys [1] = KeyCode.Alpha2;
		actionBarKeys [2] = KeyCode.Alpha3;
		actionBarKeys [3] = KeyCode.Alpha4;
		actionBarKeys [4] = KeyCode.Alpha5;
		actionBarKeys [5] = KeyCode.Alpha6;
		actionBarKeys [6] = KeyCode.Alpha7;
		actionBarKeys [7] = KeyCode.Alpha8;
		actionBarKeys [8] = KeyCode.Alpha9;
		actionBarKeys [9] = KeyCode.Alpha0;
		actionBarKeys [10] = KeyCode.Minus;
		actionBarKeys [11] = KeyCode.Equals;
	}

	public static string GetKeyCodeDisplay (KeyCode k) {
		if (keyCodeToChar.ContainsKey (k)) {
			return keyCodeToChar [k].ToString ();
		}

		return k.ToString ();
	}

	private static void SetKeycodeToChar () {

		keyCodeToChar = new Dictionary<KeyCode, char> ();

		keyCodeToChar.Add (KeyCode.Alpha0, '0');
		keyCodeToChar.Add (KeyCode.Alpha1, '1');
		keyCodeToChar.Add (KeyCode.Alpha2, '2');
		keyCodeToChar.Add (KeyCode.Alpha3, '3');
		keyCodeToChar.Add (KeyCode.Alpha4, '4');
		keyCodeToChar.Add (KeyCode.Alpha5, '5');
		keyCodeToChar.Add (KeyCode.Alpha6, '6');
		keyCodeToChar.Add (KeyCode.Alpha7, '7');
		keyCodeToChar.Add (KeyCode.Alpha8, '8');
		keyCodeToChar.Add (KeyCode.Alpha9, '9');

		keyCodeToChar.Add (KeyCode.Minus, '-');
		keyCodeToChar.Add (KeyCode.Equals, '=');

		/*
		//Other Symbols
		{'!', KeyCode.Exclaim}, //1
		{'"', KeyCode.DoubleQuote},
		{'#', KeyCode.Hash}, //3
		{'$', KeyCode.Dollar}, //4
		{'&', KeyCode.Ampersand}, //7
		{'\'', KeyCode.Quote}, //remember the special forward slash rule... this isnt wrong
		{'(', KeyCode.LeftParen}, //9
		{')', KeyCode.RightParen}, //0
		{'*', KeyCode.Asterisk}, //8
		{'+', KeyCode.Plus},
		{',', KeyCode.Comma},
		{'-', KeyCode.Minus},
		{'.', KeyCode.Period},
		{'/', KeyCode.Slash},
		{':', KeyCode.Colon},
		{';', KeyCode.Semicolon},
		{'<', KeyCode.Less},
		{'=', KeyCode.Equals},
		{'>', KeyCode.Greater},
		{'?', KeyCode.Question},
		{'@', KeyCode.At}, //2
		{'[', KeyCode.LeftBracket},
		{'\\', KeyCode.Backslash}, //remember the special forward slash rule... this isnt wrong
		{']', KeyCode.RightBracket},
		{'^', KeyCode.Caret}, //6
		{'_', KeyCode.Underscore},
		{'`', KeyCode.BackQuote},
		*/
	}
}

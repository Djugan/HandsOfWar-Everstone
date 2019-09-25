using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HashIDs : MonoBehaviour {

	public static int jumping_bool =			Animator.StringToHash ("Jumping");
	public static int inCombat_bool =			Animator.StringToHash ("InCombat");

	public static int velocity_int =			Animator.StringToHash ("Velocity");
	public static int horizontalStrafe_int =	Animator.StringToHash ("HorizontalStrafe");
	public static int weaponType_int =			Animator.StringToHash ("WeaponType");
	
}

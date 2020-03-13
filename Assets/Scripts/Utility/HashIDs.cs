using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HashIDs : MonoBehaviour {

	public static int jumping_bool =			Animator.StringToHash ("Jumping");
	public static int inCombat_bool =			Animator.StringToHash ("InCombat");

	public static int velocity_int =			Animator.StringToHash ("Velocity");
	public static int horizontalStrafe_int =	Animator.StringToHash ("HorizontalStrafe");
	public static int attackNumber_int =		Animator.StringToHash ("AttackNumber");

	public static int deathValue_int =			Animator.StringToHash ("DeathValue");
	public static int isAggro_bool =			Animator.StringToHash ("IsAggro");
	public static int isMoving_bool =			Animator.StringToHash ("IsMoving");
	public static int attackValue_int =			Animator.StringToHash ("AttackValue");
}

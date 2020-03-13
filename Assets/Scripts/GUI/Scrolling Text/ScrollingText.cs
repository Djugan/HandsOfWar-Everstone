using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScrollingText : MonoBehaviour {

	[SerializeField] private TextMeshProUGUI value_Txt;
	[SerializeField] private RectTransform trans;

	private static float scrollAmt = 2f;
	private static float totalMovementAmount = 2f;

	private Vector3 scrollDirection;

	private float startYPosition;

	private bool isActive;

	private void Start () {
		ScrollingTextManager.instance.AddToStack (this);
		isActive = false;
	}

	public void SetPosition (Vector3 position) {
		trans.position = position;
	}

	public void SetText (string text, Color color) {
		value_Txt.text = text;
		value_Txt.color = color;
	}

	public void BeginScroll (Vector3 _scrollDirection) {
		isActive = true;
		startYPosition = trans.position.y;
		scrollDirection = _scrollDirection;
	}

	private void Update () {
		if (isActive == false)
			return;

		MoveText ();
		FaceCamera ();
	}

	private void MoveText () {
		trans.position += (scrollDirection + Vector3.up) * scrollAmt * Time.deltaTime;

		float distanceTraveled = trans.position.y - startYPosition;
		float percentage = distanceTraveled / totalMovementAmount;

		if (percentage > 0.5f) {
			value_Txt.alpha = 1f - ((percentage - 0.5f) * 2f);
		}
		

		if (percentage > 1f) {
			RemoveText ();
		}
	}

	private void RemoveText () {
		isActive = false;
		value_Txt.text = "";
		value_Txt.alpha = 1f;

		ScrollingTextManager.instance.AddToStack (this);
	}

	private void FaceCamera () {
		Vector3 towardsCamera = trans.position - CameraManager.instance.mainCameraTrans.position;
		trans.rotation = Quaternion.LookRotation (towardsCamera);
	}
}

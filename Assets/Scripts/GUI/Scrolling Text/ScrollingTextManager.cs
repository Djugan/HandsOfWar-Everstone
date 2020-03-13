using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingTextManager : MonoBehaviour {

	public static ScrollingTextManager instance;

	[SerializeField] private ScrollingText scrollingTextPrefab;

	private Stack<ScrollingText> stack;
	private Vector3 verticalOffset = new Vector3 (0f, 2f, 0f);
	

	private void Awake () {
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (gameObject);

			stack = new Stack<ScrollingText> ();
			
		}
		else {
			Destroy (gameObject);
		}
	}

	public void ShowScrollingText (int amount, Color color, Vector3 sourcePosition, Vector3 targetPosition) {

		amount = Mathf.Abs (amount);

		// There are no available scrolling text objects to use/show
		if (stack.Count == 0) {
			AddToStack (Instantiate (scrollingTextPrefab));
		}

		ScrollingText t = stack.Pop ();

		Vector3 startPosition = targetPosition + verticalOffset;
		Vector3 scrollDirection = (targetPosition - sourcePosition).normalized;

		if (sourcePosition == Vector3.zero) {
			scrollDirection = Vector3.zero;
		}

		t.SetPosition (startPosition);
		t.SetText (amount.ToString (), color);
		t.BeginScroll (scrollDirection);
	}

	public void AddToStack (ScrollingText t) {
		stack.Push (t);
	}

}

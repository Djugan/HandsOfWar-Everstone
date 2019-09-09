using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreenManager : MonoBehaviour
{

	public static LoadingScreenManager instance;

	[SerializeField] private GameObject main_GO;

	private void Awake () {
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (gameObject);
			HideWindow ();
		}
		else {
			Destroy (gameObject);
		}
	}

	public void HideWindow () {
		main_GO.SetActive (false);
	}
	public void ShowWindow () {
		main_GO.SetActive (true);
	}

	public void LoadScene (string sceneToLoad) {

		StartCoroutine ( LoadSceneASync (sceneToLoad) );
	}

	IEnumerator LoadSceneASync (string scene) {

		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync (scene);

		while (!asyncLoad.isDone) {
			yield return null;
		}

		//Finished!
		HideWindow ();
	}
}

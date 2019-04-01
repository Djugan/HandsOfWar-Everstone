using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class AssetManager : MonoBehaviour {

	private static Dictionary<string, AssetBundle> loadedBundles = new Dictionary<string, AssetBundle> ();


	public static AssetBundle LoadBundle (string bundleName) {
		bundleName = bundleName.ToLower ();

		if (!loadedBundles.ContainsKey (bundleName)) {
			try {
				//Debug.Log("Loading asset bundle: " + bundleName);
				AssetBundle bundle = AssetBundle.LoadFromFile (Path.Combine (Application.streamingAssetsPath, bundleName));
				loadedBundles.Add (bundleName, bundle);
			}
			catch {
				Debug.Log ("ERROR:  Problem loading the bundle: " + bundleName);
			}
		}
		return loadedBundles [bundleName];
	}
}

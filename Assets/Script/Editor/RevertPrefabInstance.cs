using UnityEngine;
using UnityEditor;

public class RevertPrefabInstance : MonoBehaviour {

    [MenuItem ("Tools/Revert to Prefab")]
	static void Revert() {
		GameObject[] selection = Selection.gameObjects;

		if (selection.Length > 0) {
			for (int i = 0; i < selection.Length; i++) {
				PrefabUtility.RevertPrefabInstance (selection [i], InteractionMode.AutomatedAction);
			}
		} else {
			Debug.Log ("Cannot revert to prefab - nothing selected");
		}
	}
}
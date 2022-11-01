using UnityEngine;
using System.Collections;

public class RemoveRenderer : MonoBehaviour {

	void Awake () {
		Destroy (GetComponent<MeshRenderer> ());
		Destroy (GetComponent<MeshFilter> ());
	}
}

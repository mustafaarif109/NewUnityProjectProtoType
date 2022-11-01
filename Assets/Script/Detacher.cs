using UnityEngine;

public class Detacher : MonoBehaviour {

	void Awake () {
		transform.DetachChildren ();
		GameObject.Destroy (gameObject);
	}
}

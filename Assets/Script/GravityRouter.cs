using UnityEngine;

public class GravityRouter : MonoBehaviour {

	public CapsuleCollider gravityCollider;

	void Start(){
		gravityCollider.enabled = false;
	}

	void OnTriggerExit(Collider other) {
		gravityCollider.enabled = false;
	}

	void OnTriggerStay(Collider other) {
		gravityCollider.enabled = true;
		PlayerBody playerBody = other.GetComponent<PlayerBody> ();
		RaycastHit surfaceDown;

		if (playerBody && Physics.Linecast(playerBody.transform.position, gravityCollider.transform.position, out surfaceDown)) {
			Debug.DrawLine (playerBody.transform.position, gravityCollider.transform.position);
			playerBody.playerNormal = surfaceDown.normal;
		}
	}
}
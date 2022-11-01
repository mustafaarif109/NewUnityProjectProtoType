using UnityEngine;

public class PlayerBody : MonoBehaviour {

	public Transform cam;
	public Rigidbody rigid;
	public Vector3 playerNormal;
	public bool onAir = true;
	private float speed = 10f;

	private Vector3 _forceVector = Vector3.zero;
    private float _xSpeed = 0f;
    private float _ySpeed = 0f;
    private float _zSpeed = 0f;

    void Start () {
		playerNormal = transform.up;
	}
	
	void Update () {
		transform.Rotate(Vector3.up, Input.GetAxisRaw("Mouse X")*1.75f);

		Vector3 playerForward = Vector3.Cross(transform.right, playerNormal);
		transform.rotation = Quaternion.LookRotation(playerForward, playerNormal);

		RaycastHit groundCast;
		if (Physics.Linecast (transform.position + transform.up * 0.5f, transform.position - transform.up*0.6f, out groundCast)) {
			if (groundCast.distance <= 0.6f) {
				
				_ySpeed = 0f;

				if (Input.GetButton ("Jump")) {
					transform.position += transform.up * 0.1f;
					_ySpeed = 5f;
				}

			} else {
				
				if (_ySpeed > -25f) {
					_ySpeed -= 0.2f;
				}
			}
		} else {
			if (_ySpeed > -25f) {
				_ySpeed -= 0.2f;
			}
		}

		if (Input.GetAxisRaw ("Horizontal") == 1f) {
			if (_xSpeed < speed) {
				_xSpeed += 1f;
			} else {
				_xSpeed -= 1f;
			}
				
		} else if (Input.GetAxisRaw ("Horizontal") == -1f) {
			if (_xSpeed > -speed) {
				_xSpeed -= 1f;
			} else {
				_xSpeed += 1f;
			}
				
		} else {
			_xSpeed *= 0.8f;
		}

		if (Input.GetAxisRaw ("Vertical") == 1f) {
			if (_zSpeed < speed) {
				_zSpeed += 1f;
			} else {
				_zSpeed -= 1f;
			}
				
		} else if (Input.GetAxisRaw ("Vertical") == -1f) {
			if (_zSpeed > -speed) {
				_zSpeed -= 1f;
			} else {
				_zSpeed += 1f;
			}
				
		} else {
			_zSpeed *= 0.8f;
		}

		if (Mathf.Abs(_xSpeed) + Mathf.Abs(_zSpeed) > speed*1.4f) {
			float divider = speed*1.4f / (Mathf.Abs(_xSpeed) + Mathf.Abs(_zSpeed));
			_xSpeed *= divider;
			_zSpeed *= divider;
		}

		_forceVector.Set (_xSpeed, _ySpeed, _zSpeed);
		rigid.velocity = Quaternion.Euler (transform.eulerAngles) * _forceVector;

	}
}

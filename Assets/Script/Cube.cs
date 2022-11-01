using UnityEngine;

public class Cube : MonoBehaviour {

	private Cube _root;

	public bool isFixed;
	public bool isMagnetic;

	public static int[] cubes = {0,0,0,0};

	public int _cubeNo;
	public string _color;

	public int cubeNo {	get { return _cubeNo; }	}
	public string color { get { return _color; } }

	public Cube root {
		get { 
			if (!_root) _root = GetComponent<Cube> ();
			return _root;
		}
	}

	public bool rayCast(out RaycastHit result, Vector3 v3) {
		return Physics.Linecast (transform.position, transform.position + v3, out result);
	}

	public void moveTo(Vector3 v3, float dur) {
		Worker.moveObject (gameObject, v3, dur);
	}



	public bool moveTest(Vector3 v3) {
		if (tested) {
			return true;
		}
		tested = true;

		moveBy = v3;
		RaycastHit result;
		Cube cube;

		if (rayCast (out result, v3)) {
			cube = result.transform.GetComponent<Cube> ();
			if(!cube) {
				return false;	
			} else if (!cube.isFixed) {
				if (cube.moveTest (v3)) {
					nextCube = cube;
				} else {
					return false;
				}
			}
		}

		return true;
	}
	public void move(float dur) {
		tested = false;
		Worker.moveObject (gameObject, transform.position + moveBy, dur);
		if (nextCube) {
			nextCube.move (dur);
			nextCube = null;
		}
		if (topCube) {
			topCube.move (dur);
			topCube = null;
		}
	}
	public bool tested = false;
	public Vector3 moveBy;
	public Cube nextCube;
	public Cube topCube;


	public void remove(float dur) {
		Worker.removeObj (gameObject, dur);
	}

	public static void removeAction() {
		if (--_actions == 0) {
			_focusable = true;
		} else if (_actions < 0) {
			Debug.Log ("Action count(" + _actions.ToString () + ") problem.");
		}
	}
	public static void addAction() {
		_actions++;
		_focusable = false;
	}
	public static int _actions = 0;
	public static bool _focusable = true;
}

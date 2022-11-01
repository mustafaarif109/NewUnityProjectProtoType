using UnityEngine;
using System.Collections;
using System.Resources;

public class Tools : MonoBehaviour {

	public static Vector3 ease_quinticInOut (float t, Vector3 b, Vector3 c, float d) {
		t /= d / 2f;
		if (t < 1)
			return c / 2 * t * t * t * t * t + b;
		t -= 2f;
		return c / 2 * (t * t * t * t * t + 2) + b;
	}

	public static void setVisibility (GameObject obj, bool val) {
		Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
		for (int i = 0; i < renderers.Length; i++) {
			renderers[i].enabled = val;
		}
	}

	public static Vector3 snapV3toF (Vector3 v3, float toF) {
		return new Vector3 (snapFtoF (v3.x, toF), snapFtoF (v3.y, toF), snapFtoF (v3.z, toF));
	}

	public static float snapFtoF(float valF, float toF) {
		return Mathf.Round (valF / toF) * toF;
	}

	public static Vector3 vectorLimit(Vector3 v3, float max) {
		Vector3 v3val = v3;
		while (v3val.x > max || v3val.z > max) {
			v3val *= 0.5f;
		}
		return v3val;
	}

	public static Vector3 v3fixNormal(Vector3 v3) {
		Vector3 v3n = v3.normalized;
		return new Vector3 ((int)v3n.x, (int)v3n.y, (int)v3n.z);
	}

	public static Vector3 v3absolute(Vector3 v3) {
		return new Vector3 (Mathf.Abs(v3.x), Mathf.Abs(v3.y), Mathf.Abs(v3.z));
	}

	public static Vector3 v3contrast(Vector3 v3) {
		Vector3 v3c = Vector3.one - v3;
		return v3c;
	}

	public static Vector3 v3multiply(Vector3 v3, Vector3 vm) {
		return new Vector3 (v3.x * vm.x, v3.y * vm.y, v3.z * vm.z);
	}
}

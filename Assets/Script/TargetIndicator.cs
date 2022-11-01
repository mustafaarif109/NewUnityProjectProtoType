using UnityEngine;
using System.Collections;

public class TargetIndicator : MonoBehaviour {

	public Material _indicatorPurple;
	public Material _indicatorBlack;
	public Material _indicatorRed;
	public Material _indicatorGreen;
	private MeshRenderer _renderer;

	public void setColor(string t) {
		if (_renderer == null) {
			_renderer = GetComponent<MeshRenderer> ();
		}

		if (t == "purple") {
			_renderer.material = _indicatorPurple;
		} else if (t == "black") {
			_renderer.material = _indicatorBlack;
		} else if (t == "red") {
			_renderer.material = _indicatorRed;
		} else if (t == "green") {
			_renderer.material = _indicatorGreen;
		}
	}
}

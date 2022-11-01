using UnityEngine;

public class Worker : MonoBehaviour {

	public static GameObject Template;
	private GameObject _copy;
	private GameObject _obj;
	private Vector3 _source;
	private Vector3 _target;
	private float _dur;
	private float _elp;
	private bool _started;
	private string _mode;
	private string _onFinish;

	public delegate void Function();

	void Start () {
		if (!Template) {
			Template = gameObject;
		}
		_copy = gameObject;
	}

	void Update () {
		if (_started) {
			if (_elp < _dur && (_obj.transform != null)) {
				_elp++;
				if (_mode == "move")
					_obj.transform.position = Tools.ease_quinticInOut (_elp, _source, _target - _source, _dur);
				else if (_mode == "scale")
					_obj.transform.localScale = Tools.ease_quinticInOut (_elp, _source, _target - _source, _dur);
			} else {
				
				if (_onFinish == "remove") {
					Destroy (_obj);
				} else if (_mode == "move") {
					_obj.transform.position = _target;
				} else if (_mode == "scale") {
					_obj.transform.localScale = _target;
				}

				Cube.removeAction ();
				Destroy (_copy);
			}
		}
	}

	public void move(GameObject obj, Vector3 v3, float dur, string onFinish) {
		Cube.addAction ();
		_obj = obj;
		_mode = "move";
		_onFinish = onFinish;
		_source = _obj.transform.position;
		_target = v3;
		_dur = dur*60f;
		_elp = 0f;
		_started = true;
	}

	public void scale(GameObject obj, Vector3 v3, float dur, string onFinish) {
		Cube.addAction ();
		_obj = obj;
		_mode = "scale";
		_onFinish = onFinish;
		_source = _obj.transform.localScale;
		_target = v3;
		_dur = dur*60f;
		_elp = 0f;
		_started = true;
	}

	public static void moveObject(GameObject obj, Vector3 v3, float dur, string onFinish = default(string)) {
		GameObject copyWorker = GameObject.Instantiate (Worker.Template);
		Worker worker = copyWorker.GetComponent<Worker> ();
		worker.move (obj, v3, dur, onFinish);
	}

	public static void cloneObject(GameObject obj, Vector3 v3, float dur, string onFinish = default(string)) {
		GameObject copyObj = GameObject.Instantiate (obj);
		copyObj.transform.localScale *= 0.5f;
		moveObject (copyObj, v3, dur*0.75f);
		scaleObject (copyObj, Vector3.one, dur, onFinish);
	}

	public static void scaleObject(GameObject obj, Vector3 v3, float dur, string onFinish = default(string)) {
		GameObject copyWorker = GameObject.Instantiate (Worker.Template);
		Worker worker = copyWorker.GetComponent<Worker> ();
		worker.scale (obj, v3, dur, onFinish);
	}

	public static void createObject(GameObject obj, Vector3 v3, float dur, string onFinish = default(string)) {
		GameObject copyObj = Instantiate (obj);
		copyObj.transform.localScale = Vector3.zero;
		copyObj.transform.position = v3;

		GameObject copyWorker = GameObject.Instantiate (Worker.Template);
		Worker worker = copyWorker.GetComponent<Worker> ();
		worker.scale (copyObj, Vector3.one, dur, onFinish);
	}

	public static void removeObj(GameObject obj, float dur) {
		scaleObject (obj, Vector3.zero, dur, "remove");
	}

}
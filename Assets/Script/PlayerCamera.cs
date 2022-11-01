using UnityEngine;

public class PlayerCamera : MonoBehaviour {

	public Transform playerStart;
	public Transform playerBody;
    public float minRotation;
    public float maxRotation;
    public float sensitivity;
    public Transform indicator;
    public TargetIndicator targetIndicator;
    public RaycastHit _lookPoint;
    public Cube[] templateCubes;
    public UIControl _uiControl;

    private Cube _cube;
    private Transform _cubeTransform;
    private Quaternion _indicatorRotation = new Quaternion();
    private int _selectedTool = 0;
    private float _toolRange;
    private float _rotateX = 0f;
    private float _targetRotX = 0f;
    private int _cubeNo = 0;
    private Cube _selectedCube;

    void Awake() {
		Application.targetFrameRate = 60;
	}

	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update() {
		CameraControls ();
		ToolControls ();
		MouseControls ();
		MenuControls ();
	}

	private void MenuControls() {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit();
		}
	}

	private void CameraControls() {
		_targetRotX = Mathf.Clamp(_targetRotX + Input.GetAxis ("Mouse Y")*sensitivity, minRotation, maxRotation);
		_rotateX = Mathf.LerpAngle(_rotateX, _targetRotX, Time.deltaTime*25f); 
		transform.position = playerBody.position + playerBody.transform.up;
		transform.eulerAngles = new Vector3(playerBody.eulerAngles.x, playerBody.eulerAngles.y, playerBody.eulerAngles.z);
		transform.Rotate (Vector3.right, -_rotateX);
	}

	private void MouseControls() {
		if (Cube._focusable) {
			if (Physics.Raycast (transform.position, transform.forward, out _lookPoint)) {
				if (Vector3.Distance (_lookPoint.point, transform.position) < _toolRange) {
					if (_cubeTransform != _lookPoint.transform) {
						_cubeTransform = _lookPoint.transform;
						_cube = _cubeTransform.GetComponent<Cube> ();
					}

					targetIndicator.transform.localScale = Vector3.one;
					_indicatorRotation.SetLookRotation (_lookPoint.normal);
					targetIndicator.transform.position = Tools.snapV3toF(_lookPoint.point + _lookPoint.normal * 0.5f, 1f);
					indicator.position = targetIndicator.transform.position;
					indicator.rotation = _indicatorRotation;
					Tools.setVisibility (targetIndicator.gameObject, false);
					Tools.setVisibility (indicator.gameObject, false);

					if (_selectedTool == 1) {
						Tools.setVisibility (targetIndicator.gameObject, true);
						if (Input.GetMouseButtonDown (0)) {
							if (Cube.cubes [_selectedCube.cubeNo] > 0) {
								_uiControl.removeCube (_selectedCube.cubeNo, 1);
								Worker.createObject (_selectedCube.gameObject, targetIndicator.transform.position, 0.2f);

							} else {
								Debug.Log ("No " + _selectedCube.name + "s left.");
							}
						}
					} else if (_selectedTool == 2) {
						if (_cube) {
							Tools.setVisibility (targetIndicator.gameObject, true);
							targetIndicator.transform.position = _cubeTransform.position;
							targetIndicator.transform.localScale = Vector3.one * 1.1f;

							if (Input.GetMouseButtonDown (0)) {
								_uiControl.addCube (_cube.cubeNo, 1);
								_cubeTransform = null;
								Destroy (_cube.GetComponent<BoxCollider> ());
								Worker.removeObj (_cube.gameObject, 0.2f);
								_cube = null;
							}
						}
					} else if (_selectedTool == 3) {
						if (_cube && !_cube.isFixed) {
							Tools.setVisibility (indicator.gameObject, true);
							if (Input.GetMouseButtonDown (0)) {
								if (_cube.moveTest (-_lookPoint.normal)) {
									_cube.move (0.2f);
								}
							} else if (Input.GetMouseButtonDown (1)) {
								if (_cube.moveTest (_lookPoint.normal)) {
									_cube.move (0.2f);
								}
							}
						}
					}
				} else {
					RemoveIndicators ();
				}
			} else {
				RemoveIndicators ();
			}
		}  else {
			RemoveIndicators ();
		}
	}

	private void RemoveIndicators() {
		Tools.setVisibility (indicator.gameObject, false);
		Tools.setVisibility (targetIndicator.gameObject, false);
	}

	

	private void ToolControls() {
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			if (_selectedTool == 1) {
				_cubeNo = (_cubeNo == 1) ? 0 : 1;
			} else {
				_selectedTool = 1;
			}

			_selectedCube = templateCubes[_cubeNo];
			_toolRange = 5f;
			targetIndicator.GetComponent<TargetIndicator> ().setColor (_selectedCube.color);

		} else if (Input.GetKeyDown (KeyCode.Alpha2)) {
			_selectedTool = 2;
			_toolRange = 5f;
			targetIndicator.GetComponent<TargetIndicator> ().setColor ("green");
		} else if (Input.GetKeyDown (KeyCode.Alpha3)) {
			_selectedTool = 3;
			_toolRange = 5f;
		} else if (Input.GetKeyDown (KeyCode.Alpha4)) {
			_selectedTool = 4;
			_toolRange = 5f;
		}
	}
	
}
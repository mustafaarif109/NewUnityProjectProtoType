using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIControl : MonoBehaviour {

	public Text[] counters;

	public void addCube (int cubeNo, int count) {
		counters [cubeNo].text = (++Cube.cubes[cubeNo]).ToString();
	}

	public void removeCube (int cubeNo, int count) {
		counters [cubeNo].text = (--Cube.cubes[cubeNo]).ToString();
	}
}

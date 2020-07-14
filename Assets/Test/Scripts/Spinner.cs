// by @unmanuel

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	Spinner: Use this to spin a game object at a fixed speed.
*/
public class Spinner : MonoBehaviour {

	public float speed;

	void Update() {
		if(speed != 0)
			transform.Rotate(Vector3.up, speed * Time.deltaTime);
	}
}

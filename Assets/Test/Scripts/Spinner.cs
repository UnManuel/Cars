// by @unmanuel

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	Spinner: Use this to spin a game object at a fixed speed.
*/
public class Spinner : MonoBehaviour {

	public float Speed;		// Set spin speed here

	/*
		Update: Rotates the object around Y axis.
	*/
	void Update() {
		if(Speed != 0)
			transform.Rotate(Vector3.up, Speed * Time.deltaTime);
	}
}

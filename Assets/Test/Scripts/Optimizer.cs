// by @unmanuel

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	Optimizer: Base class to create recording optimizers.
*/
public class Optimizer : MonoBehaviour {

	public virtual Vector3 Optimize(Vector3 point) {
		return point;
	}
}

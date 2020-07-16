// by @unmanuel

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	Optimizer: Base class to create recording optimizers.
*/
public class Optimizer : MonoBehaviour {

	/*
		Optimize: Override this to optimize based upon custom criteria.

		Params:

		point(Vector3): The point of the path to be optimized.
	*/
	public virtual Vector3 Optimize(Vector3 point) {
		return point;
	}
}

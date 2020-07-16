// by @unmanuel

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	Squeezer: Basic optimizer. It "squeezes" point data between the given range.
*/
public class Squeezer : Optimizer {

	public Transform Pivot;					// Center point of the squeeze
	public float MinSqueeze, MaxSqueeze;	// Squeeze range - Works well with round tracks!

	/*
		Optimize: It will optimize point by squeezing it between MinSqueeze and MaxSqueeze.

		Params:

		point(Vector3): The point of the path to be squeezed.
	*/
	public override Vector3 Optimize(Vector3 point) {
		return ((point - Pivot.position) * MaxSqueeze - (point - Pivot.position) * (MaxSqueeze - MinSqueeze)) + Pivot.position;
	}
}

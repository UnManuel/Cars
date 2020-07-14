// by @unmanuel

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	Squeezer: Basic optimizer. It "squeezes" point data between the given range.
*/
public class Squeezer : Optimizer {

	public Transform pivot;
	public float minSqueeze, maxSqueeze;

	public override Vector3 Optimize(Vector3 point) {
		return ((point - pivot.position) * maxSqueeze - (point - pivot.position) * (maxSqueeze - minSqueeze)) + pivot.position;
	}
}

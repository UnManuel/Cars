// by @unmanuel

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
	Character: Model class to handle both AI and player racer characters.
*/
public class Character : MonoBehaviour {

	public int LapCount = -1;	// Lap counter for this particular racer

	public GameObject Target;	// Visual representation of the racer

	public UnityEvent OnLapComplete;	// Event to be called at each lap

	[HideInInspector]
	public Vector3 Velocity;	// Velocity data to verify laps

	// TO-DO: Use velocity to prevent lap miscount
	public void LapCheck(FinishLine finish) {

		++LapCount;

		if(OnLapComplete != null)
			OnLapComplete.Invoke();
	}
}

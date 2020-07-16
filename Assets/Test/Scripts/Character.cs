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

	/*
		LapCheck: Checks if a lap has been succesfully completed.

		Params:

		finish(FinishLine): A reference to the finish line, useful to compare against a racer.
	*/	
	public void LapCheck(FinishLine finish) {
		
		// TO-DO: Use velocity to prevent lap miscount

		++LapCount;

		if(OnLapComplete != null)
			OnLapComplete.Invoke();
	}
}

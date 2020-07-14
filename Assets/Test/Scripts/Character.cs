// by @unmanuel

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
	Character: Model class to handle both AI and player racer characters.
*/
public class Character : MonoBehaviour {

	[HideInInspector]
	public Vector3 velocity;

	public int lapCount = 0;

	public UnityEvent OnLapComplete;

	// TO-DO: Use velocity to prevent lap miscount
	public void LapCheck(FinishLine finish) {

		++lapCount;

		if(OnLapComplete != null)
			OnLapComplete.Invoke();
	}
}

// by @unmanuel

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour {

	[HideInInspector]
	public Vector3 velocity;

	public int lapCount = 0;

	public UnityEvent OnLapComplete;

	public void LapCheck(FinishLine finish) {

		++lapCount;

		if(OnLapComplete != null)
			OnLapComplete.Invoke();
	}
}

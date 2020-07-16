// by @unmanuel

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	FinishLine: This object detects when racer characters complete a lap.
*/
public class FinishLine : MonoBehaviour {

	/*
		OnTriggerEnter: We will identify a collision with a racer character.

		Params:

		collider(Collider): If this collider has a Character attached then we check it for laps.
	*/
	void OnTriggerEnter(Collider collider) {

		Character Character = collider.gameObject.GetComponent<Character>();

		if(Character != null)
			Character.LapCheck(this);
	}
}

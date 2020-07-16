// by @unmanuel

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	FinishLine: This object detects when racer characters complete a lap.
*/
public class FinishLine : MonoBehaviour {
	
	void OnTriggerEnter(Collider collider) {

		Character Character = collider.gameObject.GetComponent<Character>();

		if(Character != null)
			Character.LapCheck(this);
	}
}

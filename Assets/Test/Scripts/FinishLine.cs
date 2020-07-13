// by @unmanuel

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour {

	void OnTriggerEnter(Collider collider) {

		Character character = collider.gameObject.GetComponent<Character>();

		if(character != null)
			character.LapCheck(this);
	}
}

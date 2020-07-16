// by @unmanuel

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	Player: Basic player controller.
*/
public class Player : MonoBehaviour {

	public Character Character;		// Racer model

	public Rigidbody Body;			// Physical object to move

	void Update() {
		Character.Velocity = Body.velocity;
	}
}

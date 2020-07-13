// by @unmanuel

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public Character character;

	public Rigidbody body;

	void Update() {
		character.velocity = body.velocity;
	}
}

// by @unmanuel

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	CarSpecification: General-purpose register for car specifications.
*/
[System.Serializable]
public class CarSpecification {

	public int Id;
	public string Name;
	public float MaxSpeed;
	public float Acceleration;
	public float BrakingTime;
	public float Handling;
}
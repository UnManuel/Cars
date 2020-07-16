// by @unmanuel

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	CarStats: General-purpose register for car specifications.
*/
[System.Serializable]
public class CarStats {

	public int Id;
	public string Name;
	public float MaxSpeed;
	public float Acceleration;
	public float BrakingTime;
	public float Handling;
}
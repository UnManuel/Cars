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

	public int id;
	public string name;
	public float maxSpeed;
	public float acceleration;
	public float brakingTime;
	public float handling;
}
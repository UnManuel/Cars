// by @unmanuel

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	TapeNode: Snapshot of a racer's data at a given time.
	optimalPoint is a displaced version of the original position
	that is closer to the center of the track.
*/
public class TapeNode {

	const float MIN_SPEED = 0.1f;

	public float Time;
	public Vector3 Velocity;
	public Vector3 Point, OptimalPoint;
	public Transform Transform;

	public TapeNode(float time, Vector3 velocity, Vector3 point, Vector3 optimalPoint, Transform transform) {
		Time = time;
		Velocity = velocity.magnitude < MIN_SPEED ? Vector3.zero : velocity;	// Enforces full-stop
		Point = point;
		OptimalPoint = optimalPoint;
		Transform = transform;
	}
}

/*
	RaceRecorder: A class designed to record movement of a Rigidbody.
	Samples are stored on a linked list that is later smoothed into a spline.
	Use an Optimizer to replace movement enhancement criteria.
	Sampling rate is customizable.
*/
public class RaceRecorder : MonoBehaviour {

	public float StartDelay = 3;		// Time before recording
	public float SamplingDelay = 1;		// Snapshot time

	public Rigidbody Car;			// Physical object to record
    
	public Spline Path;				// Visual aid of the recorded curve

	public Transform BasePoint;		// Dummy point to create duplicates

	public Optimizer Optimizer;		// Optimizer delegate

	bool Optimize = false;			// Optimized mode flag

	// Control timers
	float Timer = 0, StartTime = 0, RecordTime = 0;

	// Snapshots are stored here
	LinkedList<TapeNode> Tape = new LinkedList<TapeNode>();

	/*
		Awake: The tape is started with the initial position of the car.
	*/
	void Awake() {
		BasePoint.SetPositionAndRotation(Car.transform.position, Car.transform.rotation);
		Tape.AddLast(new TapeNode(0, Vector3.zero, BasePoint.transform.position, Optimizer.Optimize(BasePoint.transform.position), BasePoint));
	}

	/*
		FixedUpdate: We store the snapshots at a fixed rate after the initial cooldown.
	*/
	void FixedUpdate() {

		if(StartTime < StartDelay)
			StartTime += Time.fixedDeltaTime;

		else {
        
	        Timer += Time.fixedDeltaTime;

	        // Snapshots are evenly sampled, then interpolated
	        if(Timer > SamplingDelay) {

	        	RecordTime += Timer;
	        	Timer -= SamplingDelay;

	        	Transform transform = Instantiate(BasePoint, BasePoint.parent);

	        	Vector3 position = Optimizer.Optimize(Car.transform.position);

		        transform.SetPositionAndRotation(Optimize ? position : Car.transform.position, Car.transform.rotation);

		        Tape.AddLast(new TapeNode(RecordTime, Car.velocity, Car.transform.position, position, transform));

		        // Catmull-Rom splines are made out of 4 or more vertices
	        	if(Tape.Count > 3) {

	        		int i = 0;

	        		Transform[] points = new Transform[Tape.Count];

	        		foreach(TapeNode node in Tape)
	        			points[i++] = node.Transform;

	        		// We must replace the entire array with the current spline implementation
	        		Path.controlPoints = points;
	        	}
	        }
	    }
    }

	/*
		ToggleOptimalPath: The recorder will provide an optimized curve when enabled.

		Params:

		optimize(bool = true): Optimized mode flag.
	*/
    public void ToggleOptimalPath(bool optimize = true) {

    	int i = 0;

    	Optimize = optimize;

    	// TO-DO: Transition is not smoothed-out!
    	foreach(TapeNode node in Tape)
    		Path.controlPoints[i++].position = Optimize ? node.OptimalPoint : node.Point;

    	Path.color = Optimize ? Color.green : Color.cyan;
    }

	/*
		GetRecordHead: Returns the first sample of the recording.
	*/
    public LinkedListNode<TapeNode> GetRecordHead() {
    	return Tape.First;
    }

	/*
		GetRecordTime: Returns the total time of the samples recorded.
	*/
    public float GetRecordTime() {
    	return RecordTime;
    }
}

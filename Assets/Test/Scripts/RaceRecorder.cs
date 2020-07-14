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

	public float time;
	public Vector3 velocity, point, optimalPoint;
	public Transform transform;

	public TapeNode(float _time, Vector3 _velocity, Vector3 _point, Vector3 _optimalPoint, Transform _transform) {
		time = _time;
		velocity = _velocity.magnitude < MIN_SPEED ? Vector3.zero : _velocity;	// Enforces full-stop
		point = _point;
		optimalPoint = _optimalPoint;
		transform = _transform;
	}
}

/*
	RaceRecorder: A class designed to record movement of a Rigidbody.
	Samples are stored on a linked list that is later smoothed into a spline.
	Use an Optimizer to replace movement enhancement criteria.
	Sampling rate is customizable.
*/
public class RaceRecorder : MonoBehaviour {

	public float startDelay = 3, samplingDelay = 1;

	public Rigidbody car;
    
	public Spline path;

	// Dummy point to create duplicates
	public Transform basePoint;

	// Optimizer delegate
	public Optimizer optimizer;

	bool optimize = false;
	float time = 0, startTime = 0, recordTime = 0;

	LinkedList<TapeNode> tape = new LinkedList<TapeNode>();

	void Awake() {
		basePoint.SetPositionAndRotation(car.transform.position, car.transform.rotation);
		tape.AddLast(new TapeNode(0, Vector3.zero, basePoint.transform.position, optimizer.Optimize(basePoint.transform.position), basePoint));
	}

	void Update() {

		if(startTime < startDelay)
			startTime += Time.deltaTime;

		else {
        
	        time += Time.deltaTime;

	        // Snapshots are evenly sampled, then interpolated
	        if(time > samplingDelay) {

	        	recordTime += time;
	        	time -= samplingDelay;

	        	Transform transform = Instantiate(basePoint, basePoint.parent);

	        	Vector3 position = optimizer.Optimize(car.transform.position);

		        transform.SetPositionAndRotation(optimize ? position : car.transform.position, car.transform.rotation);

		        tape.AddLast(new TapeNode(recordTime, car.velocity, car.transform.position, position, transform));

		        // Catmull-Rom splines are made out of 4 or more vertices
	        	if(tape.Count > 3) {

	        		int i = 0;

	        		Transform[] points = new Transform[tape.Count];

	        		foreach(TapeNode node in tape)
	        			points[i++] = node.transform;

	        		// We must replace the entire array with the current spline implementation
	        		path.controlPoints = points;
	        	}
	        }
	    }
    }

    public void ToggleOptimalPath(bool _optimize = true) {

    	int i = 0;

    	optimize = _optimize;

    	// TO-DO: Transition is not smoothed-out!
    	foreach(TapeNode node in tape)
    		path.controlPoints[i++].position = optimize ? node.optimalPoint : node.point;

    	path.color = optimize ? Color.green : Color.cyan;
    }

    public LinkedListNode<TapeNode> GetRecordHead() {
    	return tape.First;
    }

    public float GetRecordTime() {
    	return recordTime;
    }
}

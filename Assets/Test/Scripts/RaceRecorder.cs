// by @unmanuel

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapeNode {

	const float MIN_SPEED = 0.1f;

	public float time;
	public Vector3 velocity, point, optimalPoint;
	public Transform transform;

	public TapeNode(float _time, Vector3 _velocity, Vector3 _point, Vector3 _optimalPoint, Transform _transform) {
		time = _time;
		velocity = _velocity.magnitude < MIN_SPEED ? Vector3.zero : _velocity;
		point = _point;
		optimalPoint = _optimalPoint;
		transform = _transform;
	}
}

public class RaceRecorder : MonoBehaviour {

	public float startDelay = 3, samplingDelay = 1;

	public Rigidbody car;
    
	public Spline path;

	public Transform basePoint;

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

	        if(time > samplingDelay) {

	        	recordTime += time;
	        	time -= samplingDelay;

	        	Transform transform = Instantiate(basePoint, basePoint.parent);

	        	Vector3 position = optimizer.Optimize(car.transform.position);

		        transform.SetPositionAndRotation(optimize ? position : car.transform.position, car.transform.rotation);

		        tape.AddLast(new TapeNode(recordTime, car.velocity, car.transform.position, position, transform));

	        	if(tape.Count > 3) {

	        		int i = 0;

	        		Transform[] points = new Transform[tape.Count];

	        		foreach(TapeNode node in tape)
	        			points[i++] = node.transform;

	        		path.controlPoints = points;
	        	}
	        }
	    }
    }

    public void OptimizePath() {

		int i = 0;

		optimize = true;

    	foreach(TapeNode node in tape)
			path.controlPoints[i++].position = node.optimalPoint;

    	path.color = Color.green;
    }

    public LinkedListNode<TapeNode> GetRecordHead() {
    	return tape.First;
    }

    public float GetRecordTime() {
    	return recordTime;
    }
}

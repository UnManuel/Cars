// by @unmanuel

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	AI: Basic AI for the ghost racer. It uses a RaceRecorder object to move without physics.
	Enhanced mode uses an optimized version of the RaceRecorder's actual curve.
*/
public class AI : MonoBehaviour {

	public Character character;

	public GameObject target;

	public RaceRecorder recorder;

	float time = 0;

	LinkedListNode<TapeNode> recordHead;

	void Start() {
		recordHead = recorder.GetRecordHead();
	}

	void FixedUpdate() {
		
		// We need two samples to interpolate
		if(recordHead.Next != null)

			if(recordHead.Value.velocity.magnitude > 0) {

				// We move the ghost using velocity instead of time
				time += (Time.fixedDeltaTime / recorder.samplingDelay * Vector3.Distance(recordHead.Value.point, recordHead.Next.Value.point)) / ((recordHead.Value.velocity.magnitude + recordHead.Next.Value.velocity.magnitude) / 2);
				
				// Moving through the linked list using time to detect ranges
				if(time > recordHead.Next.Value.time)
					recordHead = recordHead.Next;

				if(recordHead.Next != null) {

					// Normalized position
					float f = (time - recordHead.Value.time) / (recordHead.Next.Value.time - recordHead.Value.time);

					// Interpolated position
					Vector3 position = iTween.PointOnPath(recorder.path.controlPoints, time / recorder.GetRecordTime());
					// Interpolated rotation
					Quaternion rotation = Quaternion.Slerp(recordHead.Value.transform.rotation, recordHead.Next.Value.transform.rotation, f);

					target.transform.SetPositionAndRotation(position, rotation);

					// Interpolated velocity
					character.velocity = Vector3.Slerp(recordHead.Value.velocity, recordHead.Next.Value.velocity, f);
				}

			} else
				recordHead = recordHead.Next;
	}

	public void ToggleEnhancedMode(bool enhanced = true) {
		recorder.ToggleOptimalPath(enhanced);
	}
}

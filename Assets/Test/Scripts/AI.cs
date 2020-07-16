// by @unmanuel

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	AI: Basic AI for the ghost racer. It uses a RaceRecorder object to move without physics.
	Enhanced mode uses an optimized version of the RaceRecorder's actual curve.
*/
public class AI : MonoBehaviour {

	public Character Character;		// Racer model
	
	public RaceRecorder Recorder;	// Player recorder

	// Main timer
	float Timer = 0;

	// Beginning of the player record
	LinkedListNode<TapeNode> RecordHead;

	void Start() {
		RecordHead = Recorder.GetRecordHead();
	}

	void FixedUpdate() {
		
		// We need two samples to interpolate
		if(RecordHead.Next != null)

			if(RecordHead.Value.Velocity.magnitude > 0) {

				// We move the ghost using velocity instead of time
				Timer += (Time.fixedDeltaTime / Recorder.SamplingDelay * Vector3.Distance(RecordHead.Value.Point, RecordHead.Next.Value.Point)) / ((RecordHead.Value.Velocity.magnitude + RecordHead.Next.Value.Velocity.magnitude) / 2);
				
				// Normalized position
				float f = (Timer - RecordHead.Value.Time) / (RecordHead.Next.Value.Time - RecordHead.Value.Time);

				// Interpolated position
				Vector3 position = iTween.PointOnPath(Recorder.Path.controlPoints, Timer / Recorder.GetRecordTime());
				// Interpolated rotation
				Quaternion rotation = Quaternion.Slerp(RecordHead.Value.Transform.rotation, RecordHead.Next.Value.Transform.rotation, f);

				transform.SetPositionAndRotation(position, rotation);

				// Interpolated velocity
				Character.Velocity = Vector3.Slerp(RecordHead.Value.Velocity, RecordHead.Next.Value.Velocity, f);

				// Moving through the linked list using time to detect ranges
				if(Timer > RecordHead.Next.Value.Time)
					RecordHead = RecordHead.Next;

			} else
				RecordHead = RecordHead.Next;
	}

	/*
		ToggleEnhancedMode: AI will use an optimized version of the player record when enabled.

		Params:

		enhanced(bool = 0): Enhanced mode flag.
	*/
	public void ToggleEnhancedMode(bool enhanced = true) {
		Recorder.ToggleOptimalPath(enhanced);
	}
}

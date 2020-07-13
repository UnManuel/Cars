// by @unmanuel

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {

	public Character character;

	public GameObject target;

	public RaceRecorder recorder;

	float time = 0;

	LinkedListNode<TapeNode> recordHead;

	void Start() {
		recordHead = recorder.GetRecordHead();
	}

	void Update() {
		
		if(recordHead.Next != null)

			if(recordHead.Value.velocity.magnitude > 0) {

				time += Time.deltaTime * Vector3.Distance(recordHead.Value.point, recordHead.Next.Value.point) / ((recordHead.Value.velocity.magnitude + recordHead.Next.Value.velocity.magnitude) / 2);
				
				if(time > recordHead.Next.Value.time)
					recordHead = recordHead.Next;

				Vector3 position = iTween.PointOnPath(recorder.path.controlPoints, time / recorder.GetRecordTime());
				
				float f = (time - recordHead.Value.time) / (recordHead.Next.Value.time - recordHead.Value.time);

				Quaternion rotation = Quaternion.Slerp(recordHead.Value.transform.rotation, recordHead.Next.Value.transform.rotation, f);

				target.transform.SetPositionAndRotation(position, rotation);

				character.velocity = Vector3.Slerp(recordHead.Value.velocity, recordHead.Next.Value.velocity, f);

			} else
				recordHead = recordHead.Next;
	}

	public void Enhance() {
		recorder.OptimizePath();
	}
}

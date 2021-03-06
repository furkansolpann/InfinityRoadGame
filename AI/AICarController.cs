using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Dot_TruckAI : System.Object
{
	public WheelCollider leftWheel;
	public GameObject leftWheelMesh;
	public WheelCollider rightWheel;
	public GameObject rightWheelMesh;
	public bool motor;
	public bool steering;
	
}

public class AICarController : MonoBehaviour {

	public float maxMotorTorque = 50;
	public float SteeringLeftAngle;
	public float SteeringRightAngle;

	public List<Dot_TruckAI> truck_Infos;
	private float steering = 0;


	public void VisualizeWheel(Dot_TruckAI wheelPair)
	{
		Quaternion rot;
		Vector3 pos;
		wheelPair.leftWheel.GetWorldPose ( out pos, out rot);
		wheelPair.leftWheelMesh.transform.position = pos;
		wheelPair.leftWheelMesh.transform.rotation = rot;
		wheelPair.rightWheel.GetWorldPose ( out pos, out rot);
		wheelPair.rightWheelMesh.transform.position = pos;
		wheelPair.rightWheelMesh.transform.rotation = rot;
	}

	public void Update()
	{

		if (transform.position.y < -5) {
			Destroy (gameObject);
		}


		foreach (Dot_TruckAI truck_Info in truck_Infos)
		{
			if (truck_Info.steering == true) {
				truck_Info.leftWheel.steerAngle = truck_Info.rightWheel.steerAngle = steering;
			}

			if (truck_Info.motor == true)
			{
				truck_Info.leftWheel.motorTorque = maxMotorTorque;
				truck_Info.rightWheel.motorTorque = maxMotorTorque;
			}



			VisualizeWheel(truck_Info);
		}

	}

	void OnTriggerEnter(Collider col){
		if (col.tag == "RoadRight") {
			steering = SteeringRightAngle;
			maxMotorTorque = 10;
		} else if (col.tag == "RoadRightEnd") {
			steering = 0;
			maxMotorTorque = 50;

		} else if (col.tag == "RoadLeft") {
			steering = -SteeringLeftAngle;
			maxMotorTorque = 10;
		} else if (col.tag == "RoadLeftEnd") {
			steering = 0;
			maxMotorTorque = 50;

		}
	}

	void OnCollisionEnter(Collision col){

		if (col.collider.tag == "LeftBarrier") {
			transform.Rotate(new Vector3(0,10,0));
		}
		if (col.collider.tag == "RightBarrier") {
			transform.Rotate(new Vector3(0,-10,0));
		}

	}


}
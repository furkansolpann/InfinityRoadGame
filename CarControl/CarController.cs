using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Dot_Truck : System.Object
{
	public WheelCollider leftWheel;
	public GameObject leftWheelMesh;
	public WheelCollider rightWheel;
	public GameObject rightWheelMesh;
	public bool motor;
	public bool steering;

}

public class CarController : MonoBehaviour {

	public float maxMotorTorque;
	public float maxSteeringAngle;
	public List<Dot_Truck> truck_Infos;



	public void VisualizeWheel(Dot_Truck wheelPair)
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

		float motor = maxMotorTorque * -1 *(Input.GetAxis("Vertical"));

		float steering = maxSteeringAngle * Input.GetAxis("Horizontal");
		float brakeTorque = Mathf.Abs(Input.GetAxis("Jump"));
		if (brakeTorque > 0.001) {
			brakeTorque = maxMotorTorque;
			motor = 0;
		} else {
			brakeTorque = 0;
		}

		foreach (Dot_Truck truck_Info in truck_Infos)
		{
			truck_Info.leftWheel.steerAngle = -90;
			truck_Info.rightWheel.steerAngle = -90;
				
			if (truck_Info.steering == true) {
				truck_Info.leftWheel.steerAngle = truck_Info.rightWheel.steerAngle = steering-90;
			}

			if (truck_Info.motor == true)
			{
				truck_Info.leftWheel.motorTorque = motor;
				truck_Info.rightWheel.motorTorque = motor;

			}

			truck_Info.leftWheel.brakeTorque = brakeTorque;
			truck_Info.rightWheel.brakeTorque = brakeTorque;

			VisualizeWheel(truck_Info);
		}

	}


}
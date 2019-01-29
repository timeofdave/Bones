using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class InputManager : MonoBehaviour {
	
	public GridManager gridManager;
	public PartManager partManager;
	public LayerMask clickableLayers;
	public Camera camera;
	public List<Rigidbody> wheels;
	public float motorTorque = 100f;
	public float angDrag = 0.1f;
	public float maxAngVel = 10000f;

	public float vertical = 10000f;

	public void CellClicked(GameObject cell)
	{
		gridManager.CellClicked(cell);
	}

	void Update() 
	{
		if (Input.GetButtonDown("Fire1")) 
		{
			RaycastHit hit;
			Ray ray = camera.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickableLayers)) {
				Transform objectHit = hit.transform;

				CellClicked(objectHit.gameObject);
			}
		}

		if (Input.GetKeyDown("w"))
		{
			gridManager.RaiseOrLowerGrid(1);
		}
		if (Input.GetKeyDown("s"))
		{
			gridManager.RaiseOrLowerGrid(-1);
		}

		vertical = Input.GetAxis("Vertical");

		foreach (var wheel in wheels) 
		{
			// wheel.motorTorque += 100;
			// wheel.AddTorque(wheel.gameObject.transform.right * motorTorque);
			wheel.angularDrag = angDrag;
			wheel.maxAngularVelocity = maxAngVel;
			// wheel.AddForceAtPosition(wheel.gameObject.transform.forward * motorTorque * vertical, wheel.gameObject.transform.position);
			wheel.AddRelativeTorque(Vector3.right * motorTorque * vertical);
		}

		if (Input.GetKeyDown("a"))
		{
			foreach (var wheel in wheels) 
			{
				// wheel.steerAngle = -15;
			}
		}
		if (Input.GetKeyDown("d"))
		{
			foreach (var wheel in wheels) 
			{
				// wheel.steerAngle = 15;
			}
		}

		if (Input.GetKeyDown("space"))
		{
			gridManager.PartCompleted();
		}

		if (Input.GetKeyDown("backspace"))
		{
			gridManager.PartCancelled();
		}

		if (Input.GetKeyDown("p"))
		{
			Physics.autoSimulation = false;
		}
		if (Input.GetKeyDown("l"))
		{
			partManager.JoinParts();
			Physics.autoSimulation = true;
		}
	}
	/*
	public void RegisterWheel(GameObject wheel, WheelCollider wheelCollider)
	{
		wheels.Add(wheelCollider);
	}
	*/
	public void RegisterWheel(GameObject wheel, Rigidbody wheelCollider)
	{
		wheels.Add(wheelCollider);
	}
}

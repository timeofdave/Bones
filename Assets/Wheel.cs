using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour {
	
	public GridManager gridManager;
	public InputManager inputManager;

	// Use this for initialization
	void Start () {
		gridManager = GameObject.FindObjectOfType<GridManager>();
		inputManager = GameObject.FindObjectOfType<InputManager>();

		//WheelCollider wheelCollider = GetComponentInChildren<WheelCollider>();
		Rigidbody wheelCollider = GetComponentInChildren<Rigidbody>();
		inputManager.RegisterWheel(gameObject, wheelCollider);
	}
}

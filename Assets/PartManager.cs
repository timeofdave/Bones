using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartManager : MonoBehaviour {

	public PartCreator partCreator;

	public List<Part> allParts;
	public float breakForce = 100000.0f;

	public void PartCompleted(List<Vector3> nodes)
	{
		if (nodes.Count == 2) 
		{
			Part newPart = partCreator.CreateStrut(nodes);
			allParts.Add(newPart);
		}

		if (nodes.Count == 1) 
		{
			Part newPart = partCreator.CreateWheel(nodes);
			allParts.Add(newPart);
		}

		if (nodes.Count == 5) 
		{
			Part newPart = partCreator.CreateCameraPoint(nodes);
			allParts.Add(newPart);
		}
	}

	// Add a joint between adjacent parts.
	public void JoinParts ()
	{
		// TODO: don't make two joints for each joint.
		// TODO: handle joints that cross each other.
		// TODO: allow connections to the middle of other struts.

		foreach (var thisPart in allParts) 
		{
			foreach (var thatPart in allParts) 
			{
				foreach (Vector3 thisNode in thisPart.nodes) 
				{
					foreach (Vector3 thatNode in thatPart.nodes) 
					{
						//print(thisNode.x + " ?= " + thatNode.x + "\n" + thisNode.y + " ?= " + thatNode.y + "\n" + thisNode.z + " ?= " + thatNode.z);
						if (Mathf.Approximately(thisNode.x, thatNode.x) &&
							Mathf.Approximately(thisNode.y, thatNode.y) &&
							Mathf.Approximately(thisNode.z, thatNode.z) &&
							!System.Object.ReferenceEquals(thisPart, thatPart)) 
						{
							FixedJoint joint = thisPart.gameObject.AddComponent<FixedJoint>();
							joint.connectedBody = thatPart.gameObject.GetComponent<Rigidbody>();
							joint.breakForce = breakForce;
						}
					}
				}
			}
		}
	}

	/*
	// Use this for initialization
	void Start () 
	{
		PopulatePartsList();
		JoinParts();
	}


	void PopulatePartsList()
	{
		GameObject[] partObjects = GameObject.FindGameObjectsWithTag("Part");

		foreach (var partObject in partObjects)
		{
			parts.Add(new Part(partObject));
		}
	}

	// Update is called once per frame
	void Update () 
	{
		
	}

	void FixedUpdate () 
	{
		
	}
	*/
}

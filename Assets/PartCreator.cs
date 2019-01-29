using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartCreator : MonoBehaviour {

	public PartManager partManager;
	public Camera theCamera;
	public GameObject strutPrefab;
	public GameObject wheelPrefab;
	public GameObject cameraPointPrefab;

	public Vector3 heading;
	public Vector3 direction;

	public Part CreateStrut(List<Vector3> nodes)
	{
		Vector3 node0 = nodes[0];
		Vector3 node1 = nodes[1];

		Vector3 averagePosition = new Vector3((node0.x + node1.x) / 2, (node0.y + node1.y) / 2, (node0.z + node1.z) / 2);

		heading = (node0 - node1);
		var distance = heading.magnitude;
		direction = heading / distance;

		Quaternion rotation = Quaternion.LookRotation(direction, heading);

		GameObject strut;
		strut = Instantiate(strutPrefab, averagePosition, rotation);
		strut.transform.localScale = new Vector3(strut.transform.localScale.x, strut.transform.localScale.y, strut.transform.localScale.z * distance);

		Part part = new Part(strut, nodes);
		return part;
	}

	public Part CreateWheel(List<Vector3> nodes)
	{
		Vector3 node0 = nodes[0];

		Vector3 position = node0;
		Quaternion rotation = Quaternion.identity;

		GameObject wheel;
		wheel = Instantiate(wheelPrefab, position, rotation);

		Part part = new Part(wheel, nodes);
		return part;
	}

	public Part CreateCameraPoint(List<Vector3> nodes)
	{
		Vector3 node0 = nodes[0];

		Vector3 position = node0;
		Quaternion rotation = Quaternion.identity;

		GameObject cameraPoint;
		cameraPoint = Instantiate(cameraPointPrefab, position, rotation);

		FollowScript followScript = theCamera.gameObject.GetComponent<FollowScript>();
		followScript.target = cameraPoint.transform;

		Part part = new Part(cameraPoint, nodes);
		return part;
	}

}

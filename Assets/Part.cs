using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Part : MonoBehaviour {

	public GameObject gameObject;
	public List<Vector3> nodes;
	public Quaternion rotation;

	public Part(GameObject obj)
	{
		this.gameObject = obj;
		this.nodes.Add(obj.transform.position);
		this.rotation = obj.transform.rotation;
	}

	public Part(GameObject obj, List<Vector3> nodes)
	{
		this.gameObject = obj;
		this.nodes = new List<Vector3>();
		this.nodes.AddRange(nodes);
		this.rotation = obj.transform.rotation;
	}
}

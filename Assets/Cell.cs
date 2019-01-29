using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour {
	
	public GridManager gridManager;
	public InputManager inputManager;

	// Use this for initialization
	void Start () {
		gridManager = GameObject.FindObjectOfType<GridManager>();
		inputManager = GameObject.FindObjectOfType<InputManager>();
	}
		
	void OnMouseUp() 
	{
		inputManager.CellClicked(gameObject);
	}
}

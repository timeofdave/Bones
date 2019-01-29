using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {

	public PartManager partManager;

	public GameObject[, ] grid;
	public GameObject gridPrefab;


	public Vector3Int gridPosition;
	public int gridWidth = 10;
	public int gridDepth = 10;
	public float scale = 1.0f;

	public int startHeight = 1;
	public int minHeight = 0;
	public int maxHeight = 10;

	// Keep track of whether this is the completion of a part,
	// the beginning of a part, or somewhere in between.
	public List<Vector3> currentNodes;

	public GameObject crosshairPrefab;
	public List<GameObject> crosshairs;

	// Use this for initialization
	void Start() {
		Physics.autoSimulation = false;
		BuildGrid();
	}

	void BuildGrid() {
		grid = new GameObject[gridWidth, gridDepth];

		for (int x = 0; x < gridWidth; x++)
		{
			for (int z = 0; z < gridDepth; z++)
			{
				GameObject gridInstance;
				gridInstance = Instantiate(gridPrefab, new Vector3(gridPosition.x + x, startHeight, gridPosition.z + z), Quaternion.identity);

				grid[x, z] = gridInstance;
			}
		}
	}

	public void CellClicked(GameObject cell)
	{
		Vector3 newNode = cell.transform.position;
		currentNodes.Add(newNode);

		// Make a new crosshair
		GameObject crosshairInstance;
		crosshairInstance = Instantiate(crosshairPrefab, cell.transform.position, Quaternion.identity);
		crosshairs.Add(crosshairInstance);
	}

	public void PartCompleted()
	{
		partManager.PartCompleted(currentNodes);

		PartFinishedReset();
	}

	public void PartCancelled()
	{
		PartFinishedReset();
	}

	public void PartFinishedReset()
	{
		// Empty current node list
		currentNodes = new List<Vector3>();

		// Delete all crosshairs
		foreach (var crosshair in crosshairs)
		{
			Destroy(crosshair);
		}
		crosshairs = new List<GameObject>();
	}

	public void RaiseOrLowerGrid(int movement)
	{
		for (int x = 0; x < gridWidth; x++)
		{
			for (int z = 0; z < gridDepth; z++)
			{
				var cellPosition = grid[x, z].transform.position;
				grid[x, z].transform.position = new Vector3(cellPosition.x, cellPosition.y + (movement * scale), cellPosition.z);
			}
		}
	}

}

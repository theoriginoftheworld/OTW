using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {

	public int columns = 6;
	public int rows = 4;
	public GameObject[] floorTiles;
	public GameObject[] toiletTiles;

	private Transform boardHolder;
	private List<Vector3> gridPositions = new List<Vector3>();

	void InitialiseList ()
	{
		gridPositions.Clear();
		for (int x = 1; x<columns; x++)
			for (int y = 1; y<rows; y++)
			{
				gridPositions.Add(new Vector3(x*.32f, y*.32f, 0f));
			}	
	}
	void BoardSetup ()
	{
		boardHolder = new GameObject("Board").transform;
		for (int x = 0; x!=columns; x++) {
			for (int y = 0; y!=rows; y++) {
				GameObject toInstantiate = floorTiles[0];	
				
				//if (x == -1 || x == columns || y == -1 || y == rows)
					//toInstantiate = outerWalls[0];
				
				GameObject instance = Instantiate(toInstantiate, new Vector3(x*.32f, y*.32f, 0f), Quaternion.identity) as GameObject;

				instance.transform.SetParent(boardHolder);
			}
		}	
	}
	Vector3 RandomPosition() {
		Debug.Log("Grid positions count" + gridPositions.Count);
		int randomIndex = Random.Range(0, gridPositions.Count);
		Vector3 randomPosition = gridPositions[randomIndex];
		gridPositions.RemoveAt(randomIndex);

		return randomPosition;
	}	
	Vector3 RandomPosition(float x, float y) {
		int randomIndex;

		return new Vector3(0f, 0f, 0f);
	}	
	void LayoutObject(GameObject[] ar, int min, int max) {
		for (int i = 0; i < max; i++)
		{
			Vector3 position = RandomPosition();
			GameObject obj = ar[0];

			Instantiate(obj, position, Quaternion.identity);
		}	
	}	
	public void SetUpScene (int level) {
		BoardSetup();
		InitialiseList();
		LayoutObject(toiletTiles, 0, 2);
	}	
}	

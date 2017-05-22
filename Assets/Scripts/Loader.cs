using UnityEngine;
using System;
using System.Collections.Generic;

public class Loader : MonoBehaviour {
	public GameObject gameManager;

	void Awake () {
		if (GameManager.instance == null)
			Instantiate(gameManager);
	}	
}	

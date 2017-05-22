using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	public static GameManager instance = null;

	private BoardManager boardScript;

	public bool doingSetup = true;

	void Awake () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);

		boardScript = GetComponent<BoardManager>();

		InitGame();
	}
	void InitGame() { 
		doingSetup = true;
		boardScript.SetUpScene(0);
	}

	void Update () {
		if (doingSetup)
			return;
	}	
}	

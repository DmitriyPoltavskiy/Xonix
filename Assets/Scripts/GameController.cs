using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();
	}

	public void OnApplicationQuit() {
		Application.Quit();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

	void Awake() {
		DontDestroyOnLoad (gameObject);
	}

	void Start () {

		var prefab = Resources.Load<GameObject> ("UITest");
		GameObject go = GameObject.Instantiate (prefab);
		go.name = "UITest";
		go.SetActive (true);
	}
}

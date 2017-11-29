using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadManagerScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
	}

}

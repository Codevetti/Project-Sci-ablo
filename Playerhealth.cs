using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerhealth : healthManager {

	public GameObject healthBar;
	public float percentHealth = 100;
	public Vector3 healthBarVector = new Vector3 (1, 1, 1);

	// Use this for initialization
	void Start () {

	playerHealth = 500;
	}
	
	// Update is called once per frame
	void Update () {
		percentHealth = (playerHealth / 500);
		healthBar.transform.localScale = new Vector3 (healthBarVector.x * percentHealth, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
	}
}

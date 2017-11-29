using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    Image healthBar;
    healthManager healthManager;

    float maxHealth;
    float currHealth;

	// Use this for initialization
	void Start ()
    {
        healthBar = GetComponent<Image>();
        healthManager = GameObject.FindGameObjectWithTag("Health Manager").GetComponent<healthManager>();
        maxHealth = healthManager.playerHealth;
        currHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update ()
    {
        currHealth = healthManager.playerHealth;
        healthBar.fillAmount = currHealth / maxHealth;
	}
}

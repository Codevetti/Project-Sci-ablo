using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemyHealthScript : MonoBehaviour {

    Image[] images;
    Image healthBar;
    crawlerAI crawlerAI;

    float maxHealth;
    float currHealth;

	// Use this for initialization
	void Start () {
        crawlerAI = transform.GetComponentInParent<crawlerAI>();
        maxHealth = crawlerAI.mobHealth;

        images = transform.GetComponentsInChildren<Image>();
        healthBar = images[1];
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(Camera.main.transform);

        currHealth = crawlerAI.mobHealth;

        healthBar.fillAmount = currHealth / maxHealth;
	}
}

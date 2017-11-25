using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienAnimationsScript : MonoBehaviour {
    public Animator anim;
    bool run = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (run)
            {
                run = false;
                anim.SetBool("walk", false);
                //rend.enabled = false;
            }
            else
            {
                run = true;
                anim.SetBool("walk", true);
                //rend.enabled = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && !run)
        {
            anim.SetTrigger("attack");
        }
    }
}

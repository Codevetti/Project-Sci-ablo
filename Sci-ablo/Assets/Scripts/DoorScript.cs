using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {

    public Animator doorAnim;
    AudioSource doorOpen;

    void Start()
    {
        doorOpen = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            //open the door
            doorAnim.SetBool("Open", true);
            doorOpen.Play();
        }
    }
    void OnTriggerExit(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            //close the door
            doorAnim.SetBool("Open", false);
        }
    }
}

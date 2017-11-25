using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepScript : MonoBehaviour {

    public GameObject audioObj;
    AudioSource [] footsteps;

    float rand = 0f;

    void Start()
    {
        footsteps = audioObj.GetComponents<AudioSource>();
    }

    public void Footstep()
    {
        rand = Random.Range(0, 30);

        if(rand >= 0 && rand < 10)
        {
            footsteps[0].Play();
        }
        else if(rand >= 10 && rand < 20)
        {
            footsteps[1].Play();
        }
        else
        {
            footsteps[2].Play();
        }
    }
}

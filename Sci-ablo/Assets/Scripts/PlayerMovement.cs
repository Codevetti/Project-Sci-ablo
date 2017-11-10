﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour {

    public Animator anim;
    public Transform target;
    public NavMeshAgent agent;
    public Camera cam;

    float dist;

    bool setPath = true;

    Ray ray;
    RaycastHit hit;
    int layerMask;
    int runId = Animator.StringToHash("Run");
    // Use this for initialization
    void Start () {

        layerMask = LayerMask.GetMask("Floor");

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray,out hit, layerMask))
            {
                target.position = hit.point;
                agent.SetDestination(target.position);
                setPath = true;
            }
        }
        
        dist = agent.remainingDistance;
        if (setPath)
        {
            if (dist != Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance < 0.1)
            {
                //arrived at destination
                anim.SetBool("Run", false);
            }
            else
            {
                if (!anim.GetBool(runId))
                    anim.SetBool("Run", true);
            }
        }
        else
        {
            anim.SetBool("Run", false);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.transform == target)
        {
            setPath = false;
        }
    }

}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor.Animations;
using System;

public class crawlerAI : MonoBehaviour {

    
    public UnityEngine.AI.NavMeshAgent mob;
    public GameObject player;
    public Animator anim;
    private bool aiOn;
    public bool isSpitter;
    public float mobHealth;

    private bool sighted;
    private bool playerNearby;
    private float distanceToPlayer;
    public Transform playerLocation;
    private float visionRange = 60.0f;
    private float rotateSpeed = 1.0f;

    public Transform center;
    public float distanceToCenter;
    public bool inGroup;
    public bool attacking = false;
    public bool meleeAttack = false;
    public bool spitAttack = false;

    void Start ()
    {
        aiOn = true;

        anim = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player");
        playerLocation = player.transform;

        aiState = State.standby;

        GameObject.FindGameObjectWithTag("Health Manager").GetComponent<healthManager>().mobs.Add(this.gameObject);

        StartCoroutine("FiniteStateMachine");
    }

	void FixedUpdate ()
    {
        Debug.DrawRay(transform.position, transform.up * 5, Color.red);

        distanceToPlayer = Vector3.Distance(playerLocation.position, transform.position);
        distanceToCenter = Vector3.Distance(center.position, transform.position);
        if (distanceToCenter >= 15)
        {
            inGroup = false;
            aiState = State.regroup;
        }
    }


    public enum State
    {
        standby,
        attack,
        spit,
        regroup,
        moveto
    }

    public State aiState;

    IEnumerator FiniteStateMachine()
    {
        while (aiOn)
        {
            switch (aiState)
            {
                case State.standby:
                    Standby();
                    break;
                case State.attack:
                    Attack();
                    break;
                case State.spit:
                    Spit();
                    break;
                case State.regroup:
                    Regroup();
                    break;
                case State.moveto:
                    Moveto();
                    break;
            }
            yield return null;
        }
    }

    private void Standby()
    {
        //Debug.Log("idle");
        anim.SetTrigger("Idle");
        if(distanceToPlayer <= visionRange)
        {
            aiState = State.moveto;
        }
    }

    private void Attack()
    {
        //Debug.Log("attack");
        //Vector3 forward = transform.TransformDirection(Vector3.forward);
        //Vector3 toTarget = playerLocation.position - transform.position;
        //float angle = Vector3.Dot(forward, toTarget);

        Vector3 targetDir = playerLocation.position - transform.position;
        float step = rotateSpeed * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f));

        //if (angle > 0.5f && angle < 1.5f)
        if(distanceToPlayer <= 2.2 && player.GetComponent<CharacterController>().velocity.magnitude > 0)
        {
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
            gameObject.GetComponent<NavMeshObstacle>().enabled = true;
            anim.SetTrigger("Attacking");
        }

        else
        {
            gameObject.GetComponent<NavMeshObstacle>().enabled = false;
            gameObject.GetComponent<NavMeshAgent>().enabled = true;
            aiState = State.moveto;
        }

    }

    private void Spit()
    {

    }

    private void Regroup()
    {
        //Debug.Log("regroup");
        anim.SetTrigger("Moving");

        if (distanceToCenter <= 13 )
        {
            aiState = State.moveto;
        }
        else
        {
            mob.destination = center.position;
        }
    }

    private void Moveto()
    {
        //Debug.Log("moveto");
        
        mob.transform.LookAt(new Vector3(playerLocation.position.x, transform.position.y, playerLocation.position.z));
        if (distanceToPlayer <= 2.2)
        {
            aiState = State.attack;
        }

        else if (inGroup == true)
        {
            anim.SetTrigger("Moving");
            mob.destination = playerLocation.position;
        }
        else
        {
            aiState = State.regroup;
        }
    }
}

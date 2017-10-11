using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;
using System;

public class crawlerAI : MonoBehaviour {

    public UnityEngine.AI.NavMeshAgent mob;
    public GameObject player;
    public bool aiOn;

    private bool sighted;
    private bool playerNearby;
    private float distanceToPlayer;
    public Transform playerLocation;

    public float sightLength = 50.0f;
    public float mobEyeHeight;
    private float visionAngle;
    private float visionRange = 60.0f;


    void Start ()
    {
        aiState = State.standby;

        StartCoroutine("FiniteStateMachine");
    }

	void Update ()
    {
		
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
        
    }

    private void Attack()
    {
        
    }

    private void Spit()
    {
        
    }

    private void Regroup()
    {

    }

    private void Moveto()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour {

    public Animator anim;
    public Transform target;
    Transform enemyTarget;
    public NavMeshAgent agent;
    public Camera cam;
    public PlayerEquipmentManager equipmentManager;
    public healthManager healthManager;
    public LaserScript laserScript;
    
    float dist;

    bool setPath = true;
    bool attacking = false;
    bool rotate = false;
    bool isShooting = false;

    Ray ray;
    RaycastHit hit;
    int floorMask;
    int layerMaskEnemy;
    int runId = Animator.StringToHash("Run");
    
    // Use this for initialization
    void Start ()
    {

        floorMask = LayerMask.GetMask("Floor");
        layerMaskEnemy = LayerMask.GetMask("Enemy");

    }
	
	// Update is called once per frame
	void Update () {

        //if not attacking, can select a location on the screen to navigate to
        if (!attacking)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                ray = cam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, floorMask))
                {
                    target.position = hit.point;
                    agent.SetDestination(target.position);
                    agent.isStopped = false;
                    setPath = true;
                }
            }
        }

        //if the player has a weapon and that weapon is a hammer
        if (Input.GetMouseButtonDown(1) && equipmentManager.equipWeapon && equipmentManager.isHammer)
        {
            //do hammer stuff
            MeleeAttack();
        }

        if (Input.GetMouseButton(1) && equipmentManager.equipWeapon && equipmentManager.isGun)
        {
            //do gun stuff
            Shoot();
        }
        else if(Input.GetMouseButtonUp(1) && equipmentManager.equipWeapon && equipmentManager.isGun)//finish shooting
        {
            rotate = false;
            isShooting = false;
            laserScript.activate = false;
            anim.SetBool("Aim", false);
        }

        //while the player is attacking rotate towards the enemy
        if (rotate && !isShooting)
        {
            RotateTowardsEnemy();
        }
        else if(rotate && isShooting)
        {
            RotateTowardsTarget();
        }
        
        dist = agent.remainingDistance;

        if (setPath)
        {
            //if the player has a hammer and is attacking move to set destination to attack
            if (attacking && equipmentManager.isHammer)
            {
                //check if within two meters of destination, then attack
                if(agent.remainingDistance <= 2f && agent.hasPath)
                {
                    //Debug.Log("attack now");
                    attacking = false;
                    setPath = false;
                    rotate = true;
                    agent.isStopped = true;
                    anim.SetTrigger("Attack");
                }
            }
            //if the path is impossible or completed
            if (dist != Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance < 0.1)
            {
                //arrived at destination
                anim.SetBool("Run", false);
            }
            else
            {
                //if the path is possible and the path is set then the player should be running.
                if (!anim.GetBool(runId) && setPath)
                    anim.SetBool("Run", true);
            }

            
        }
        else
        {
            //if the path is not set the player should not be running
            anim.SetBool("Run", false);
        }
    }

    void MeleeAttack()
    {
        //set the destination as the enemy
        //after the player is within one meter attack
        ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, layerMaskEnemy))
        {
            if (hit.transform.tag == "Mob")
            {
                enemyTarget = hit.transform;
                //target.transform.position = hit.transform.position;
                agent.SetDestination(enemyTarget.position);
                setPath = true;
                attacking = true;
                agent.isStopped = false;
            }
        }
    }

    void Shoot()
    {
        ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, floorMask))
        {
            //enemyTarget.position = hit.point;
            target.position = hit.point;
            rotate = true;
            isShooting = true;
            setPath = false;
            agent.isStopped = true;
            anim.SetBool("Aim", true);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.transform == target)
        {
            setPath = false;
            agent.isStopped = true;
        }
    }

    public void finishAttack(string message)
    {
        //Debug.Log(message);
        attacking = false;
        rotate = false;
        healthManager.damageHealth(enemyTarget.gameObject, 50);
    }

    public void ActivateLaser()
    {
        laserScript.activate = true;
    }

    void RotateTowardsEnemy()
    {
        //gets a direction from the enemy's current position and rotates the player towards that location
        Vector3 enemyDirection = enemyTarget.position - transform.position;
        enemyDirection.y = 0f;
        transform.forward = Vector3.Lerp(transform.forward, enemyDirection, 5 * Time.deltaTime);
    }

    void RotateTowardsTarget()
    {
        Vector3 enemyDirection = target.position - transform.position;
        enemyDirection.y = 0f;
        transform.forward = Vector3.Lerp(transform.forward, enemyDirection, 5 * Time.deltaTime);
    }
}

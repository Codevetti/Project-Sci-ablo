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
    
    float dist;

    bool setPath = true;
    bool attacking = false;
    bool rotate = false;

    Ray ray;
    RaycastHit hit;
    int layerMask;
    int layerMaskEnemy;
    int runId = Animator.StringToHash("Run");

    Vector3 newDirection;

    // Use this for initialization
    void Start () {

        layerMask = LayerMask.GetMask("Floor");
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
                if (Physics.Raycast(ray, out hit, layerMask))
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
            MeleeAttack();
        }

        //while the player is attacking rotate towards the enemy
        if (rotate)
        {
            newDirection = new Vector3(enemyTarget.position.x - transform.position.x, 0, enemyTarget.position.z - transform.position.z);
            transform.forward = Vector3.Lerp(transform.forward, newDirection, 5 * Time.deltaTime);
        }
        
        dist = agent.remainingDistance;
        if (setPath)
        {
            if (attacking)
            {
                //check if within on meter of destination
                if(agent.remainingDistance <= 2f && agent.hasPath)
                {
                    Debug.Log("attack now");
                    attacking = false;
                    setPath = false;
                    rotate = true;
                    agent.isStopped = true;
                    anim.SetTrigger("Attack");
                }
            }
            if (dist != Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance < 0.1)
            {
                //arrived at destination
                anim.SetBool("Run", false);
            }
            else
            {
                if (!anim.GetBool(runId) && setPath)
                    anim.SetBool("Run", true);
            }

            
        }
        else
        {
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

    void OnTriggerEnter(Collider col)
    {
        if(col.transform == target)
        {
            setPath = false;
        }
    }

    public void finishAttack(string message)
    {
        Debug.Log(message);
        attacking = false;
        rotate = false;
        healthManager.damageHealth(enemyTarget.gameObject, 50);
    }

    void RotateTowards(Transform rotTarget)
    {

    }

}

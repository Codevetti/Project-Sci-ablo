using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour {

    
    public Animator anim;
    public Transform target;

    [SerializeField]
    private Transform enemyTarget;


    public NavMeshAgent agent;
    public Camera cam;
    public PlayerEquipmentManager equipmentManager;
    public healthManager healthManager;
    public LaserScript laserScript;
    
    float dist;
    public float hammerDamage; //set this value

    bool setPath = true;
    bool attacking = false;
    bool rotate = false;
    bool isShooting = false;

    Ray ray;
    RaycastHit hit;
    int floorMask;
    int enemyMask;
    int runId = Animator.StringToHash("Run");

    bool damageCoroutine;

    public Transform meleeAudio; //needs to be set
    AudioSource[] meleeHits;
    
    // Use this for initialization
    void Start ()
    {
        floorMask = LayerMask.GetMask("Floor");
        enemyMask = LayerMask.GetMask("Enemy");

        meleeHits = meleeAudio.GetComponents<AudioSource>();

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
            SetUpMelee();
        }
        
        if(Input.GetMouseButtonDown(1) && equipmentManager.equipWeapon && equipmentManager.isGun)
        {
            laserScript.Reset();
            
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
            laserScript.gunLight.enabled = false;
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
                if(agent.remainingDistance <= 2f)
                {
                    MeleeAttack();
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

    void SetUpMelee()
    {
        //set the destination as the enemy
        //after the player is within one meter attack
        ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, enemyMask))
        {
            if (hit.transform.tag == "Mob")
            {
                enemyTarget = hit.transform;
                agent.SetDestination(enemyTarget.position);
                setPath = true;
                attacking = true;
                agent.isStopped = false;

                //find the actual distance from the enemy origin to the player origin. If that distance is less than 2.25 meters then attack the enemy.
                float distanceToEnemy = Vector3.Distance(transform.position, enemyTarget.position);
                if(distanceToEnemy < 2.25f)
                {
                    MeleeAttack();
                }
            }
        }
    }

    void MeleeAttack()
    {
        attacking = false;
        setPath = false;
        rotate = true;
        agent.isStopped = true;
        anim.SetTrigger("Attack");
    }

    void Shoot()
    {
        ray = cam.ScreenPointToRay(Input.mousePosition);
        //find the point at which the player should be aiming the weapon.
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

        Vector3 gunBarrelZero = new Vector3(equipmentManager.gScript.gunBarrel.position.x, 0, equipmentManager.gScript.gunBarrel.position.z);
        RaycastHit[] enemyInfo = Physics.CapsuleCastAll(equipmentManager.gScript.gunBarrel.position, gunBarrelZero, 0.3f, equipmentManager.gScript.gunBarrel.forward, 50f, enemyMask);
        Debug.DrawLine(equipmentManager.gScript.gunBarrel.position, gunBarrelZero, Color.red);
        if (enemyInfo != null)
        {
            if (!damageCoroutine)
            {
                StartCoroutine(LaserHit(enemyInfo));
            }
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
        healthManager.damageHealth(enemyTarget.gameObject, hammerDamage);
        foreach (Collider col in equipmentManager.hScript.enemiesHit)
        {
            if(col.gameObject != enemyTarget.gameObject)
            {
                healthManager.damageHealth(col.gameObject, hammerDamage / 2f);
            }
        }
        PlayMeleeHit();
    }

    public void ActivateLaser()
    {
        laserScript.activate = true;
        laserScript.gunLight.enabled = true;
        laserScript.PlayAudio();
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

    IEnumerator LaserHit(RaycastHit [] raycastHits)
    {
        damageCoroutine = true;
        foreach(RaycastHit raycastHit in raycastHits)
        {
            healthManager.damageHealth(raycastHit.collider.gameObject, 15);
        }
        yield return new WaitForSeconds(.3f);
        damageCoroutine = false;
    }

    void PlayMeleeHit()
    {
        float rand = Random.Range(0, 40);
        if(rand >= 0 && rand < 10)
        {
            meleeHits[0].Play();
        }
        else if(rand >= 10 && rand < 20)
        {
            meleeHits[1].Play();
        }
        else if(rand >= 20 && rand < 30)
        {
            meleeHits[2].Play();
        }
        else
        {
            meleeHits[3].Play();
        }
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSocketScript : MonoBehaviour {

    public Transform socket;
    public Transform hand;
    public Animator anim;
    public MeshRenderer rend;

    bool equipHammer = true;
    bool run = false;

	// Use this for initialization
	void Start () {
        transform.position = socket.position;
        transform.rotation = socket.rotation;
        transform.parent = socket;
        socket.parent = hand;
        anim.SetBool("EquipHammer", true);
	}
	
	// Update is called once per frame
	void Update () {
       if (Input.GetKeyDown(KeyCode.Space))
        {
            if (equipHammer)
            {
                equipHammer = false;
                anim.SetBool("EquipHammer", false);
                rend.enabled = false;
            }
            else
            {
                equipHammer = true;
                anim.SetBool("EquipHammer", true);
                rend.enabled = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (run)
            {
                run = false;
                anim.SetBool("Run", false);
                //rend.enabled = false;
            }
            else
            {
                run = true;
                anim.SetBool("Run", true);
                //rend.enabled = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && equipHammer)
        {
            anim.SetTrigger("Attack");
        }
    }
}

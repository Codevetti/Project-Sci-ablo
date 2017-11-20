using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipmentManager : MonoBehaviour {

    public bool equipWeapon;
    public bool hasWeapon = false;
    //takes the hammer or gun object and orients it in the players hand.
    //makes the weapon a child of the socket.
    public Transform hammerSocket;
    public Transform gunSocket;
    public Transform hammer;
    public Transform gun;
    
    public Animator anim;
    public MeshRenderer rend;

    //When a weapon is instantiated, the weapon's script will apply itself to the weapon variable.
    //That script will also determine whether the weapon is a gun or a hammer.
    public bool isGun;
    public bool isHammer;

    public GunScript gScript;

    // Update is called once per frame
    void Update()
    {
        if (equipWeapon)
        {
            if(!hasWeapon)
                setUpWeapon();
        }
        else
        {
            unequipWeapon();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (equipWeapon)
                equipWeapon = false;
            else if (!equipWeapon)
                equipWeapon = true;
        }
    }

    void setUpWeapon()
    {
        if (isHammer)
        {
            equipHammer();
        }
        else if (isGun)
        {
            equipGun();
        }
    }
    
    void equipHammer()
    {
        if (rend)
        {
            if (!rend.enabled)
            {
                rend.enabled = true;
            }
        }
        if (hammer)
        {
            hammer.position = hammerSocket.position;
            hammer.rotation = hammerSocket.rotation;
            hammer.parent = hammerSocket;
        }
        anim.SetBool("EquipHammer", true);
        hasWeapon = true;
        isGun = false;
    }

    void equipGun()
    {
        if (rend)
        {
            if (!rend.enabled)
            {
                rend.enabled = true;
            }
        }
        if (gun)
        {
            gun.position = gunSocket.position;
            gun.rotation = gunSocket.rotation;
            gun.parent = gunSocket;
        }
        anim.SetBool("EquipGun", true);
        hasWeapon = true;
        isHammer = false;
    }

    void unequipWeapon()
    {
        //TODO: needs to be changed to destroy the object rather than just disabling the renderer.
        if (rend)
        {
            rend.enabled = false;
        }
        hasWeapon = false;
        
        if (isHammer)
            anim.SetBool("EquipHammer", false);
        else if (isGun)
            anim.SetBool("EquipGun", false);

    }
}

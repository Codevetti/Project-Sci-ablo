using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour {

    GameObject player;
    PlayerEquipmentManager equipmentManager;
    public Transform gunBarrel;

    void Start()
    {
        //find the player and get the equipment script
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            equipmentManager = player.GetComponent<PlayerEquipmentManager>();
            equipmentManager.gScript = this;
        }
        gunBarrel = transform.GetChild(0);
    }

    void Update()
    {
        //equip this weapon to the player.
        //this wouldn't happen in update, but would happen when the weapon is put in the proper inventory slot.
        if (!equipmentManager.hasWeapon)
        {
            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                equipmentManager.gun = transform;
                equipmentManager.rend = transform.GetComponent<MeshRenderer>();
                equipmentManager.isGun = true;
                equipmentManager.equipWeapon = true;
                equipmentManager.isHammer = false;
            }
        }
    }
}

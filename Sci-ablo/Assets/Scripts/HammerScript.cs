using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerScript : MonoBehaviour {

    GameObject player;
    PlayerEquipmentManager equipmentManager;

	void Start()
    {
        //find the player and get the equipment script
        if(GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            equipmentManager = player.GetComponent<PlayerEquipmentManager>();
        }
    }

    void Update()
    {
        //equip this weapon to the player.
        //this wouldn't happen in update, but would happen when the weapon is put in the proper inventory slot.
        if (!equipmentManager.hasWeapon)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                equipmentManager.hammer = transform;
                equipmentManager.rend = transform.GetComponent<MeshRenderer>();
                equipmentManager.isHammer = true;
                equipmentManager.equipWeapon = true;
                equipmentManager.isGun = false;
            }
        }
    }

}

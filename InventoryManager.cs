using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : Inventory{

	public PlayerEquipmentManager manager;
	public Button equip;
	public Button unequip;

	public void equipItem(int index){

		if (!items [index].isEquipped) {
			if (items [index].isHammer) {
				manager.equipHammer ();
			} else if (items [index].isGun) {
				manager.equipGun ();
			} else {
				Debug.Log ("No Item In Slot");
			}
		} else {

			manager.unequipWeapon ();
		}
	}

	public void removeItem(int index){

		RemoveItem (items [index]);
	}
}

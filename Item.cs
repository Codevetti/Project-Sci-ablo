using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class Item : ScriptableObject {

	public Sprite sprite;
	public bool isHammer;
	public bool isGun;
	public string name;
	public bool isEquipped;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotate : MonoBehaviour {
    public float speed;
    public Vector3 axis;
	// Update is called once per frame
	void Update ()
    {
        transform.RotateAround(transform.position, axis, speed * Time.deltaTime);
        Debug.DrawRay(transform.position, axis * 1000,Color.red);
    }
}

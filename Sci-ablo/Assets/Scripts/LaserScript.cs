using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour {

    LineRenderer lineRen;
    Vector3 origPos;
    public float speed;

    void Start()
    {
        lineRen = GetComponent<LineRenderer>();
        origPos = Vector3.zero;
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            lineRen.SetPosition(0, origPos);
            lineRen.SetPosition(1, origPos);
        }
        if (Input.GetMouseButton(0))
        {
            //lineRen.SetPosition(1, lineRen.GetPosition(1) + Vector3.forward * speed * Time.deltaTime);
            lineRen.SetPosition(1, Vector3.Lerp(lineRen.GetPosition(1), lineRen.GetPosition(0) + Vector3.forward * 50, 3 * speed * Time.deltaTime));
        }
        else
        {
            lineRen.SetPosition(0, Vector3.Lerp(lineRen.GetPosition(0), lineRen.GetPosition(1), 5 * speed * Time.deltaTime));
        }
	}
}

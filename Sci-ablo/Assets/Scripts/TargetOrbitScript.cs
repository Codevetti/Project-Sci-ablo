using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetOrbitScript : MonoBehaviour {

    public Transform camPivot;
    Vector3 camPosition;
    Vector3 camDirection;
    public float camRadius;
    public float camHeight;
    public float camRotationSpeed;
    float mouseX;
    float mouseWheel;
    // Use this for initialization
    void Start () {
        camPosition = new Vector3(-camPivot.forward.x * camRadius, camPivot.position.y + camHeight, -camPivot.forward.z * camRadius);
        transform.position = camPosition;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        mouseX = Input.GetAxis("Mouse X");
        
        if (Input.GetKey(KeyCode.Mouse2))
        {
            if (mouseX < 0)
            {
                transform.RotateAround(camPivot.position, -transform.up, Time.deltaTime * camRotationSpeed);
            }
            else if (mouseX > 0)
            {
                // transform.Translate(Vector3.right * camRotationSpeed * Time.deltaTime);
                transform.RotateAround(camPivot.position, transform.up, Time.deltaTime * camRotationSpeed);
            }
        }
        
    }

    void Update()
    {
        mouseWheel = Input.GetAxis("Mouse ScrollWheel");
        transform.LookAt(camPivot);
        transform.Translate(Vector3.forward * mouseWheel* 30 * Time.deltaTime);
    }
}

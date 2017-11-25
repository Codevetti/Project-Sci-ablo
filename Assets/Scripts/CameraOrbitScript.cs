using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbitScript : MonoBehaviour {

    //public CameraPivotScript camTargetScript;
    public Transform camPivot;
    public Transform camTargetPos;
    public Camera cam;
    public float camRadius = 9.25f;
    public float camHeight = 9.0f;
    public float camMoveSpeed;

    Vector3 camPosition;

    bool fixedPos = false;

	// Use this for initialization
	void Start () {
        //position camera behind player, with a set height value.
        camPosition = new Vector3(-camPivot.forward.x * camRadius, camPivot.position.y + camHeight, -camPivot.forward.z * camRadius);
        transform.position = camPosition;
	}
	
	// Update is called once per frame
	void Update () {
        
        camPosition.x = Mathf.Lerp(transform.position.x, camTargetPos.position.x, Time.deltaTime * camMoveSpeed);
        camPosition.y = Mathf.Lerp(transform.position.y, camTargetPos.position.y, Time.deltaTime * camMoveSpeed);
        camPosition.z = Mathf.Lerp(transform.position.z, camTargetPos.position.z, Time.deltaTime * camMoveSpeed);

        transform.position = camPosition;
        transform.LookAt(camPivot);   
    }
}

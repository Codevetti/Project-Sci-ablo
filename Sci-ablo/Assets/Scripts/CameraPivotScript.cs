using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPivotScript : MonoBehaviour {

    public Transform playerTransform;
    public Camera cam;
    Vector3 newPos;
    Vector3 directionW;
    Vector3 directionA;
    Vector3 directionS;
    Vector3 directionD;
    bool fixedPos = true;
    public float speed;
	// Use this for initialization
	void Start () {
        newPos = new Vector3(playerTransform.position.x, playerTransform.position.y + 1, playerTransform.position.z);
        transform.position = newPos;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        
        //if (Input.GetKeyDown(KeyCode.Space) && fixedPos)
        //{
        //    fixedPos = false;
        //}
        //else if(Input.GetKeyDown(KeyCode.Space) && !fixedPos)
        //{
        //    fixedPos = true;
        //}
        
    }

    void Update()
    {
        //if (!fixedPos)
        //{//allow free movement
        //    if (Input.GetKey(KeyCode.W))
        //    {
        //        directionW = new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z);
        //        transform.Translate(directionW * Time.deltaTime * speed);
        //    }
        //    if (Input.GetKey(KeyCode.A))
        //    {
        //        directionA = new Vector3(-cam.transform.right.x, 0, -cam.transform.right.z);
        //        transform.Translate(directionA * Time.deltaTime * speed);
        //    }
        //    if (Input.GetKey(KeyCode.S))
        //    {
        //        directionS = new Vector3(-cam.transform.forward.x, 0, -cam.transform.forward.z);
        //        transform.Translate(directionS * Time.deltaTime * speed);
        //    }
        //    if (Input.GetKey(KeyCode.D))
        //    {
        //        directionD = new Vector3(cam.transform.right.x, 0, cam.transform.right.z);
        //        transform.Translate(directionD * Time.deltaTime * speed);
        //    }
        //}
        //if the position is fixed, lerp to the player's current position
        //if (fixedPos)
        //{
        newPos.x = Mathf.Lerp(transform.position.x, playerTransform.position.x, Time.deltaTime * 3);
        newPos.y = Mathf.Lerp(transform.position.y, playerTransform.position.y + 1, Time.deltaTime * 3);
        newPos.z = Mathf.Lerp(transform.position.z, playerTransform.position.z, Time.deltaTime * 3);
        transform.position = newPos;
        //}
    }

    public void fixCamPosition(bool fixedPos)
    {
        this.fixedPos = fixedPos;
    }
}

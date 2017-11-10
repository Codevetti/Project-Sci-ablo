using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienMaterialSwapper : MonoBehaviour {
    public Material mat1;
    public Material mat2;
    public Material mat3;
    public Material mat4;
    public SkinnedMeshRenderer alienRend;

    int currMat = 0;
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currMat += 1;
            if (currMat == 1)
            {
                alienRend.material = mat1;
            }
            else if(currMat == 2)
            {
                alienRend.material = mat2;
            }
            else if(currMat == 3)
            {
                alienRend.material = mat3;
            }
            else if(currMat == 4)
            {
                alienRend.material = mat4;
                currMat = 0;
            }
        }
	}
}

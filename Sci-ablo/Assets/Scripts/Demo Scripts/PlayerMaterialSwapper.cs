using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMaterialSwapper : MonoBehaviour {

    public Material[] matSet1;
    public Material[] matSet2;
    public Material[] matSet3;
    public SkinnedMeshRenderer[] playerMeshRend;

    public int currMat = 1;

    public bool croutineRunning = false;
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if(currMat == 1)
            {
                playerMeshRend[0].material = matSet1[0];
                playerMeshRend[1].material = matSet1[1];
                playerMeshRend[2].material = matSet1[2];
                playerMeshRend[3].material = matSet1[3];
                playerMeshRend[4].material = matSet1[4];
            }
            else if (currMat == 2)
            {
                playerMeshRend[0].material = matSet2[0];
                playerMeshRend[1].material = matSet2[1];
                playerMeshRend[2].material = matSet2[2];
                playerMeshRend[3].material = matSet2[3];
                playerMeshRend[4].material = matSet2[4];
            }
            else if (currMat == 3)
            {
                playerMeshRend[0].material = matSet3[0];
                playerMeshRend[1].material = matSet3[1];
                playerMeshRend[2].material = matSet3[2];
                playerMeshRend[3].material = matSet3[3];
                playerMeshRend[4].material = matSet3[4];
                currMat = 0;
            }
            currMat += 1;
        }
	}
}

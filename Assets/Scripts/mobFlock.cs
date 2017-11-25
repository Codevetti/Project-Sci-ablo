using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mobFlock : MonoBehaviour {

    public GameObject mob;
    public Transform pos;
    public int spawnDistance = 5;
    private int stepSize = 1;
    public int numMob;
    public GameObject[] mobs;

    public int GetNewPos()
    {
        int randomPos = Random.Range(-spawnDistance, spawnDistance);
        int numSteps = (int)Mathf.Floor(randomPos / stepSize);
        int adjustedPos = numSteps * stepSize;
        return adjustedPos;
    }

    void Start()
    {
        numMob = Random.Range(5, 11);
        mobs = new GameObject[numMob];

        for (int i = 0; i < numMob; i++)
        {
            mobs[i] = Instantiate(mob, new Vector3(GetNewPos(), GetNewPos(), GetNewPos()), Quaternion.identity);
            crawlerAI ai = mobs[i].GetComponent<crawlerAI>();
            ai.center = gameObject.transform;
            if(i%2 == 0)
                ai.isSpitter = true;
        }
    }

    void FixedUpdate()
    {
        center(mobs);
        Debug.DrawRay(transform.position, transform.up * 8, Color.yellow);
    }

    void center(GameObject[] mobs)
    {
        mobs = this.mobs;
        Bounds bounds = new Bounds(mobs[0].transform.position, Vector3.zero);
        for (var i = 1; i < mobs.Length; i++)  //averaging the mobs position using bounds
            bounds.Encapsulate(mobs[i].transform.position);
        this.transform.position = bounds.center;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mobFlock : MonoBehaviour {

    public GameObject[] mob;
    public Transform pos;
    public int spawnDistance = 5;
    private int stepSize = 1;
    private int numMob;
    private int rarity;
    private int temp;
    public List<GameObject> mobs;

    void Start()
    {
        spawnMobs(pos);
    }

    void FixedUpdate()
    {
        if (mobs.Count > 0)
        {
            center(mobs);
            Debug.DrawRay(transform.position, transform.up * 8, Color.yellow);
        }

        else
        {
            DestroyObject(this.gameObject);
        }
    }

    public int GetNewPos()
    {
        int randomPos = Random.Range(-spawnDistance, spawnDistance);
        int numSteps = (int)Mathf.Floor(randomPos / stepSize);
        int adjustedPos = numSteps * stepSize;
        return adjustedPos;
    }

    void spawnMobs(Transform pos)
    {
        pos = this.pos;
        numMob = Random.Range(3, 7);

        

        for (int i = 0; i < numMob; i++)
        {

            temp = (int)Random.Range(0, 21);
            if (temp < 13 && temp > -1)
                rarity = 0;
            else if (temp < 20 && temp > 12)
                rarity = 1;
            else
                rarity = 2;

            mobs.Add(Instantiate(mob[rarity], new Vector3(pos.position.x + GetNewPos(), 0, pos.position.z + GetNewPos()), Quaternion.identity));
            crawlerAI ai = mobs[i].GetComponent<crawlerAI>();
            ai.mobCenter = this.gameObject;

            if (i % 2 == 0)
                ai.isSpitter = true;
        }
        
    }

    void center(List<GameObject> mobs)
    {
        mobs = this.mobs;
        Bounds bounds = new Bounds(mobs[0].transform.position, Vector3.zero);
        for (var i = 1; i < mobs.Count; i++)  //averaging the mobs position using bounds
            bounds.Encapsulate(mobs[i].transform.position);
        this.transform.position = bounds.center;
    }
}

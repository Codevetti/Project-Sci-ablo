using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour {

    LineRenderer lineRen;
    Vector3 origPos;
    GameObject player;
    public float speed;

    public bool activate;

    PlayerController playerController;

    AudioSource laserAudio;
    bool laserStart;
    bool changePitch;

    float pitchStart = 0.3f;
    float pitchLevel;

    public Light gunLight;
    void Start()
    {
        lineRen = GetComponent<LineRenderer>();
        laserAudio = GetComponent<AudioSource>();
        origPos = Vector3.zero;
        pitchLevel = pitchStart;
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerController = player.GetComponent<PlayerController>();
            playerController.laserScript = this;
        }
        gunLight = GetComponentInChildren<Light>();
    }

	// Update is called once per frame
	void Update () {
        if (activate)
        {
            //lineRen.SetPosition(1, lineRen.GetPosition(1) + Vector3.forward * speed * Time.deltaTime);
            changePitch = true;
            lineRen.SetPosition(1, Vector3.Lerp(lineRen.GetPosition(1), lineRen.GetPosition(0) + Vector3.forward * 50, 3 * speed * Time.deltaTime));
        }
        else
        {
            changePitch = false;
            laserAudio.Stop();
            lineRen.SetPosition(0, Vector3.Lerp(lineRen.GetPosition(0), lineRen.GetPosition(1), 10 * speed * Time.deltaTime));
        }
        
        if (changePitch)
        {
            pitchLevel = Mathf.Lerp(pitchLevel, 1, .5f * Time.deltaTime);
            laserAudio.pitch = pitchLevel;
        }
        else
        {
            pitchLevel = pitchStart;
            laserAudio.pitch = pitchLevel;
        }
	}

    public void Reset()
    {
        lineRen.SetPosition(0, origPos);
        lineRen.SetPosition(1, origPos);
    }

    public void PlayAudio()
    {
        laserAudio.Play();
    }
}

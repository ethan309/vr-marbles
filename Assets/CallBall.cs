﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class CallBall : MonoBehaviour
{
    public SteamVR_Action_Boolean gripAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("default", "GrabGrip");
    private Interactable interactable;
    private GameObject shooter;

    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<Interactable>();
        shooter = GameObject.FindGameObjectsWithTag("Ball")[0];
    }

    // Update is called once per frame
    void Update()
    {
        bool gripPressed = gripAction.state;
        if(gripPressed)
        {
            shooter.transform.SetPositionAndRotation(transform.position, transform.rotation);
        }
    }
}
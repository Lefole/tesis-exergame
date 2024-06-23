using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JammoController : MonoBehaviour
{


    private PlayerControllerMovement playerControllerMovement;
    private Animator jammo_animator;

    void Start()
    {
        playerControllerMovement = GameObject.
           FindGameObjectWithTag("Player Movement Controller").GetComponent<PlayerControllerMovement>();
        jammo_animator = GameObject.FindGameObjectWithTag("AnimController").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!playerControllerMovement.exerciseExecutionAngleCorrect || !playerControllerMovement.exerciseExecutionMagnitudeCorrect)
        {
            jammo_animator.SetTrigger("badPerformanceDone");
        }
        else
        {
            jammo_animator.SetTrigger("correctPerformanceDone");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Animator animator;
    bool closed = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        closed = true;
    }

    public void PlayAnim()
    {
        if (closed)
        {
            animator.Play("Door_Open");
            closed = false;
        }
        else
        {
            animator.Play("Door_Close");
            closed = true;
        }
    }

    public void PlaySound()
    {
        //play sound
    }

}
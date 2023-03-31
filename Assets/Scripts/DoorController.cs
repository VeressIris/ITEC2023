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
            Debug.Log("OPEN DOOR");
            animator.Play("Door_Open");
            closed = false;
        }
        else
        {
            Debug.Log("CLOSE DOOR");
            animator.Play("Door_Close");
            closed = true;
        }
    }

    public void PlaySound()
    {
        //play sound
    }
    
}

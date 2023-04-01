using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Animator animator;
    bool closed = true;

    private AudioSource audioSource;
    [SerializeField] private AudioClip closeDoorSFX;
    [SerializeField] private AudioClip openDoorSFX;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        
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
        audioSource.clip = closed ? closeDoorSFX : openDoorSFX;
        audioSource.Play();
    }

}
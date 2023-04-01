using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingLight : MonoBehaviour
{
    public Animator animator;

    public float triggerTime; //time after which the flashing light is triggered
    private float duration; //lights off duration
    
    public bool triggered = false;

    [SerializeField] private GameManager gameManager;

    void Start()
    {
        animator = GetComponent<Animator>();

        triggerTime = SetTime(5f, 60f);
    }

    void Update()
    {
        if (triggered)
        {
            animator.Play("Flash");
        }
    }

    float SetTime(float minTime, float maxTime)
    {
        return Random.Range(minTime, maxTime);
    }

    bool StoppedPlaying()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Flash") &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            return false;
        }

        return true;
    }
}

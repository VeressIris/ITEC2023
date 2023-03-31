using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingLight : MonoBehaviour
{
    private Animator animator;

    public float triggerTime; //time after which the flashing light is triggered
    public float duration; //lights off duration
    
    bool canTrigger = true; 

    void Start()
    {
        animator = GetComponent<Animator>();

        triggerTime = SetTime(5f, 60f);
    }

    void Update()
    {
        if (canTrigger)
        {
            StartCoroutine(Trigger());
        }
    }

    float SetTime(float minTime, float maxTime)
    {
        return Random.Range(minTime, maxTime);
    }

    IEnumerator Trigger()
    {
        yield return new WaitForSeconds(triggerTime);
        
        animator.Play("Flash");
        
        if (StoppedPlaying() && canTrigger)
        {
            duration = SetTime(20f, 100f);
            
            canTrigger = false;
            
            StartCoroutine(LightsOff());
        }
    }

    IEnumerator LightsOff()
    {
        yield return new WaitForSeconds(duration);
        
        animator.Play("On");

        canTrigger = true;
        triggerTime = SetTime(5f, 60f);
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

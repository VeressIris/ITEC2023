using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public int senseIndex = 0;
    private int maxSenses = 3;

    [SerializeField] private AudioMixer mixer;
    [SerializeField] private float targetLowPassVal = 855f;
    private const float MAXLOWPASSVAL = 22000f;
    private float lowPassVal = MAXLOWPASSVAL;

    [SerializeField] FlashingLight flashlightScript;

    [HideInInspector] public bool enableDisorientation = false;

    public float effectDuration;
    [SerializeField] private float effectCooldown = 15f;

    private bool firstCall = true;
    public bool gameOver = false;

    void Start()
    {
        flashlightScript.enabled = false;

        StartCoroutine(Game());
    }
    IEnumerator Game()
    {   
        while(!gameOver)
        {
            senseIndex = Random.Range(0, maxSenses);

            if (firstCall)
            {
                yield return new WaitForSeconds(5f);
                firstCall = false;
            }
            else
            {
                yield return new WaitForSeconds(effectCooldown);
            }

            //Lose senses
            effectDuration = SetDuration();
            if (senseIndex == 0) //lose sight
            {
                FlashlightFlicker();
            }
            else if (senseIndex == 1) //lose hearing
            {
                AudioMuffle();
            }
            else if (senseIndex == 2) //disorient
            {
                enableDisorientation = true;
            }

            yield return new WaitForSeconds(effectDuration);

            RegainSenses(senseIndex);
        }
    }

    void FlashlightFlicker()
    {
        flashlightScript.enabled = true;
        flashlightScript.triggered = true;
    }

    void AudioMuffle()
    {
        if (lowPassVal > targetLowPassVal)
        {
            lowPassVal -= 85f;
            mixer.SetFloat("Lowpass", lowPassVal);
        }
    }

    void RegainSenses(int effect)
    {
        if (effect == 0)
        {
            flashlightScript.animator.Play("TurnOn");
            flashlightScript.triggered = false;
            flashlightScript.enabled = false;
        }
        else if (effect == 1)
        {
            mixer.SetFloat("Lowpass", MAXLOWPASSVAL);
            lowPassVal = MAXLOWPASSVAL;
        }
        else if (effect == 2)
        {
            enableDisorientation = false;
        }
    }

    float SetDuration()
    {
        return Random.Range(20f, 50f);
    }
}

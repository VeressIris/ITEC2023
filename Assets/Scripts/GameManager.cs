using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int senseIndex = 0;
    private int maxSenses = 3;

    [SerializeField] private AudioMixer mixer;
    [SerializeField] private float targetLowPassVal = 855f;
    private const float MAXLOWPASSVAL = 22000f;
    private float lowPassVal = MAXLOWPASSVAL;
    
    private AudioSource audioSource;
    [SerializeField] private AudioClip shhSFX;
    [SerializeField] private AudioClip whisperSFX;

    [SerializeField] FlashingLight flashlightScript;

    [HideInInspector] public bool enableDisorientation = false;

    private float effectDuration;
    public float effectCooldown;

    private bool firstCall = true;
    public bool gameOver = false;

    bool ok = false;

    [SerializeField] private GameObject black;
    private Animator fade;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        flashlightScript.enabled = false;

        effectCooldown = Random.Range(20f, 35f);

        fade = black.GetComponent<Animator>();
        fade.Play("FadeIn");

        StartCoroutine(Game());
    }

    void Update()
    {
        if (gameOver && !ok)
        {
            ok = true;
            EndGame();
        }
    }

    IEnumerator Game()
    {   
        while(!gameOver)
        {
            senseIndex = Random.Range(0, maxSenses);
            effectCooldown = Random.Range(20f, 35f);

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
                audioSource.PlayOneShot(shhSFX);
                audioSource.PlayOneShot(whisperSFX);
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
            //flashlightScript.enabled = false;
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

    void EndGame()
    {
        Debug.Log("stopping game");
        StopAllCoroutines();
        RegainSenses(senseIndex);

        StartCoroutine(LoadMenu());
    }

    IEnumerator LoadMenu()
    {
        yield return new WaitForSeconds(4.2f);
        
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(0);
    }
}

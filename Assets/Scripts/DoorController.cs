using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    private int nextScene;

    [SerializeField] private Animator animator;
    [SerializeField] private AnimationClip fade;
    private float animDuration;

    void Start()
    {
        nextScene = SceneManager.GetActiveScene().buildIndex + 1;

        animDuration = fade.length;
    }

    public IEnumerator LoadNextLevel()
    {
        animator.Play("FadeToBlack");
        yield return new WaitForSeconds(animDuration);

        SceneManager.LoadScene(nextScene);
    }

    public void PlaySound()
    {
        //play sound
    }
    
}

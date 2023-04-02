using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject black;
    private Animator fade;

    private void Awake()
    {
        fade = black.GetComponent<Animator>();
    }

    private void Start()
    {
        fade.Play("FadeIn");
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}

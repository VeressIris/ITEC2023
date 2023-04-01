using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    [SerializeField] private float moveSpeed = 10f;

    private AudioSource audio;
    [SerializeField] private AudioClip footstepSound;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        audio = GetComponent<AudioSource>();
        audio.clip = footstepSound;
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        characterController.Move(move * moveSpeed * Time.deltaTime);

        //Footsteps
        if(!audio.isPlaying && characterController.velocity.magnitude > 2f)
        {
            audio.volume = Random.Range(0.55f, 1.2f);
            audio.Play();
        }
    }
}

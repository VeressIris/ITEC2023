using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float range = 5f;
    [SerializeField] private DoorController doorController;

    private AudioSource audioSource;
    [SerializeField] private AudioClip pagePickup;

    [SerializeField] private GameObject pageUI;

    public bool powerupPickedUp = false;
    [SerializeField] private GameManager gameManager;

    [SerializeField] private GameObject arrows;

    void Start()
    {
        audioSource = GetComponentInParent<AudioSource>();

        arrows.SetActive(false);
    }

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward * range));
        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            if (hit.collider.tag == "Door" && Input.GetMouseButtonDown(0))
            {
                doorController.PlayAnim();
                doorController.PlaySound();
            }
            else if (hit.collider.tag == "Page" && Input.GetMouseButtonDown(0))
            {
                gameManager.effectCooldown = 0f;
                
                audioSource.PlayOneShot(pagePickup);
                
                pageUI.SetActive(true);
                arrows.SetActive(true);

                Destroy(hit.collider.gameObject);
            }
        }

        if (pageUI.activeSelf)
        {
            if (Input.GetMouseButtonDown(1))
            {
                audioSource.PlayOneShot(pagePickup);
                pageUI.SetActive(false);
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float range = 5f;
    [SerializeField] private DoorController doorController;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward * range));
        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            if (hit.collider.tag == "Door" && Input.GetMouseButtonDown(0))
            {
                StartCoroutine(doorController.LoadNextLevel());
            }
            else if (hit.collider.tag == "PowerUp")
            {
                Debug.Log("POWERUP");
            }
        }
    }
}

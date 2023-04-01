using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Scalpel : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float cooldown = 10f;
    [SerializeField] private float pause = 2f;
    [SerializeField] private float offset = 5f;

    [SerializeField] Transform[] spawnPoints;
    public Vector3 spawnPoint;
    private Vector3 initialPos;

    [SerializeField] private GameManager gameManager;

    void Start()
    {
        StartCoroutine(Animate());
    }

    void PickPoint()
    {
        int index = Random.Range(0, spawnPoints.Length);
        spawnPoint = spawnPoints[index].position;
    }

    IEnumerator Animate()
    {
        while(!gameManager.gameOver)
        {
            PickPoint();

            SetPosition();
            initialPos = transform.position;
            
            spawnPoint = new Vector3(spawnPoint.x, spawnPoint.y + offset, spawnPoint.z);

            StartCoroutine(MoveToTarget(spawnPoint));
            yield return new WaitForSeconds(pause);

            StartCoroutine(MoveToTarget(initialPos));
            yield return new WaitForSeconds(cooldown);
        }
    }

    IEnumerator MoveToTarget(Vector3 target)
    {
        float time = 0f;

        while (time < 1)
        {
            time += Time.deltaTime;
            
            transform.position = Vector3.Lerp(transform.position, target, speed * Time.deltaTime);
            yield return null; //wait a frame
        }
    }

    //move object to chosen spawn point to line up the x and z values so it doesn't move diagonally
    void SetPosition()
    {
        transform.position = new Vector3(spawnPoint.x, spawnPoint.y + 11, spawnPoint.z);
    }
}

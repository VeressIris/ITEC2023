using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Scalpel : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float cooldown = 10f;

    [SerializeField] Transform[] spawnPoints;
    public Vector3 spawnPoint;
    private Vector3 initialPos;
    bool called = false;

    void Start()
    {
        PickPoint();
        initialPos = transform.position;
    }

    void Update()
    {
        if (!called)
        {
            StartCoroutine(Animate());
        }
    }

    void PickPoint()
    {
        int index = Random.Range(0, spawnPoints.Length);
        spawnPoint = spawnPoints[index].position;
    }

    IEnumerator Animate()
    {
        called = true;

        StartCoroutine(MoveToTarget(spawnPoint));

        yield return new WaitForSeconds(2f);

        StartCoroutine(MoveToTarget(initialPos));

        yield return new WaitForSeconds(cooldown);
        called = false;

        PickPoint();
    }

    IEnumerator MoveToTarget(Vector3 target)
    {
        while (transform.position != target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            yield return null; //wait a frame
        }
    }
}

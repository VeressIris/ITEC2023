using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
//using static UnityEngine.GraphicsBuffer;

public class Scalpel : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float cooldown = 10f;
    [SerializeField] private float pause = 2f;

    [SerializeField] GameObject[] spawnPoints;
    public Vector3 spawnPoint;
    private Vector3 initialPos;

    [SerializeField] private GameManager gameManager;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
        StartCoroutine(Animate());
    }

    void PickPoint()
    {
        int index = Random.Range(0, spawnPoints.Length);
        spawnPoint = spawnPoints[index].transform.position;
    }

    IEnumerator Animate()
    {
        while(!gameManager.gameOver)
        {
            PickPoint();

            //move object to chosen spawn point to line up the x and z values so it doesn't move diagonally
            transform.position = spawnPoint;
            initialPos = transform.position;

            StartCoroutine(MoveToTarget(spawnPoint));
            audioSource.Play();
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

    bool isPair(GameObject obj)
    {
        if (obj.name.Contains("Begin") || obj.name.Contains("End"))
        {
            return true;
        }
        return false;
    }
}

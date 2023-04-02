using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scalpel : MonoBehaviour
{
    private float speed = 5f;
    [SerializeField] private float cooldown = 10f;
    private float pause = 2f;

    [SerializeField] GameObject[] spawnPoints;
    public Vector3 spawnPoint;
    private Vector3 initialPos;

    [SerializeField] private GameManager gameManager;

    private AudioSource audioSource;

    [SerializeField] private Transform player;

    [SerializeField] private GameObject black;
    private Animator fade;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
        fade = black.GetComponent<Animator>();

        StartCoroutine(Animate());
    }

    IEnumerator Animate()
    {
        while(!gameManager.gameOver)
        {
            PickPoint();

            //move object to chosen spawn point to line up the x and z values so it doesn't move diagonally
            transform.position = spawnPoint;
            initialPos = transform.position;

            //offset spawnpoint Y position so the knife moves downwards
            spawnPoint = new Vector3(spawnPoint.x, spawnPoint.y - 7.4f, spawnPoint.z);

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

    int ClosestPoint()
    {
        float minDistance = 0;
        int ans = 0;

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (minDistance < distance)
            {
                minDistance = distance;
                ans = i;
            }
        }
        
        return ans;
    }

    void PickPoint()
    {
        int index = ClosestPoint();
        if (isPair(spawnPoints[index]))
        {
            Vector3 point1 = spawnPoints[index].transform.position;
            Vector3 point2 = spawnPoints[index + 1].transform.position;

            if (point1.x == point2.x)
            {
                spawnPoint = new Vector3(point1.x, point1.y, Random.Range(point1.z, point2.z));
            }
            else if (point1.z == point2.z)
            {
                spawnPoint = new Vector3(Random.Range(point1.x, point2.x), point1.y, point1.z);
            }
        }
        else
        {
            spawnPoint = spawnPoints[index].transform.position;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("DEAD");
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        //play death sound
        fade.Play("FadeToBlack");
        yield return null;
        SceneManager.LoadScene(1);
    }
}

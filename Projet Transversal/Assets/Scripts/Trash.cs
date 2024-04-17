using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    public KeyCode P1;
    public KeyCode P2;
    public bool isInPlayerRange;
    public LayerMask whatIsPlayer;
    public GameObject trashPrefab; 
    public int initialNumberOfTrash = 26; // Initial number of trash objects to spawn
    public float minX = -8f;
    public float maxX = 7.3f;
    public float minY = -4.42f;
    public float maxY = 2f;
    public float minDistance = 1.3f; // Minimum distance between trash objects
    public float spawnInterval = 1f; // Interval between each trash spawn after the initial delay
    private bool spawningEnabled = false;

    // Start is called before the first frame update
    void Start()
    {
        SpawnInitialTrash();
        Invoke("EnableSpawning", 20f); // Enable spawning after 10 seconds of gameplay
    }

    // Update is called once per frame
    void Update()
    {
        isInPlayerRange = Physics2D.OverlapCircle(transform.position, 0.5f, whatIsPlayer);

        if (Input.GetKeyDown(P1) && isInPlayerRange)
        {
            StartCoroutine(waitt());
        }
        if (Input.GetKeyDown(P2) && isInPlayerRange)
        {
            StartCoroutine(waitt());
        }
    }
    IEnumerator waitt()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(this.gameObject);
    }

    // Function to spawn initial trash objects
    void SpawnInitialTrash()
    {
        for (int i = 0; i < initialNumberOfTrash; i++)
        {
            SpawnTrash();
        }
    }

    // Function to spawn trash objects
    void SpawnTrash()
    {
        Vector3 randomPosition = GetRandomPosition();
        GameObject trashClone = Instantiate(trashPrefab, randomPosition, Quaternion.identity);
    }

    // Enable spawning after initial delay
    void EnableSpawning()
    {
        spawningEnabled = true;
        StartCoroutine(SpawnTrashRepeatedly());
    }

    // Coroutine to spawn trash objects repeatedly
    IEnumerator SpawnTrashRepeatedly()
    {
        while (spawningEnabled)
        {
            SpawnTrash();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // Function to get a random position for spawning trash
    Vector3 GetRandomPosition()
    {
        Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0f);

        // Check if the random position is too close to existing trash objects
        foreach (GameObject trashObject in GameObject.FindGameObjectsWithTag("Trash"))
        {
            if (Vector3.Distance(randomPosition, trashObject.transform.position) < minDistance)
            {
                return GetRandomPosition(); // Retry with a new random position
            }
        }

        return randomPosition;
    }
}

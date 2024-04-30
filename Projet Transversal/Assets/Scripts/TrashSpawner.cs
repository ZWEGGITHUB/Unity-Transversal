using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    public GameObject trashPrefab; 
    public int initialNumberOfTrash = 24; // Initial number of trash objects to spawn
    public float minX = -8.35f;
    public float maxX = 7.5f;
    public float minY = -4.25f;
    public float maxY = 2f;
    public float minDistance = 1.25f; // Minimum distance between trash objects
    public float spawnInterval = 1.5f; // Interval between each trash spawn after the initial delay
    public float spawnAfter = 16f;
    private bool spawningEnabled = false;

    // Start is called before the first frame update
    void Start()
    {
        SpawnInitialTrash();
        Invoke("EnableSpawning", spawnAfter); // Enable spawning after 15 seconds of gameplay
    }

    // Update is called once per frame
    void Update()
    {
        
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

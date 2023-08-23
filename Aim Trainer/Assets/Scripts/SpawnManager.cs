using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [Space]
    [SerializeField] GameObject botPrefab;
    [SerializeField] GameObject[] spawnPoints;
    [SerializeField] float timeBetweenSpawns;
    [SerializeField] int botCount;

    bool spawning = false;
    int botsToSpawn;
    float spawnTime;
    List<GameObject> botCollection = new List<GameObject>();

    // Update is called once per frame
    void Update()
    {
        // If spawning, do the spawn sequence
        if (spawning) {
            // This check is for possible future use for if I were to limit the number of targets per round
            if (botsToSpawn > 0) {
                if (spawnTime > 0) {
                    spawnTime -= Time.deltaTime;
                } else {
                    // Spawn the target at a specified range
                    GameObject randomSpawn = playerData.constantSpawnRange ? spawnPoints[Random.Range(2 * playerData.spawnRange, (2 * playerData.spawnRange) + 2)]
                        : spawnPoints[Random.Range(0, (2 * playerData.spawnRange) + 2)];
                    botCollection.Add(Instantiate(botPrefab, randomSpawn.transform.position, randomSpawn.transform.rotation));
                    botsToSpawn -= 1;
                    spawnTime = timeBetweenSpawns;
                }
            }
        }
    }

    // Start the spawning sequence
    public void StartSpawning()
    {
        spawning = true;
        botsToSpawn = botCount;
    }

    // Stop the spawning sequence
    public void StopSpawning()
    {
        // Clear all bots currently on screen
        spawning = false;
        foreach (GameObject bot in botCollection) {
            Destroy(bot);
        }
    }
}

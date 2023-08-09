using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] PlayerData pd;
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
        if (spawning) {
            if (botsToSpawn > 0) {
                if (spawnTime > 0) {
                    spawnTime -= Time.deltaTime;
                } else {
                    GameObject randomSpawn = pd.constantSpawnRange ? spawnPoints[Random.Range(2 * pd.spawnRange, (2 * pd.spawnRange) + 2)]
                        : spawnPoints[Random.Range(0, (2 * pd.spawnRange) + 2)];
                    botCollection.Add(Instantiate(botPrefab, randomSpawn.transform.position, randomSpawn.transform.rotation));
                    botsToSpawn -= 1;
                    spawnTime = timeBetweenSpawns;
                }
            }
        }
    }

    public void StartSpawning()
    {
        spawning = true;
        botsToSpawn = botCount;
    }

    public void StopSpawning()
    {
        // Clear all bots currently on screen
        spawning = false;
        foreach (GameObject bot in botCollection) {
            Destroy(bot);
        }
    }
}

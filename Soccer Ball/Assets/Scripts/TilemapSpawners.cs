using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapSpawners : MonoBehaviour
{
    public int maxEnemyOnTheField;
    public int TotalEnemysToSpawn;
    private int totalSpawned;


    private int enemyCount;
    public GameObject Enemy;
    public GameEvent gameEvent;
    [SerializeField]
    private Transform[] spawnLocations;

    void Start()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemies").Length;
        gameEvent.onEnemyToKillCount?.Invoke(TotalEnemysToSpawn);
        Debug.Log(enemyCount);
    }

    // Update is called once per frame
    void Update()
    {
        SpawnEnemies();
    }
    private void SpawnEnemies()
    {
        if(enemyCount < maxEnemyOnTheField)
        {
            if (totalSpawned < TotalEnemysToSpawn)
            {
                totalSpawned++;
                int range = Random.Range(0, 3);
                Vector2 spawnLocation = spawnLocations[range].position;
                Instantiate(Enemy, spawnLocation, Quaternion.identity);

            }
        }
    }
    private void OnDisable()
    {
        
    }
}

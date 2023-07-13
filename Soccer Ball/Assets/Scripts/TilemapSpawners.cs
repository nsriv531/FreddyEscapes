using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapSpawners : MonoBehaviour
{
    public int maxEnemyTOSpawn;
    public int totalEnemySpawned;
    public int totalSpawned;


    private int enemyCount;
    public GameEvent gameEvent;

    void Start()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemies").Length;
        gameEvent.onEnemyCount?.Invoke(enemyCount);
        Debug.Log(enemyCount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void SpawnEnemies()
    {

    }
    private void OnDisable()
    {
        
    }
}

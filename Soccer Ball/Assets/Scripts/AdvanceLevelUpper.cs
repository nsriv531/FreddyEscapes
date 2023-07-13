using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class AdvanceLevelUpper : MonoBehaviour
{

    public GameEvent gameEvent;
    
    public int maxEnemies;
    public int Enemieskilled;
    public Light2D light2D;

    private void Start()
    {

    }

    void Update()
    {
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.buildIndex + 1);

        }
    }
    public void UpdateKillCOunt(int enemy)
    {
        Enemieskilled += enemy;
        if (Enemieskilled >= maxEnemies)
        {
            GetComponent<BoxCollider2D>().enabled = true;
            light2D.enabled = true;
        }
    
    }
    public void GetEnemys(int enemy)
    {
        maxEnemies = enemy;
    }
    private void OnEnable()
    {
        gameEvent.onEnemyToKillCount += GetEnemys;
        gameEvent.onEnemyKillCount += UpdateKillCOunt;
    }
    private void OnDisable()
    {
        gameEvent.onEnemyToKillCount -= GetEnemys;
        gameEvent.onEnemyKillCount -= UpdateKillCOunt;
    }
}


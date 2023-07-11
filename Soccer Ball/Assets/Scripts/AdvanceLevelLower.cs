using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdvanceLevelLower : MonoBehaviour
{

    public Transform placement;
    public GameObject enemies;
    public float spawnTimer = 0;
    public float spawnRate = 8;

    private void Start()
    {
        
    }

    void Update()
    {
        /*Instantiate(enemies, new Vector3(40, -11, 0), new Quaternion(0, 0, 0, 0));
*/
        /*if (spawnRate > spawnTimer)
        {
            spawnTimer += Time.deltaTime;
        }
        else
        {
            spawnTimer = 0;
            Instantiate(enemies, placement.position, Quaternion.identity);
        }*/
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        /*Debug.Log(SceneManager.GetActiveScene().buildIndex);*/
        /*Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex + 1);*/
    }
}


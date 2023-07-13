using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdvanceLevelUpper : MonoBehaviour
{

    

    private void Start()
    {

    }

    void Update()
    {
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex + 1);
    }
}


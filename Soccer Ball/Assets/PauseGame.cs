using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{

    public GameObject pausemenue;
    private bool isPause;
    void Start()
    {
        isPause = false;   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPause)
        {
            isPause = true;
            PauseTheGame();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && isPause)
        {
            UnpauseGame();
            isPause= false;
        }
    }
    public void PauseTheGame()
    {
        pausemenue.SetActive(true);
        Time.timeScale = 0f;
    }
    public void UnpauseGame()
    {
        pausemenue.SetActive(false);
        Time.timeScale = 1f;


    }
    public void MainMenue()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
}

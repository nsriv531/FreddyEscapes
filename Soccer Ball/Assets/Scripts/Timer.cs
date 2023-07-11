using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float time;
    public GameEvent gameEvents;
    bool isTimerRunning;

    public void Start()
    {
        StartCoroutine(GameTimer());
    }

    public IEnumerator GameTimer()
    {
        isTimerRunning= true;
        while(isTimerRunning) 
        {
            time += Time.deltaTime;
            gameEvents.onTimerChange?.Invoke(time);
            yield return null;
        }
    }
    
    
    public void StopTimer()
    {

    }
}

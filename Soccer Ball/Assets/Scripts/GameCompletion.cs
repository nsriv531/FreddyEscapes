using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCompletion : MonoBehaviour
{
    public GameEvent gameEvent;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           
                gameEvent.onGameOver?.Invoke();

            
        }
    }
}

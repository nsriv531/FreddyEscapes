using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class AttackComponent : MonoBehaviour
{

    public IDamagable enemyHitData;

    public AudioSource audioPlayer;
    public event Action OnattackFound;
    public GameEvent gameEvent;
    private void Start()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetEnemy(collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        RemoveEnmyRefrance();
    }

    /// <summary>
    /// stores the refrance of the enemy, when they enter your hit box 
    /// </summary>
    /// <param name="collision"></param>
    public void GetEnemy(Collider2D collision)
    {
        IDamagable damagable = null;
        if (gameObject.transform.parent.CompareTag("Player"))
        {
             damagable = collision.gameObject.GetComponent<IDamagable>();

        }
        if (collision.CompareTag("Player"))
        {
            damagable = collision.gameObject.GetComponent<IDamagable>();
        }

        if (damagable != null)
        {
            enemyHitData = damagable;
        }
        
    }

    /// <summary>
    /// removes the enemy refrance if they leave the hit detection
    /// </summary>
    public void RemoveEnmyRefrance()
    {
        enemyHitData = null;

    }

    public void AattackTheEnemy(float damage)
    {
        if(enemyHitData != null)
        {
            if (transform.parent.CompareTag("Player"))
            {
                gameEvent.onEnemyHit?.Invoke();
            }
            enemyHitData.TakeDamage(damage);
            enemyHitData = null;

     
        }
    }

    


}

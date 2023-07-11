using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dummyscript : MonoBehaviour
{
    public int health;
 

    

   
    public void PlayDeath()
    {
        Debug.Log("im dead");
    }
    public void Health()
    {
        health--;

        if(health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {}

    public void OnCollisionEnter2D(Collision2D collision)
    {}

    public void OnTriggerStay2D(Collider2D collision)
    {}

    public void OnCollisionStay2D(Collision2D collision)
    {}

    public void OnTriggerExit2D(Collider2D collision)
    {}

    public void OnCollisionExit2D(Collision2D collision)
    {}
}

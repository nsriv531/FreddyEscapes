using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class HealthItem1 : MonoBehaviour
{
    public GameObject ball;
    public GameObject ui;

    void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Player");
        ui = GameObject.FindGameObjectWithTag("GameController");
    }

    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("hit ball");
            Destroy(gameObject);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashChargeVisual : MonoBehaviour
{
    public GameObject DirectionArrow;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public GameEvent playerEvents;
    private Transform Player;
    void Start()
    {

        animator = GetComponent<Animator>();
        spriteRenderer= GetComponent<SpriteRenderer>();
        playerEvents.onChargeValue += UpdateCharge;
        playerEvents.onChargeFirection += DashDirection;
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            spriteRenderer.enabled = false;
            DirectionArrow.SetActive(false);

        }
    }
    private void UpdateCharge(float chargeValue)
    {
        spriteRenderer.enabled = true;
        animator.SetFloat("charge", chargeValue);
        if(chargeValue> 0.9f)
        {
            spriteRenderer.color= Color.red;
        }
        else
        {
            spriteRenderer.color= Color.white;
        }
       transform.position= new Vector2(Player.position.x, Player.position.y-2.5f);
    }
    public void DashDirection(float angle)
    {
        DirectionArrow.SetActive(true);
        DirectionArrow.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    }

    private void OnDisable()
    {
        playerEvents.onChargeValue -= UpdateCharge;

    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KillVisual : MonoBehaviour
{
    public GameEvent gameEvent;
    public TextMeshProUGUI maxenemy;
    public TextMeshProUGUI totalEnemysKilled;
    private int killCOunt;
    private void Start()
    {

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
    public void UpdateKillCOunt(int enemy)
    {
        killCOunt += enemy;
        totalEnemysKilled.text = killCOunt.ToString();
    }
    public void GetEnemys(int enemy)
    {
        maxenemy.text = enemy.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboCounter : MonoBehaviour
{
    private int combocount;
    private int highestComboCount;
    public GameEvent gameEvent;
    private void Start()
    {
        combocount= 0;
        gameEvent.onEnemyHit += IncreaseComboCount;
        gameEvent.OnPlayerDamaged+= ResetComboCOunt;
    }
    public void IncreaseComboCount()
    {
        combocount++;
        gameEvent.onComboMeterIncrease?.Invoke(combocount);
        if(combocount > highestComboCount)
        {
            highestComboCount = combocount;
        }
    }
    public void ResetComboCOunt()
    {
        if(combocount > 0)
        {
            gameEvent.OnComboMeterReset?.Invoke();
            combocount= 0;

        }
    }
    public int ReturnCOmboCount()
    {
        return combocount;
    }
    public int ReturnHighestComboCount()
    {
        return combocount;
    }
}

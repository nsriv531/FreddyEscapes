using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEvent : ScriptableObject
{
    public Action<float> onChargeValue;
    public Action<float> onChargeFirection;
    public Action<float> OnChargCoolDown;
    public Action OnStageClear;
    public Action OnGameStart;
    public Action<float> onTimerChange;
    public Action<int> onComboMeterIncrease;
    public Action  OnComboMeterReset;
    public Action OnPlayerDamaged;
    public Action onEnemyHit;
    public Action onGameOver;
}

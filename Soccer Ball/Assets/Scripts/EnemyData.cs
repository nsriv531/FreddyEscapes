using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class EnemyData : ScriptableObject
{
    public int MaxPursuers;
    public int pursures;

    public void CheckiFCanPersue(EnemyAiVer2 ai)
    {
        if(pursures <MaxPursuers && !ai.GetPursue())
        {
            pursures++;
            ai.EnablePursue();
        }
    }
    public void EnemyDead()
    {
      
        
            pursures--;

        
        
    }
}

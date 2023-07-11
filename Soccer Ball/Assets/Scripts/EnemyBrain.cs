using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    public EnemyData enemyData; 
    // Start is called before the first frame update
    void Start()
    {
        enemyData.pursures = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

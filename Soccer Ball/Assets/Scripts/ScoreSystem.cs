using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{

    private int scoreCount = 200;
    private int HighestHitStreak = 20;
    private int timesHit = 10;
    private float timecompleted = 15f;
    private string Grade;

    public TextMeshProUGUI time;
    public TextMeshProUGUI score;
    public TextMeshProUGUI hitcount;
    public TextMeshProUGUI damagecount;

    private float calaulationTime = 1;
    private float elapsetime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(calculate());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GetHitCount(int streak)
    {
        timesHit = streak;
    }
    private void DamagedAmount()
    {
        timesHit++;
    }
    private void GetTime(float time)
    {
        timecompleted = time;
    }

    private IEnumerator calculate()
    {
        float completion = elapsetime/calaulationTime;
        while(completion < 1)
        {
            Debug.Log("hi");
            elapsetime += Time.unscaledDeltaTime;
            completion = elapsetime / calaulationTime;
            time.text =Mathf.Lerp(0, timecompleted, completion).ToString("0");
            yield return null;


        }
        elapsetime = 0;
         completion = elapsetime / calaulationTime;

        while (completion < 1)
        {
            elapsetime += Time.unscaledDeltaTime;
            completion = elapsetime / calaulationTime;
            damagecount.text = Mathf.Lerp(0f, (int)timesHit, completion).ToString("0");
            yield return null;


        }
        elapsetime = 0;
        completion = elapsetime / calaulationTime;

        while (completion < 1)
        {
            elapsetime += Time.unscaledDeltaTime;
            completion = elapsetime / calaulationTime;
            hitcount.text = Mathf.Lerp(0f, (int)HighestHitStreak, completion).ToString("0");
            yield return null;

        }
        elapsetime = 0;
        completion = elapsetime / calaulationTime;

        while (completion < 1)
        {
            elapsetime += Time.unscaledDeltaTime;
            completion = elapsetime / calaulationTime;
            yield return null;

            score.text = Mathf.Lerp(0f, (float)scoreCount, completion).ToString("0");
        }




    }

    private int ComboScoreWeighting()
    {
        return HighestHitStreak;
    }
    private int CHitCountWeight()
    {
        return timesHit;
    }
    private float TimeWeight()
    {
        return HighestHitStreak;
    }
    



}

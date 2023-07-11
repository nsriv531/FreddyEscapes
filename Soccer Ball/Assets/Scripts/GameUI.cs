using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{

    public Image CoolDownImage;
    public GameEvent playerEvents;
    public TextMeshProUGUI timerui;
    public GameObject combocounter;
    public TextMeshProUGUI comboNumberText;
    public TextMeshProUGUI comboText;
    public GameObject gameover;



    // Start is called before the first frame update
    void Start()
    {
        playerEvents.OnChargCoolDown += Cooldown;
        playerEvents.onTimerChange += DisplayTime;
        playerEvents.onComboMeterIncrease += DisplayCOmbo;
        playerEvents.OnComboMeterReset += ComboResetDisplay;
        playerEvents.onGameOver += DisplayGameOver;
    }

    // Update is called once per frame
    void Update()
    {
     
    }
    public void Cooldown(float cooldown)
    {
        CoolDownImage.fillAmount= cooldown;
    }
    public void DisplayTime(float timer)
    {
        float minutes = Mathf.FloorToInt(timer / 60);
        float seconds = Mathf.FloorToInt(timer% 60);
        timerui.text = string.Format("{1:00}", minutes, seconds);
    }
    public void DisplayCOmbo(int combo)
    {
        combocounter.SetActive(true);
        comboNumberText.text = combo.ToString();
        combocounter.GetComponent<Animator>().Play("IncreasCombo");
        comboText.text = "hit";
        




    }
    public void ComboResetDisplay()
    {
        combocounter.SetActive(true);
        comboNumberText.text = "COMBO";
        comboText.text = "Break";
        combocounter.GetComponent<Animator>().Play("ComboReset");


        

    }
    public void  DisplayGameOver()
    {
        gameover.SetActive(true);
    }
    public void HideCombo()
    {
        combocounter.SetActive(false);
    }

}

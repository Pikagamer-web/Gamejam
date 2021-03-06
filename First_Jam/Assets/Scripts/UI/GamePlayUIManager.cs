﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GamePlayUIManager : MonoBehaviour
{
    [SerializeField] Canvas GamplayUICanvas, TempCanvas;
    [SerializeField] Image FieldStrengthBar, ChipCorruptionBar, RewindTimeBar;
    [SerializeField] Image ClockRewindFill;
    [SerializeField] Image TimeRewindFill;
    [SerializeField] CharacterControllerMine player;
    Vector3 playerStartPosition;
    int tempSwitch = 0;
    [SerializeField] TextMeshProUGUI textDisplay;
    public float score = 0;

    [SerializeField] RewindTarget rewindTarget;

    // Start is called before the first frame update
    void Start()
    {
        playerStartPosition = player.transform.position;
        textDisplay.text = Mathf.Abs(score).ToString();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControllerMine>();
        FieldStrengthBar.rectTransform.localScale = new Vector3(0.93f, FieldStrengthBar.rectTransform.localScale.y, FieldStrengthBar.rectTransform.localScale.z) ;
        ChipCorruptionBar.rectTransform.localScale = new Vector3(0, ChipCorruptionBar.rectTransform.localScale.y, ChipCorruptionBar.rectTransform.localScale.z);
        TempCanvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        score = ((player.transform.position.z-playerStartPosition.z)/790f)*1000;
        int displayScore = (int)Mathf.Abs(score);
       
        textDisplay.SetText(displayScore);
        float barFillVal1 = player.fieldStrength / 93f;
        barFillVal1 = Mathf.Clamp(barFillVal1, 0, 0.93f);
        float barFillVal2 = player.chipCorruption / 93f;
        barFillVal2 = Mathf.Clamp(barFillVal2, 0, 0.93f);
        Vector3 scale = FieldStrengthBar.rectTransform.localScale;
        scale.x = barFillVal1;
        FieldStrengthBar.rectTransform.localScale = scale;

        scale = ChipCorruptionBar.rectTransform.localScale;
        scale.x = barFillVal2;
        ChipCorruptionBar.rectTransform.localScale = scale;

        //Clock rewindingBAr
        if(player.currentClock!=null && player.currentClock.gameObject.activeInHierarchy)
        {
            if (tempSwitch==0) { TempCanvas.gameObject.SetActive(true); tempSwitch = 1; }
            barFillVal1 = player.currentClock.timer / player.currentClock.RewindTime;
            ClockRewindFill.fillAmount = barFillVal1;
        }
        else
        {
            if(tempSwitch == 1) { TempCanvas.gameObject.SetActive(false); tempSwitch = 0; }
        }

        //TimeRewindBar
        barFillVal2 = player.RewindTimeBar / 100f;
        TimeRewindFill.fillAmount = barFillVal2;

        //Rewinmd Target found UI

        if (rewindTarget.ShowRewindTarget)
        {
            //Shoe rewind target visuals
        }
    }
}

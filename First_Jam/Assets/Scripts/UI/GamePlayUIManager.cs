using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayUIManager : MonoBehaviour
{
    [SerializeField] Canvas GamplayUICanvas;
    [SerializeField] Image FieldStrengthBar, ChipCorruptionBar, RewindTimeBar;

    [SerializeField] CharacterControllerMine player;

    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControllerMine>();
        FieldStrengthBar.rectTransform.localScale = new Vector3(0.93f, FieldStrengthBar.rectTransform.localScale.y, FieldStrengthBar.rectTransform.localScale.z) ;
        ChipCorruptionBar.rectTransform.localScale = new Vector3(0, ChipCorruptionBar.rectTransform.localScale.y, ChipCorruptionBar.rectTransform.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {
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

       
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_StatControl : UI_Base
{
    

    enum BtnObjects
    {
        HealthMinus,
        HealthPlus,
        SpeedMinus,
        SpeedPlus,
        AttackMinus,
        AttackPlus,
        AttackSpeedMinus,
        AttackSpeedPlus,
        DefenseMinus,
        DefensePlus,
        CriticalPerMinus,
        CriticalPerPlus,
        CriticalAtkMinus,
        CriticalAtkPlus,
        EnforceBtn
    }

    enum SliderObjects
    {
        ResourceSlider,
        HealthSlider,
        SpeedSlider,
        AttackSlider,
        AttackSpeedSlider,
        DefenseSlider,
        CriticalPerSlider,
        CriticalAtkSlider
    }

    enum PointTxt
    {
        NeedResValue,
        HadPointsValue
    }

    public override void Init()
    {
        Bind<Button>(typeof(BtnObjects));
        Bind<Slider>(typeof(SliderObjects));
        Bind<TextMeshProUGUI>(typeof(PointTxt));

        

        GetButton((int)BtnObjects.HealthPlus).onClick.AddListener(delegate {IncreaseCharacterStat(gameObject.name, "Health");});
        GetButton((int)BtnObjects.SpeedPlus).onClick.AddListener(delegate { IncreaseCharacterStat(gameObject.name, "Speed"); });
        GetButton((int)BtnObjects.AttackPlus).onClick.AddListener(delegate { IncreaseCharacterStat(gameObject.name, "Attack"); });
        GetButton((int)BtnObjects.AttackSpeedPlus).onClick.AddListener(delegate { IncreaseCharacterStat(gameObject.name, "AttackSpeed"); });
        GetButton((int)BtnObjects.DefensePlus).onClick.AddListener(delegate { IncreaseCharacterStat(gameObject.name, "Defense"); });
        GetButton((int)BtnObjects.CriticalPerPlus).onClick.AddListener(delegate { IncreaseCharacterStat(gameObject.name, "CriticalPer"); });
        GetButton((int)BtnObjects.CriticalAtkPlus).onClick.AddListener(delegate { IncreaseCharacterStat(gameObject.name, "CriticalAtk"); });

        GetButton((int)BtnObjects.HealthMinus).onClick.AddListener(delegate { RemoveCharacterStat(gameObject.name, "Health");});
        GetButton((int)BtnObjects.SpeedMinus).onClick.AddListener(delegate { RemoveCharacterStat(gameObject.name, "Speed"); });
        GetButton((int)BtnObjects.AttackMinus).onClick.AddListener(delegate { RemoveCharacterStat(gameObject.name, "Attack"); });
        GetButton((int)BtnObjects.AttackSpeedMinus).onClick.AddListener(delegate { RemoveCharacterStat(gameObject.name, "AttackSpeed"); });
        GetButton((int)BtnObjects.DefenseMinus).onClick.AddListener(delegate { RemoveCharacterStat(gameObject.name, "Defense"); });
        GetButton((int)BtnObjects.CriticalPerMinus).onClick.AddListener(delegate { RemoveCharacterStat(gameObject.name, "CriticalPer"); });
        GetButton((int)BtnObjects.CriticalAtkMinus).onClick.AddListener(delegate { RemoveCharacterStat(gameObject.name, "CriticalAtk"); });

    }

    private void IncreaseCharacterStat(string characterName, string statName)
    {
        Managers.Game.characterManager.IncreaseCharacterStat(characterName, statName, 10);
        Debug.Log(Managers.Game.characterManager.GetCharacterStatValue(gameObject.name, "Health"));
        GetSlider((int)Enum.Parse(typeof(SliderObjects), statName+"Slider")).value += 0.125f;
    }

    private void RemoveCharacterStat(string characterName, string statName)
    {
         Managers.Game.characterManager.RemoveCharacterStat(characterName, statName, 10);
        GetSlider((int)Enum.Parse(typeof(SliderObjects), statName + "Slider")).value -= 0.125f;

    }
}

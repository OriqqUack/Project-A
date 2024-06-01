using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_StatControl : UI_Base
{
    string _characterName;

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
        HadPointsValue,
        HadResourceValue,
        NowResValue,
        LevelTxt
    }

    public override void Init()
    {
        _characterName = gameObject.name;

        Bind<Button>(typeof(BtnObjects));
        Bind<Slider>(typeof(SliderObjects));
        Bind<TextMeshProUGUI>(typeof(PointTxt));

        GetButton((int)BtnObjects.HealthPlus).onClick.AddListener(delegate {IncreaseCharacterStat(_characterName, "Health");});
        GetButton((int)BtnObjects.SpeedPlus).onClick.AddListener(delegate { IncreaseCharacterStat(_characterName, "Speed"); });
        GetButton((int)BtnObjects.AttackPlus).onClick.AddListener(delegate { IncreaseCharacterStat(_characterName, "Attack"); });
        GetButton((int)BtnObjects.AttackSpeedPlus).onClick.AddListener(delegate { IncreaseCharacterStat(_characterName, "AttackSpeed"); });
        GetButton((int)BtnObjects.DefensePlus).onClick.AddListener(delegate { IncreaseCharacterStat(_characterName, "Defense"); });
        GetButton((int)BtnObjects.CriticalPerPlus).onClick.AddListener(delegate { IncreaseCharacterStat(_characterName, "CriticalPer"); });
        GetButton((int)BtnObjects.CriticalAtkPlus).onClick.AddListener(delegate { IncreaseCharacterStat(_characterName, "CriticalAtk"); });

        GetButton((int)BtnObjects.HealthMinus).onClick.AddListener(delegate { RemoveCharacterStat(_characterName, "Health");});
        GetButton((int)BtnObjects.SpeedMinus).onClick.AddListener(delegate { RemoveCharacterStat(_characterName, "Speed"); });
        GetButton((int)BtnObjects.AttackMinus).onClick.AddListener(delegate { RemoveCharacterStat(_characterName, "Attack"); });
        GetButton((int)BtnObjects.AttackSpeedMinus).onClick.AddListener(delegate { RemoveCharacterStat(_characterName, "AttackSpeed"); });
        GetButton((int)BtnObjects.DefenseMinus).onClick.AddListener(delegate { RemoveCharacterStat(_characterName, "Defense"); });
        GetButton((int)BtnObjects.CriticalPerMinus).onClick.AddListener(delegate { RemoveCharacterStat(_characterName, "CriticalPer"); });
        GetButton((int)BtnObjects.CriticalAtkMinus).onClick.AddListener(delegate { RemoveCharacterStat(_characterName, "CriticalAtk"); });

        GetButton((int)BtnObjects.EnforceBtn).onClick.AddListener(delegate { IncreaseCharacterExp(_characterName, 10); });
        
    }

    private void Start()
    {
        GetSlider((int)SliderObjects.ResourceSlider).value = (Managers.Character.GetCharacterNowExp(_characterName) / (int)Managers.Character._levelUpXpTable[(int)Managers.Character.GetCharacterStatValue(_characterName, "Level")-1])*100;

        GetSlider((int)SliderObjects.HealthSlider).value = 0.125f * (Managers.Character.GetCharacterStatValue(_characterName, "Health") / 10f);
        GetSlider((int)SliderObjects.SpeedSlider).value = 0.125f * (Managers.Character.GetCharacterStatValue(_characterName, "Speed") / 10f);
        GetSlider((int)SliderObjects.AttackSlider).value = 0.125f * (Managers.Character.GetCharacterStatValue(_characterName, "Attack") / 10f);
        GetSlider((int)SliderObjects.AttackSpeedSlider).value = 0.125f * (Managers.Character.GetCharacterStatValue(_characterName, "AttackSpeed") / 10f);
        GetSlider((int)SliderObjects.DefenseSlider).value = 0.125f * (Managers.Character.GetCharacterStatValue(_characterName, "Defense") / 10f);
        GetSlider((int)SliderObjects.CriticalPerSlider).value = 0.125f * (Managers.Character.GetCharacterStatValue(_characterName, "CriticalPer") / 10f);
        GetSlider((int)SliderObjects.CriticalAtkSlider).value = 0.125f * (Managers.Character.GetCharacterStatValue(_characterName, "CriticalAtk") / 10f);

        GetText((int)PointTxt.HadPointsValue).text = Managers.Character.GetCharacterStatValue(_characterName, "StatPoint").ToString();
        GetText((int)PointTxt.NeedResValue).text = Managers.Character._levelUpXpTable[(int)Managers.Character.GetCharacterStatValue(_characterName, "Level") - 1].ToString();
        GetText((int)PointTxt.NowResValue).text = Managers.Character.GetCharacterNowExp(_characterName).ToString();
        GetText((int)PointTxt.LevelTxt).text = Managers.Character.GetCharacterStatValue(_characterName, "Level").ToString();
    }

    private void IncreaseCharacterStat(string characterName, string statName)
    {
        if (ValidValue() == false)
            return;
        if (Math.Clamp(Managers.Character.GetCharacterStatValue(_characterName, statName), 0, 80) == 80)
            return;

        Managers.Character.IncreaseCharacterStat(characterName, statName, 10);
        Managers.Character.RemoveCharacterStat(characterName, "StatPoint", 1);
        GetText((int)PointTxt.HadPointsValue).text = Managers.Character.GetCharacterStatValue(_characterName, "StatPoint").ToString();
        GetSlider((int)Enum.Parse(typeof(SliderObjects), statName + "Slider")).value += 0.125f;
    }

    private void IncreaseCharacterExp(string characterName, float amount)
    {
        Managers.Character.IncreaseCharacterExp(characterName, amount);
        
        GetSlider((int)SliderObjects.ResourceSlider).value = (Managers.Character.GetCharacterNowExp(characterName) / Managers.Character._levelUpXpTable[(int)Managers.Character.GetCharacterStatValue(_characterName, "Level") - 1])*100;
        GetText((int)PointTxt.NowResValue).text = Managers.Character.GetCharacterNowExp(_characterName).ToString();
        GetText((int)PointTxt.LevelTxt).text = Managers.Character.GetCharacterStatValue(_characterName, "Level").ToString();// Event 처리 가능
        GetText((int)PointTxt.NeedResValue).text = Managers.Character._levelUpXpTable[(int)Managers.Character.GetCharacterStatValue(_characterName, "Level") - 1].ToString();
        GetText((int)PointTxt.HadPointsValue).text = Managers.Character.GetCharacterStatValue(_characterName, "StatPoint").ToString();
    }

    private void RemoveCharacterStat(string characterName, string statName)
    {
        if (Math.Clamp(Managers.Character.GetCharacterStatValue(_characterName, statName), 0, 80) == 0)
            return;

        Managers.Character.RemoveCharacterStat(characterName, statName, 10);
        Managers.Character.IncreaseCharacterStat(characterName, "StatPoint", 1);
        GetText((int)PointTxt.HadPointsValue).text = Managers.Character.GetCharacterStatValue(_characterName, "StatPoint").ToString();
        GetSlider((int)Enum.Parse(typeof(SliderObjects), statName + "Slider")).value -= 0.125f;
    }

    private bool ValidValue()
    {
        bool isValid = Managers.Character.GetCharacterStatValue(_characterName, "StatPoint") != 0;
        return isValid;
    }
}

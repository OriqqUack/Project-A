using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_RocketStatControl : UI_Base
{
    enum BtnObjects
    {
        HealthPlus,
        EnergyPlus,
        PurificationPlus,
        EnforceBtn
    }

    enum SliderObjects
    {
        ResourceSlider,
        RocketHealthSlider,
        EnergyProductionRateSlider,
        PurificationEfficiencySlider
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
        Bind<Button>(typeof(BtnObjects));
        Bind<Slider>(typeof(SliderObjects));
        Bind<TextMeshProUGUI>(typeof(PointTxt));

        GetButton((int)BtnObjects.HealthPlus).onClick.AddListener(delegate { IncreaseRocketStat("RocketHealth"); });
        GetButton((int)BtnObjects.EnergyPlus).onClick.AddListener(delegate { IncreaseRocketStat("EnergyProductionRate"); });
        GetButton((int)BtnObjects.PurificationPlus).onClick.AddListener(delegate { IncreaseRocketStat("PurificationEfficiency"); });
        GetButton((int)BtnObjects.EnforceBtn).onClick.AddListener(delegate { IncreaseRocketExp(10); });

        GetSlider((int)SliderObjects.RocketHealthSlider).value = 0.125f * (Managers.Character.GetRocketStat("RocketHealth") / 10f);
        GetSlider((int)SliderObjects.EnergyProductionRateSlider).value = 0.125f * (Managers.Character.GetRocketStat("EnergyProductionRate") / 10f);
        GetSlider((int)SliderObjects.PurificationEfficiencySlider).value = 0.125f * (Managers.Character.GetRocketStat("PurificationEfficiency") / 10f);
    }

    private void Start()
    {
        GetSlider((int)SliderObjects.ResourceSlider).value = (Managers.Character.GetRocketStat("EXP") / (int)Managers.Character._levelUpXpTable[(int)Managers.Character.GetRocketStat("Level")-1])*100;
        
        GetText((int)PointTxt.HadPointsValue).text = Managers.Character.GetRocketStat("StatPoint").ToString();
        GetText((int)PointTxt.NeedResValue).text = Managers.Character._levelUpXpTable[(int)Managers.Character.GetRocketStat("Level") - 1].ToString();
        GetText((int)PointTxt.NowResValue).text = Managers.Character.GetRocketStat("EXP").ToString();
        GetText((int)PointTxt.LevelTxt).text = Managers.Character.GetRocketStat("Level").ToString();
    }

    private void IncreaseRocketStat(string statName)
    {
        if (ValidValue() == false)
            return;
        if (Managers.Character.GetRocketStat(statName) >= 80)
            return;

        Managers.Character.IncreaseRocketStat(statName, 10);
        Managers.Character.RemoveRocketStat("StatPoint", 1);

        GetText((int)PointTxt.HadPointsValue).text = Managers.Character.GetRocketStat("StatPoint").ToString();
        GetSlider((int)Enum.Parse(typeof(SliderObjects), statName + "Slider")).value += 0.125f;
    }

    private void IncreaseRocketExp(float amount)
    {
        Managers.Character.IncreaseRocketStat("EXP", amount);

        GetSlider((int)SliderObjects.ResourceSlider).value = (Managers.Character.GetRocketStat("EXP") / Managers.Character._levelUpXpTable[(int)Managers.Character.GetRocketStat("Level") - 1]) * 100;
        GetText((int)PointTxt.NowResValue).text = Managers.Character.GetRocketStat("EXP").ToString();
        GetText((int)PointTxt.LevelTxt).text = Managers.Character.GetRocketStat("Level").ToString();// Event 처리 가능
        GetText((int)PointTxt.NeedResValue).text = Managers.Character._levelUpXpTable[(int)Managers.Character.GetRocketStat("Level") - 1].ToString();
        GetText((int)PointTxt.HadPointsValue).text = Managers.Character.GetRocketStat("StatPoint").ToString();
    }

    private bool ValidValue()
    {
        bool isValid = Managers.Character.GetRocketStat("StatPoint") != 0;
        return isValid;
    }
}

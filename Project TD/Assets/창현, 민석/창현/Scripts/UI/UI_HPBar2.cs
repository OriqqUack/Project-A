using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_HPBar2 : UI_Base
{
    enum GameObjects
    {
        HPBar,
        Text
    }

    PlayerStat _playerStat;

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));

        _playerStat = GameObject.FindWithTag("Player").GetComponent<PlayerStat>();
        _playerStat.OnHpChanged += UpdateCurrentHp;
        _playerStat.OnMaxHpChanged += UpdateMaxHp;

        SetHpRatio(1);
    }

    void UpdateCurrentHp(int newHp)
    {
        float ratio = newHp / (float)_playerStat.MaxHp;
        SetHpRatio(ratio);
    }

    void UpdateMaxHp(int newMaxHp)
    {
        SetHpRatio(1);
    }


    public void SetHpRatio(float ratio)
    {
        GetObject((int)GameObjects.HPBar).GetComponent<Slider>().value = ratio;
        GetObject((int)GameObjects.Text).GetComponent<TextMeshProUGUI>().text = $"{_playerStat.Hp}/{_playerStat.MaxHp}";
    }

}
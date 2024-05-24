using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI_EXPBar : UI_Base
{
    enum GameObjects
    {
        EXPBar,
        Text
    }

    PlayerStat _playerStat;

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));

        _playerStat = GameObject.FindWithTag("Player").GetComponent<PlayerStat>();
    }

    // Update is called once per frame
    void Update()
    {
        float ratio = _playerStat.Exp / (float)_playerStat.TotalExp;
        SetHpRatio(ratio);
    }

    public void SetHpRatio(float ratio)
    {
        GetObject((int)GameObjects.EXPBar).GetComponent<Slider>().value = ratio;
        GetObject((int)GameObjects.Text).GetComponent<TextMeshProUGUI>().text = $"{_playerStat.Exp}/{_playerStat.TotalExp}";

    }
}

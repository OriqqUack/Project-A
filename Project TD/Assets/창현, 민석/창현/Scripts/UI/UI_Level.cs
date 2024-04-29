using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UI_Level : UI_Base
{
    enum GameObjects
    {
        Text
    }

    PlayerStat _playerStat;

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));

        _playerStat = GameObject.FindWithTag("Player").GetComponent<PlayerStat>();
        _playerStat.OnLevelChanged += UpdateLevel;

        GetObject((int)GameObjects.Text).GetComponent<TextMeshProUGUI>().text = _playerStat.Level.ToString();
    }

    void UpdateLevel(int newLevel)
    {
        GetObject((int)GameObjects.Text).GetComponent<TextMeshProUGUI>().text = newLevel.ToString();
    }

}

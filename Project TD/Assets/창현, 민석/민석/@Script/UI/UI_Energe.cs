using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Energe : UI_Popup
{
    PlayerStat _stat;
    string _text1, _text2;

    public override void Init()
    {
        base.Init();
        GameObject _player = Managers.Game.GetPlayer();
        _stat = _player.GetComponent<PlayerStat>();

        Bind<TextMeshProUGUI>(typeof(Define.UIText));

        //_text1 = GetText((int)Define.UIText.Timer_Text).text;
        //_text2 = GetText((int)Define.UIText.Energe_Text).text;

        GetText((int)Define.UIText.Timer_Text).text = string.Format("{0:N1}", EnergeController._timer);
        GetText((int)Define.UIText.Energe_Text).text = $"{_stat.Energe}";
    }


    // Update is called once per frame
    void Update()
    {
        //_text1 = string.Format("{0:N1}", EnergeController._timer);
        //_text2 = $"{_stat.Energe}";

        GetText((int)Define.UIText.Timer_Text).text = string.Format("{0:N1}", EnergeController._timer);
        GetText((int)Define.UIText.Energe_Text).text = $"{_stat.Energe}";
    }
}

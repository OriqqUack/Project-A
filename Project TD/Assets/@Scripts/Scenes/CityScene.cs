using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.City;
        GameObject[] npc = new GameObject[(int)Define.ObjectNumber.Npc];
        GameObject[] tp = new GameObject[(int)Define.ObjectNumber.TP];

        GameObject go = GameObject.Find("@Objects");
        GameObject go2 = GameObject.Find("@TP Point");

    }

    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Managers.UI.ClosePopupUI();
        }

    }

    public override void Clear()
    {

    }
}

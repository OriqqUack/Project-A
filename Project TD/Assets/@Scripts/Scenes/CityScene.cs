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

        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;
        gameObject.GetOrAddComponent<CursorController>();
        GameObject player = Managers.Game.Spawn(Define.WorldObject.Player, "SPlayer");

        if (go == null)
        {
            go = new GameObject { name = "@Objects" };
            go2 = new GameObject { name = "@TP Point" };
        }

        for (int i = 0; i < (int)Define.ObjectNumber.Npc; i++)
        {
            npc[i] = Managers.Game.Spawn(Define.WorldObject.Npc, $"NPC/Npc{i}");
            npc[i].transform.SetParent(GameObject.Find("@Objects").transform);
        }

        for (int i = 0; i < (int)Define.ObjectNumber.TP; i++)
        {
            tp[i] = Managers.Game.Spawn(Define.WorldObject.TP, $"TP Point/Point{i}");
            tp[i].transform.SetParent(GameObject.Find("@TP Point").transform);
        }

        GameObject box = Managers.Game.Spawn(Define.WorldObject.Box, $"Box/Box");
        box.transform.SetParent(GameObject.Find("@Objects").transform);

        GameObject roket = Managers.Game.Spawn(Define.WorldObject.Roket, $"Roket");
        roket.transform.SetParent(GameObject.Find("@Objects").transform);
        //Camera.main.gameObject.GetOrAddComponent<CameraController>().SetPlayer(player);

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

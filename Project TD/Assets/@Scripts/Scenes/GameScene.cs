using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        base.Init();

        SceneType = Define.Scene.Game;

        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;
        gameObject.GetOrAddComponent<CursorController>();
        GameObject player = Managers.Game.Spawn(Define.WorldObject.Player, "UnityChan");
        for (int i = 0; i < 3; i++)
        {
            GameObject npc = Managers.Game.Spawn(Define.WorldObject.Npc, $"NPC/Npc{i}");
        }
        GameObject box = Managers.Game.Spawn(Define.WorldObject.Box, $"Box/Box");
        Camera.main.gameObject.GetOrAddComponent<CameraController>().SetPlayer(player);

        //Managers.Game.Spawn(Define.WorldObject.Monster, "Knight");
        GameObject go = new GameObject { name = "SpawningPool" };
        SpawningPool pool = go.GetOrAddComponent<SpawningPool>();
        pool.SetKeepMonsterCount(0);
    }

    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && Util.FindChild(Managers.UI.Root.gameObject, "Craft", true) == null)
        {
            Managers.UI.ShowPopupUI<UI_Popup>("Craft/Craft");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Managers.UI.ClosePopupUI();
        }

    }
    public override void Clear()
    {
        
    }
}

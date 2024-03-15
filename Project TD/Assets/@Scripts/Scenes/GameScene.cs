﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;
        //Managers.UI.ShowSceneUI<UI_Inven>();
        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;
        Dictionary<string, Data.MonsterStat> dict2 = Managers.Data.MonsterDict;
        gameObject.GetOrAddComponent<CursorController>();

        GameObject player = Managers.Game.PlayerSpawn(Define.Players.Normal, "UnityChan");
        Camera.main.gameObject.GetOrAddComponent<CameraController>().SetPlayer(player);

        Managers.Game.MonsterSpawn(Define.Monsters.Slime, "Knight");
        GameObject go = new GameObject { name = "SpawningPool" };
        SpawningPool pool = go.GetOrAddComponent<SpawningPool>();
        pool.SetKeepMonsterCount(2);
    }
    
    public override void Clear()
    {
        
    }
}

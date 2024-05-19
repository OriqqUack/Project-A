using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Start;

    }

    private void Start()
    {
        Managers.Sound.Play("StartSceneBGM",Define.Sound.Bgm);
    }

    public override void Clear()
    {
    }

    
}

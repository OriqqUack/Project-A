using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Main;

        Managers.UI.ShowSceneUI<UI_Scene>("MainScene/Energe");
        
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Managers.UI.CloseAllPopupUI();
        }
    }

    public override void Clear()
    {
        // 씬 끝날 때 처리
    }
}

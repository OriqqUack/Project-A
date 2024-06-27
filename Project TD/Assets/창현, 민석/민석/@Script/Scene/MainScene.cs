using Minseok.CollectionSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : BaseScene
{
    private GameObject _energe;

    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Main;
        Managers.UI.ShowSceneUI<UI_Scene>("MainScene/Energe");

        _energe = GameObject.Find("Energe");

    }

    public void Update()
    {
        if (Define._TabletActive)
            _energe.SetActive(false);
        else
            _energe.SetActive(true);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Define._TabletActive = false;
            Managers.UI.CloseAllPopupUI();
        }
    }

    public override void Clear()
    {
        // 씬 끝날 때 처리
    }
}

using Rito.InventorySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : BaseScene
{
    [SerializeField]
    public GameObject _Inven;
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Main;
        Managers.UI.ShowSceneUI<UI_Scene>("MainScene/Energe");

        //Managers.UI.ShowPopupUI<UI_Popup>("Inven");
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Define._InvenActive = false;
            Managers.UI.CloseAllPopupUI();
        }
    }

    public override void Clear()
    {
        // 씬 끝날 때 처리
    }
}

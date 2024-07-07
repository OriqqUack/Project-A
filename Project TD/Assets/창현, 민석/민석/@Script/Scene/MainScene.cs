using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
using Minseok.Collection;

public class MainScene : BaseScene
{
    private GameObject _energe;
    private CollectionData _collectionData;

    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Main;
        Managers.UI.ShowSceneUI<UI_Scene>("MainScene/Energe");

        _collectionData = new CollectionData();

        _energe = GameObject.Find("Energe");
    }

    public void Update()
    {
        if (Define._TabletActive)
            _energe.SetActive(false);
        else
            _energe.SetActive(true);

        if (Input.GetKeyUp(KeyCode.I))
            Managers.UI.ShowPopupUI<UI_Popup>("SaveTest");

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Define._TabletActive = false;
            Managers.UI.CloseAllPopupUI();
        }
    }

    public void CollectionSave()
    {
        _collectionData.SaveCollection();
    }

    public void CollectionLoad()
    {
        _collectionData.LoadCollection();
    }


    public override void Clear()
    {
        // 씬 끝날 때 처리
    }
}

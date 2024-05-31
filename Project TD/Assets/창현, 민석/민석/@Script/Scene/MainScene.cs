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
        GameObject go = Util.FindChild(_Inven, "Inven", false);
        Canvas ca = go.GetComponent<Canvas>();
        ca.sortingOrder = 12;
        _Inven.SetActive(false);
        Managers.UI.ShowSceneUI<UI_Scene>("MainScene/Energe");
    }

    public void Update()
    {
        if (Define._InvenActive)
            _Inven.SetActive(true);
        else
            _Inven.SetActive(false);

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

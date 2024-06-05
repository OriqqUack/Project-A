using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_DeletePopup : UI_Base
{
    enum GameObjects
    {
        Cancel,
        Delete
    }
    public override void Init()
    {
        Bind<Button>(typeof(GameObjects));
        GetButton((int)GameObjects.Cancel).onClick.AddListener(delegate { OnClickedCancelBtn(); });
        GetButton((int)GameObjects.Delete).onClick.AddListener(delegate { OnClickedDeleteBtn(); });
    }

    public void OnClickedDeleteBtn()
    {
        int nowSlot = DataPersistenceManager.instance.nowSlot;
        DataPersistenceManager.instance.DataClear(nowSlot);
        this.gameObject.SetActive(false);
    }

    public void OnClickedCancelBtn()
    {
        this.gameObject.SetActive(false);
    }
}

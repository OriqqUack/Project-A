using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInven : UI_Scene
{
    enum GameObjects
    {
        GridPanel
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        var listWeaponItems = Managers.Data.ShopWeaponData;

        GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);
        foreach (Transform child in gridPanel.transform)
            Managers.Resource.Destroy(child.gameObject);

        // ���� �κ��丮 ������ �����ؼ�
        foreach (var weaponItem in listWeaponItems.Values)
        {
            GameObject item = Managers.UI.MakeSubItem<UI_Inven_Item>(gridPanel.transform).gameObject;
            UI_Inven_Item invenItem = item.GetOrAddComponent<UI_Inven_Item>();
            invenItem.SetInfo(weaponItem);
        }
    }
}

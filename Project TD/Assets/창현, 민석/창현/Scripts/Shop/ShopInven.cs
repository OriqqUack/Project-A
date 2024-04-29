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
        var test = Managers.Data.StatDict;

        GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);
        foreach (Transform child in gridPanel.transform)
            Managers.Resource.Destroy(child.gameObject);

        Debug.Log(listWeaponItems.Values.Count);
        Debug.Log(test.Values.Count);
        Debug.Log(Managers.Data.ShopWeaponPer.Count);
        // 실제 인벤토리 정보를 참고해서
        foreach (Data.JsonWeapon weaponItem in listWeaponItems.Values)
        {
            GameObject item = Managers.UI.MakeSubItem<UI_Inven_Item>(gridPanel.transform).gameObject;
            UI_Inven_Item invenItem = item.GetOrAddComponent<UI_Inven_Item>();
            invenItem.SetInfo(weaponItem.item_name);
        }
    }
}

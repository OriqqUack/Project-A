using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_ItemDescript : UI_Base
{
    enum GameObjects
    {
        ItemName,
        ItemInformation,
    }
    public override void Init()
    {
        Bind<TextMeshProUGUI>(typeof(GameObjects));

        Inventory1.Singleton.OnClickedItem -= OnChangedText;
        Inventory1.Singleton.OnClickedItem += OnChangedText;
    }

    private void OnChangedText(Item item)
    {

        GetText((int)GameObjects.ItemName).text = item.ItemName;
        //GetText((int)GameObjects.ItemInfo).text = item
    }

}

using Rito.InventorySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Decomposer : UI_Popup
{
    private Dictionary<Define.ItemGrade, List<ItemData>> gradeToMaterials;

    [SerializeField]
    private List<ItemData> normalItemDecomposer;
    [SerializeField]
    private List<ItemData> rearItemDecomposer;
    [SerializeField]
    private List<ItemData> epicItemDecomposer;
    [SerializeField]
    private List<ItemData> legendaryItemDecomposer;

    private ItemSlotUI b_slot1;
    private ItemSlotUI b_slot2;
    private ItemSlotUI b_slot3;
    private ItemSlotUI b_slot4;
    private ItemSlotUI b_slot5;
    private ItemSlotUI b_slot6;

    private ItemSlotUI a_slot1;
    private ItemSlotUI a_slot2;
    private ItemSlotUI a_slot3;
    private ItemSlotUI a_slot4;
    private ItemSlotUI a_slot5;
    private ItemSlotUI a_slot6;


    enum BeforeItemSlots
    {
        ItemSlot1,
        ItemSlot2,
        ItemSlot3,
        ItemSlot4,
        ItemSlot5,
        ItemSlot6
    }

    enum AfterItemSlots
    {
        ItemSlot_1,
        ItemSlot_2,
        ItemSlot_3,
        ItemSlot_4,
        ItemSlot_5,
        ItemSlot_6
    }
    public override void Init()
    {
        Bind<ItemSlotUI>(typeof(BeforeItemSlots));
        Bind<ItemSlotUI>(typeof(AfterItemSlots));

        Inventory.Instance.onChangedSlotUI += new Inventory.OnChangedSlotUI(UpdateSlotUI);

        InitializeList();
    }

    void InitializeList()
    {
        gradeToMaterials = new Dictionary<Define.ItemGrade, List<ItemData>>();

        gradeToMaterials[Define.ItemGrade.Normal] = normalItemDecomposer;
        gradeToMaterials[Define.ItemGrade.Rear] = rearItemDecomposer;
        gradeToMaterials[Define.ItemGrade.Epic] = epicItemDecomposer;
        gradeToMaterials[Define.ItemGrade.Legendary] = legendaryItemDecomposer;

        int index = 18;

        Get<ItemSlotUI>((int)BeforeItemSlots.ItemSlot1).SetSlotIndex(++index);
        b_slot1 = Get<ItemSlotUI>((int)BeforeItemSlots.ItemSlot1);
        b_slot1.SetSlotIndex(++index);
        b_slot2 = Get<ItemSlotUI>((int)BeforeItemSlots.ItemSlot2);
        b_slot2.SetSlotIndex(++index);
        b_slot3 = Get<ItemSlotUI>((int)BeforeItemSlots.ItemSlot3);
        b_slot3.SetSlotIndex(++index);
        b_slot4 = Get<ItemSlotUI>((int)BeforeItemSlots.ItemSlot4);
        b_slot4.SetSlotIndex(++index);
        b_slot5 = Get<ItemSlotUI>((int)BeforeItemSlots.ItemSlot5);
        b_slot5.SetSlotIndex(++index);
        b_slot6 = Get<ItemSlotUI>((int)BeforeItemSlots.ItemSlot6);
        b_slot6.SetSlotIndex(++index);


        a_slot1 = Get<ItemSlotUI>((int)AfterItemSlots.ItemSlot_1);
        a_slot2 = Get<ItemSlotUI>((int)AfterItemSlots.ItemSlot_2);
        a_slot3 = Get<ItemSlotUI>((int)AfterItemSlots.ItemSlot_3);
        a_slot4 = Get<ItemSlotUI>((int)AfterItemSlots.ItemSlot_4);
        a_slot5 = Get<ItemSlotUI>((int)AfterItemSlots.ItemSlot_5);
        a_slot6 = Get<ItemSlotUI>((int)AfterItemSlots.ItemSlot_6);
    }

    private void Start()
    {
       
    }

    public List<ItemData> DecomposeItem(List<Define.ItemGrade> beforItemList)
    {
        List<ItemData> resultItems = new List<ItemData>();
        foreach(var item in beforItemList)
        {
            if (gradeToMaterials.ContainsKey(item))
            {
                foreach(var resultItem in gradeToMaterials[item])
                {
                    if(resultItem is CountableItemData ciData)
                    {
                    }
                }
            }
        }
        return resultItems;
    }

    public void BtnCompose()
    {
        List<Define.ItemGrade> list = new List<Define.ItemGrade>();
        List<ItemData> afterItem;
        list.Add(b_slot1.item.itemGrade);
        list.Add(b_slot2.item.itemGrade);
        list.Add(b_slot3.item.itemGrade);
        list.Add(b_slot4.item.itemGrade);
        list.Add(b_slot5.item.itemGrade);
        list.Add(b_slot6.item.itemGrade);

        afterItem = DecomposeItem(list);

        a_slot1.item = afterItem[0];
        a_slot1.SetItem(afterItem[0].IconSprite);

        a_slot2.item = afterItem[1];
        a_slot2.SetItem(afterItem[1].IconSprite);

        a_slot3.item = afterItem[2];
        a_slot3.SetItem(afterItem[2].IconSprite);

        a_slot4.item = afterItem[3];
        a_slot4.SetItem(afterItem[3].IconSprite);

        a_slot5.item = afterItem[4];
        a_slot5.SetItem(afterItem[4].IconSprite);

        a_slot6.item = afterItem[5];
        a_slot6.SetItem(afterItem[5].IconSprite);

        //TODO : 중복되는 아이템은 카운팅이 되어야 함
    }

    private void UpdateSlotUI()
    {

    }
}

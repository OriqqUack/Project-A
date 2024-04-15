using Data;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CollectionManager : MonoBehaviour, IDataPersistence
{
    public GameObject collectionEntryPrefab;
    public List<Transform> collectionContent;
    public Sprite unkownSprite;

    private Data.CollectionEntry[] collectionEntries;
    private bool[] discovered;

    private void Awake()
    {
        #region ����Ʈ �ʱ�ȭ
        Transform gameObject = transform.Find("ItemInfo");
        Transform[] allChilderen = gameObject.GetComponentsInChildren<Transform>();

        for(int i = 0; i < allChilderen.Length; i++)
            collectionContent.Add(allChilderen[i]);

        collectionContent.RemoveAt(0);
        #endregion
    }
    private void Start()
    {
        InitializeCollection();
    }

    void InitializeCollection()
    {
        LoadCollectionEntries();

        for(int i = 0; i < collectionEntries.Length; i++)
        {
            GameObject collectionEntryObj = Instantiate(collectionEntryPrefab, collectionContent[i]);
            Button button = collectionEntryObj.GetComponent<Button>();
            int index = i; //Ŭ���� ���� �ذ�
            button.onClick.AddListener(() => OnCollectionEntryClick(index));

            Image image = collectionEntryObj.GetComponent<Image>();
            if (discovered[i])
                image.sprite = Managers.Resource.Load<Sprite>(collectionEntries[i].imagePath);
            else
                image.sprite = unkownSprite;
        }
    }

    void LoadCollectionEntries()
    {

    }

    void OnCollectionEntryClick(int index)
    {
        if (discovered[index]) 
        {
            //TODO : ������ ���� ������ ��Ȳ
            Debug.Log("Open Collection : " + index.ToString());
        }
        else
        {
            //TODO : ������ �ȿ��� ������
            Debug.Log("Not opened Collection!");
        }
    }

    public void LoadData(GameData data)
    {
        collectionEntries = data.collectionEntries;
        discovered = data.collectionDiscovered;
    }

    public void SaveData(ref GameData data)
    {
        data.collectionEntries = collectionEntries;
        data.collectionDiscovered = discovered;
    }
}


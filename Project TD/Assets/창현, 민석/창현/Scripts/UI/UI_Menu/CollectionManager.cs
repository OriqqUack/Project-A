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
        #region 리스트 초기화
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
            int index = i; //클로저 문제 해결
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
            //TODO : 도감이 열려 있을때 상황
            Debug.Log("Open Collection : " + index.ToString());
        }
        else
        {
            //TODO : 도감이 안열려 있을때
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


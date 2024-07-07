using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
using Minseok.Collection;
using System.IO;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class DataManager
{
    public Dictionary<string, Data.MonsterStat> MonsterDict { get; private set; } = new Dictionary<string, Data.MonsterStat>();

    public Dictionary<string, CollectionData.Collection> collectionDic = new Dictionary<string, CollectionData.Collection>();


    public void Init()
    {
        MonsterDict = LoadJson<Data.MonsterData, string, Data.MonsterStat>("MonsterData").MakeDict();
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        try
        {
            return JsonUtility.FromJson<Loader>(textAsset.text);
        }
        catch (Exception e)
        {
            Debug.LogError($"JSON 파싱 오류: {e}");
            return default(Loader);
        }
    }

}

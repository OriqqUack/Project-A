using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//저장하는 방법
//1. 저장할 데이터가 존재
//2. 데이터를 제이슨으로 변환
//3. 제이슨을 외부에 저장

//불러오는 방법
//1. 외부에 저장된 제이슨을 가져옴
//2. 제이슨을 데이터 형태로 변환
//3. 불러온 데이터를 사용

public class PlayerData
{
    public string name;
    public PlayerStat stat;
}

public class DataController : MonoBehaviour
{
    //싱글톤
    public static DataController instance;

    public PlayerData _playerData = new PlayerData();

    public string path;
    public int nowSlot;

    private void Awake()
    {
        #region 싱글톤
        /*GameObject go = GameObject.Find("DataController");
        if (go == null)
        {
            go = new GameObject { name = "DataController" };
            go.AddComponent<DataController>();
        }
        DontDestroyOnLoad(go);
        instance = go.GetComponent<DataController>();*/
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        #endregion

        path = Application.persistentDataPath + "/save";
    }

    private void Start()
    {
    }

    public void SaveData()
    {
        _playerData.stat = GameObject.FindWithTag("Player").GetComponent<PlayerStat>();
        string data = JsonUtility.ToJson(_playerData);

        File.WriteAllText(path + nowSlot.ToString(), data);
    }

    public void LoadData()
    {
        string data = File.ReadAllText(path + nowSlot.ToString());
        _playerData = JsonUtility.FromJson<PlayerData>(data);
    }

    public void DataClear()
    {
        nowSlot -= 1;
        _playerData = new PlayerData();
    }
}

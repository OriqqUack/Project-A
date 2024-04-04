using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//�����ϴ� ���
//1. ������ �����Ͱ� ����
//2. �����͸� ���̽����� ��ȯ
//3. ���̽��� �ܺο� ����

//�ҷ����� ���
//1. �ܺο� ����� ���̽��� ������
//2. ���̽��� ������ ���·� ��ȯ
//3. �ҷ��� �����͸� ���

public class PlayerData
{
    public string name;
    public PlayerStat stat;
}

public class DataController : MonoBehaviour
{
    //�̱���
    public static DataController instance;

    public PlayerData _playerData = new PlayerData();

    public string path;
    public int nowSlot;

    private void Awake()
    {
        #region �̱���
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

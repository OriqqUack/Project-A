using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using System.Text;
using Unity.VisualScripting;
using System.Linq;

[System.Serializable]
public class KeyData
{
    //�ش� Ű�� ���ó(�̸�)
    public string keyName;

    //����Ƽ���� �����ϴ� KeyCode ����
    public KeyCode keyCode; //json���·� ������ �� ���� KeyCode.I �� �ƴ϶� 106(����)�� ������ �ȴ�. (enum)

    //KeyData ������
    public KeyData(string keyName, KeyCode keyCode)
    {
        this.keyName = keyName;
        this.keyCode = keyCode;
    }
}

/// <summary>
/// Ű �Է¿� ���� ������ �������ְ�, Ư���� ��ɿ� �����ϴ� Ű�� �����ϴ� �Ŵ��� Ŭ����
/// </summary>
public class KeyManager : MonoBehaviour
{
    public static KeyManager instance { get; private set; }

    private static string mOptionDataFileName = "/KeyData.json"; //Ű ������ ���� �̸�
    private static string mFilePath;

    private Dictionary<string, KeyCode> mKeyDictionary;
    private Dictionary<string, KeyCode> beforeChangedKeyDictionary;


    public bool isKeyChanged = true;

    void Awake()
    {
        if (instance == null)
        {
            GameObject go = GameObject.Find("@KeyManager");
            if (go == null)
            {
                go = new GameObject { name = "@KeyManager" };
                go.AddComponent<KeyManager>();
            }

            DontDestroyOnLoad(go);
            instance = go.GetComponent<KeyManager>();
        }

        mKeyDictionary = new Dictionary<string, KeyCode>();
        mFilePath = Application.persistentDataPath + mOptionDataFileName;

        ResetOptionData();

        LoadOptionData();
    }

    private void LoadOptionData()
    {
        // ����� ������ �ִٸ�
        if (File.Exists(mFilePath))
        {
            string fromJsonData = File.ReadAllText(mFilePath);

            List<KeyData> keyList = JsonConvert.DeserializeObject<List<KeyData>>(fromJsonData);

            foreach (var data in keyList)
            {
                mKeyDictionary.Add(data.keyName, data.keyCode);
                beforeChangedKeyDictionary.Add(data.keyName, data.keyCode);
            }
        }
        // ����� ������ ���ٸ�
        else
        {
            Debug.Log(GetType() + " ������ ����");

            ResetOptionData();
        }
    }

    /// <summary>
    /// ������Ʈ���� ������ �ش� ������ ������ �°� Ű�� �����Ѵ�.
    /// ��ũ��Ʈ���� ������ Ű�� �缳���ȴ�.
    /// </summary>
    private void ResetOptionData()
    {
        mKeyDictionary.Clear();

        //�� ������ ����� Ű �����͵�//
        mKeyDictionary.Add("Inventory", KeyCode.I); //������ �κ��丮
        mKeyDictionary.Add("Stat", KeyCode.P); //����
        mKeyDictionary.Add("Rolling", KeyCode.Space); //��ų
        mKeyDictionary.Add("Interaction", KeyCode.F); //��ȣ�ۿ�
        mKeyDictionary.Add("IndexBook", KeyCode.C); //����
        mKeyDictionary.Add("Build", KeyCode.B); //�ǹ�����

        mKeyDictionary.Add("ItemQuickSlot1", KeyCode.Alpha1); //������ ������ 1��
        mKeyDictionary.Add("ItemQuickSlot2", KeyCode.Alpha2); //������ ������ 2��
        mKeyDictionary.Add("ItemQuickSlot3", KeyCode.Alpha3); //������ ������ 3��
        mKeyDictionary.Add("ItemQuickSlot4", KeyCode.Alpha4); //������ ������ 4��
        mKeyDictionary.Add("ItemQuickSlot5", KeyCode.Alpha5); //������ ������ 5��
        mKeyDictionary.Add("ItemQuickSlot6", KeyCode.Alpha5); //������ ������ 6��

        Debug.Log(GetType() + " �ʱ�ȭ");

        beforeChangedKeyDictionary = mKeyDictionary.ToDictionary(entry => entry.Key, entry => entry.Value);
        SaveOptionData();
    }

    public void SaveOptionData()
    {
        //��ųʸ��� �ִ� Ű �����͵��� ������Ʈ ����Ʈ�� �̿��Ͽ� �±׸� ���� ����ȭ��Ų��.
        //����Ʈ�� ������� �ʰ� ��ųʸ��� ����ȭ�ϸ� �±װ� ���⿡ ����� �� ����. ������Ʈ ����(KeyData)�� �����, Object type�� json ���Ϸ� �������.
        //https://www.geeksforgeeks.org/json-data-types/#:~:text=JSON%20(JavaScript%20Object%20Notation)%20is,easy%20to%20understand%20and%20generate.

        //KeyData�� ������Ʈ�� ���� ����Ʈ
        List<KeyData> keys = new List<KeyData>();

        if(isKeyChanged) //Ű�� �ٲ������ mkey, �ȹٲ������ before�� ��
        {
            mKeyDictionary = beforeChangedKeyDictionary.ToDictionary(entry => entry.Key, entry => entry.Value);
        }

        foreach (KeyValuePair<string, KeyCode> keyName in mKeyDictionary)
        {
            keys.Add(new KeyData(keyName.Key, keyName.Value));
        }

        //List<KeyData>�� SeriaizeObject�� �ϸ� Object type json�� ���´�.
        string jsonData = JsonConvert.SerializeObject(keys);

        //���Ϸ� ����
        FileStream fileStream = new FileStream(mFilePath, FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();

        Debug.Log(GetType() + " ���� ����");
    }

    /// <summary>
    /// Ű �̸��� ������� �ش� Ű�� ��ϵ� KeyCode�� �����Ѵ�.
    /// </summary>
    /// <param name="keyName"></param>
    /// <returns></returns>
    public KeyCode GetKeyCode(string keyName)
    {
        return mKeyDictionary[keyName];
    }

    /// <summary>
    /// �ش� Ű���� �ڱ� �ڽ��� ������ Ű�� ��ϵǾ��ִ°�츦 �����ϰ�, Ư���� Ű ������ �����ϱ����� Ű�� üũ�Ѵ�.
    /// </summary>
    /// <returns>�Ҵ� ������ Ű�ΰ�?</returns>
    public bool CheckKey(KeyCode key, KeyCode currentKey)
    {
        //����1. ���� �Ҵ�� Ű�� ���� Ű�� �����ϵ��� �� ���� ������� �����Ѵ�.
        if (currentKey == key) { return true; }

        //1�� Ű �˻�. 
        //Ű�� �Ʒ��� Ű�� ����Ѵ�.
        if
        (
            key >= KeyCode.A && key <= KeyCode.Z || //97 ~ 122   A~Z
            key >= KeyCode.Alpha0 && key <= KeyCode.Alpha9 || //48 ~ 57    ���� 0~9
            key == KeyCode.Quote || //39         
            key == KeyCode.Comma || //44
            key == KeyCode.Period || //46
            key == KeyCode.Slash || //47
            key == KeyCode.Semicolon || //59
            key == KeyCode.LeftBracket || //91
            key == KeyCode.RightBracket || //93
            key == KeyCode.Minus || //45
            key == KeyCode.Equals || //61
            key == KeyCode.BackQuote //96
        ) { }
        else { return false; }

        //2�� Ű �˻�. 
        //1�� Ű �˻縦 ������ Ű �� ���� ���ǹ� Ű�� ������ �� ����.
        if
        (
            //�̵� Ű WASD
            key == KeyCode.W ||
            key == KeyCode.A ||
            key == KeyCode.S ||
            key == KeyCode.D
        ) { return false; }

        //3�� Ű �˻�.
        //���� ������ Ű�� �� �̹� �Ҵ�� Ű�� �ִ°��� ������ �� ����.
        foreach (KeyValuePair<string, KeyCode> keyPair in mKeyDictionary)
        {
            if (key == keyPair.Value)
            {
                return false;
            }
        }

        //��� Ű �˻縦 ����ϸ� �ش� Ű�� ������ ������ Ű.
        return true;
    }

    /// <summary>
    /// keyName�� �ش��ϴ� Ű�� KeyCode�� key�� �����Ų��.
    /// </summary>
    /// <param name="keyCode">���� �����ϴ� Ű�� �ڵ尪(enum)</param>
    /// <param name="keyName">������ Ű(keyCode)�� keyName�� �Ҵ��Ѵ�</param>
    public void AssignKey(KeyCode keyCode, string keyName)
    {
        //��ųʸ� 
        mKeyDictionary[keyName] = keyCode;

        //Ű ������ ���ÿ� ����
        //SaveOptionData();
    }

    public void LoadData(GameData data)
    {
        throw new System.NotImplementedException();
    }

    public void SaveData(ref GameData data)
    {
        throw new System.NotImplementedException();
    }
}
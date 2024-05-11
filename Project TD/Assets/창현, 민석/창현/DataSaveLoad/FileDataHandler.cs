using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler 
{
    private string dataDirPath = "";
    private string dataFileName = "";

    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public T DataLoad<T>()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        T loadedData = default(T);
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                loadedData = JsonUtility.FromJson<T>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Error occured when trying to save data to file : " + fullPath + "\n" + e);
            }
        }
        return loadedData;
    }

    public void Save(GameData data)
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName); // �ü������ ��� ������ �޶�
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = JsonUtility.ToJson(data, true); //true�� �б� ���� ���� false�� ����
            //FileStream�� ����Ͽ� ������ �����ϰų� ���� ���Ͽ� �����͸� ���ϴ�.
            //StreamWriter�� ����Ͽ� ���Ͽ� �����͸� ���ϴ�.
            using (FileStream stream = new FileStream(fullPath, FileMode.Create)) //using ���� FileStream��  // ���� �� ������ ����, ����� �ش����� �����
            {                                                                     //StreamWriter ���� �� �˾Ƽ� ����
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error occured when trying to save data to file : " + fullPath + "\n" + e);
        }
    }

    public void Save(GlobalData data)
    {
        string fullPath = Path.Combine(dataDirPath, "GlobalData");
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = JsonUtility.ToJson(data, true); //true�� �б� ���� ���� false�� ����
            //FileStream�� ����Ͽ� ������ �����ϰų� ���� ���Ͽ� �����͸� ���ϴ�.
            //StreamWriter�� ����Ͽ� ���Ͽ� �����͸� ���ϴ�.
            using (FileStream stream = new FileStream(fullPath, FileMode.Create)) //using ���� FileStream��  // ���� �� ������ ����, ����� �ش����� �����
            {                                                                     //StreamWriter ���� �� �˾Ƽ� ����
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error occured when trying to save data to file : " + fullPath + "\n" + e);
        }
    }

    public void DeleteSave()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
            Debug.Log($"Delete SaveFile : {fullPath}");
        }
        else
        {
            Debug.LogWarning("Not found SaveFile");
        }
    }
}

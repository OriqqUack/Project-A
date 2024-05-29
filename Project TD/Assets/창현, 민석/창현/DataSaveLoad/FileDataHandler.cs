using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

[System.Serializable]
public class KeyValue
{
    public string key;
    public Character value;
}
public static class DictionaryUtility
{
    public static List<KeyValue> ConvertToKeyValueList(Dictionary<string, Character> dictionary)
    {
        List<KeyValue> keyValueList = new List<KeyValue>();
        foreach (var kvp in dictionary)
        {
            keyValueList.Add(new KeyValue { key = kvp.Key, value = kvp.Value });
        }
        return keyValueList;
    }

    public static Dictionary<string, Character> ConvertToDictionary(List<KeyValue> keyValueList)
    {
        Dictionary<string, Character> dictionary = new Dictionary<string, Character>();
        foreach (var keyValue in keyValueList)
        {
            dictionary[keyValue.key] = keyValue.value;
        }
        return dictionary;
    }
}



public class FileDataHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";

    JsonSerializerSettings settings;
    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;

        settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple,
            Converters = new List<JsonConverter> { new IStatConverter() }
        };
    }

    public T DataLoad<T>()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        T loadedData = default(T);
        if (File.Exists(fullPath))
        {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                loadedData = JsonConvert.DeserializeObject<T>(dataToLoad, settings);
        }
        return loadedData;
    }

    public void Save(GameData data)
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = JsonConvert.SerializeObject(data, settings);
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error occurred when trying to save data to file: " + fullPath + "\n" + e);
        }
    }

    public void Save(GlobalData data)
    {
        string fullPath = Path.Combine(dataDirPath, "GlobalData");
        //try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = JsonUtility.ToJson(data, true); //true시 읽기 쉬운 형태 false는 압축
                                                                    //FileStream을 사용하여 파일을 생성하거나 열고 파일에 데이터를 씁니다.
                                                                    //StreamWriter를 사용하여 파일에 데이터를 씁니다.
            using (FileStream stream = new FileStream(fullPath, FileMode.Create)) //using 사용시 FileStream과  // 없을 시 새파일 생성, 존재시 해당파일 덮어쓰기
            {                                                                     //StreamWriter 사용된 후 알아서 닫힘
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        //catch (Exception e)
        {
            //Debug.LogError("Error occured when trying to save data to file : " + fullPath + "\n" + e);
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

public class IStatConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return (objectType == typeof(IStat));
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        // JsonReader로부터 데이터를 읽어와서 적절한 IStat 구현 클래스로 역직렬화합니다.
        JObject obj = JObject.Load(reader);

        // "type" 속성을 사용하여 어떤 클래스를 역직렬화할지 결정합니다.
        JToken typeToken = obj["$type"];
        string type = (typeToken != null) ? typeToken.ToString() : null;

        IStat stat;
        type = type.Replace(", Assembly-CSharp","");
        switch (type)
        {
            case "Health":
                Debug.Log(obj["_healthValue"]);
                float healthValue = obj["_healthValue"]?.ToObject<float>() ?? 0;
                stat = new Health(healthValue);
                break;
            case "Level":
                Debug.Log(obj["_levelValue"]);
                float levelValue = obj["_levelValue"]?.ToObject<float>() ?? 0;
                stat = new Health(levelValue);
                break;
            case "StatPoint":
                int statPoints = obj["_statPoints"]?.ToObject<int>() ?? 0;
                stat = new StatPoint(statPoints);
                break;
            case "Speed":
                float speedValue = obj["_speedValue"]?.ToObject<float>() ?? 0;
                stat = new Speed(speedValue);
                break;
            case "AttackStat":
                float attackValue = obj["_attackStatValue"]?.ToObject<float>() ?? 0;
                stat = new AttackStat(attackValue);
                break;
            case "AttackSpeed":
                float attackSpeedValue = obj["_attackSpeedValue"]?.ToObject<float>() ?? 0;
                stat = new AttackSpeed(attackSpeedValue);
                break;
            case "Defense":
                float defenseValue = obj["_defenseValue"]?.ToObject<float>() ?? 0;
                stat = new Defense(defenseValue);
                break;
            case "CriticalPer":
                float criticalPerValue = obj["_criticalPerValue"]?.ToObject<float>() ?? 0;
                stat = new CriticalPer(criticalPerValue);
                break;
            case "CriticalAtk":
                float criticalAtkValue = obj["_criticalAtkValue"]?.ToObject<float>() ?? 0;
                stat = new CriticalAtk(criticalAtkValue);
                break;
            default:
                throw new JsonSerializationException($"Unknown type: {type}");
        }

        serializer.Populate(obj.CreateReader(), stat);
        return stat;
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        // 주어진 IStat 객체를 JSON으로 직렬화합니다.
        serializer.Serialize(writer, value);
    }
}
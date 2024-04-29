using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

public class DataPersistenceManager : MonoBehaviour
{
    public string fileName;

    private GameData gameData;
    private GlobalData globalData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;

    public GameData GameData
    {
        get { return gameData; }
    }

    public GlobalData GlobalData
    {
        get { return globalData; }
    }

    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Not exist Data Persistence Manager in the scene.");
        }
        instance = this;

        fileName = "GlobalData";
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistenceObjects = FindAllDataPersistenceObejcts();

        //this.globaldata
    }

    private void Start()
    {
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame(int nowSlot)
    {
        fileName = Path.Combine("save", nowSlot.ToString());
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        

        this.gameData = dataHandler.DataLoad<GameData>();

        if (this.gameData == null)
        {
            Debug.Log("No data was FOund. Initialzing data to defaults");
            NewGame();
        }

        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }

        dataHandler.Save(gameData);
    }

    public void DataClear()
    {
        gameData = new GameData();
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObejcts()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = 
            FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}

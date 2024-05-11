using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

public class DataPersistenceManager : MonoBehaviour
{
    public string fileName;
    public int nowSlot;

    private GameData gameData;
    private GlobalData globalData;
    private List<IGameDataPersistence> gameDataPersistenceObjects;
    private List<IGlobalDataPersistence> globalDataPersistenceObjects;
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
        nowSlot = 4;
        if (instance != null)
        {
            Debug.LogError("Not exist Data Persistence Manager in the scene.");
        }
        instance = this;

        
    }

    private void Start()
    {
        LoadStartScene();
        SaveGame();
    }

    #region GlobalData
    public void LoadStartScene()
    {
        fileName = "GlobalData";
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.globalDataPersistenceObjects = FindAllGlobalDataPersistenceObejcts();

        this.globalData = dataHandler.DataLoad<GlobalData>();

        if (this.globalData == null)
        {
            Debug.Log("No data was FOund. Initialzing data to defaults");
            StartGame();
            return;
        }

        foreach (IGlobalDataPersistence dataPersistenceObj in globalDataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(globalData);
        }
    }

    private List<IGlobalDataPersistence> FindAllGlobalDataPersistenceObejcts()
    {
        IEnumerable<IGlobalDataPersistence> dataPersistenceObjects =
            FindObjectsOfType<MonoBehaviour>().OfType<IGlobalDataPersistence>();

        return new List<IGlobalDataPersistence>(dataPersistenceObjects);
    }

    public void StartGame()
    {
        this.globalData = new GlobalData();
    }
    #endregion

    #region GameData
    public void LoadGame(int nowSlot)
    {
        fileName = Path.Combine($"save{nowSlot}");
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.gameDataPersistenceObjects = FindAllGameDataPersistenceObejcts();

        this.gameData = dataHandler.DataLoad<GameData>();

        if (this.gameData == null)
        {
            Debug.Log("No data was FOund. Initialzing data to defaults");
            NewGame();
            return;
        }

        foreach (IGameDataPersistence dataPersistenceObj in gameDataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }

        LoadSceneController.LoadScene("City");
    }

    private List<IGameDataPersistence> FindAllGameDataPersistenceObejcts()
    {
        IEnumerable<IGameDataPersistence> dataPersistenceObjects =
            FindObjectsOfType<MonoBehaviour>().OfType<IGameDataPersistence>();

        return new List<IGameDataPersistence>(dataPersistenceObjects);
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }
    #endregion

    public void SaveGame()
    {
        if(gameDataPersistenceObjects != null)
        {
            foreach (IGameDataPersistence dataPersistenceObj in gameDataPersistenceObjects)
            {
                dataPersistenceObj.SaveData(ref gameData);
            }
        }

        foreach (IGlobalDataPersistence dataPersistenceObj in globalDataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref globalData);
        }

        dataHandler.Save(gameData);
        dataHandler.Save(globalData);

    }

    public void DataClear(int nowSlot)
    {
        fileName = Path.Combine($"save{nowSlot}");
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        globalDataPersistenceObjects = FindAllGlobalDataPersistenceObejcts();
        dataHandler.DeleteSave();

        globalData.existSaveFile[nowSlot] = false;
        globalData.SaveSlotName[nowSlot] = "비어있음";
        globalData.SaveSlotPlayTime[nowSlot] = 0;
        dataHandler.Save(globalData);

        dataHandler = new FileDataHandler(Application.persistentDataPath, "GlobalData");
        globalData = dataHandler.DataLoad<GlobalData>();

        foreach (IGlobalDataPersistence dataPersistenceObj in globalDataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(globalData);
        }
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}

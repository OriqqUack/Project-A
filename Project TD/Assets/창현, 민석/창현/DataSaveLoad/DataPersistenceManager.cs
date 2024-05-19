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

        if (instance == null)
        {
            GameObject go = GameObject.Find("@DataManager");
            if (go == null)
            {
                go = new GameObject { name = "@DataManager" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            instance = go.GetComponent<DataPersistenceManager>();

            instance.nowSlot = 4;
        }

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
    public void LoadGame()
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
        SaveGame();
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
        if (globalData.existSaveFile[nowSlot] == false)
            return;

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

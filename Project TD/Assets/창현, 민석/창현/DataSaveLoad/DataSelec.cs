using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;
using MoreMountains.Feedbacks;

public class DataSelec : MonoBehaviour, IGlobalDataPersistence
{
    public GameObject _create;
    public GameObject _deletePopup;
    public SaveSlot[] _slot;
    public TextMeshProUGUI _newPlayerName;

    bool[] _saveFile = new bool[3];

    private void Start()
    {
    }

    public void Slot(int number)
    {
        Debug.Log(DataPersistenceManager.Instance.nowSlot);
        if (DataPersistenceManager.Instance.nowSlot == number)
        {
            if (_saveFile[number])
            {
                Managers.Scene.LoadScene(Define.Scene.GridTestScene);
            }
            else
            {
                Create();
            }
        }
        DataPersistenceManager.Instance.nowSlot = number;
    }

    public void Create()
    {
        _create.gameObject.SetActive(true);
    }

    public void CreateName()
    {
        int slotNum = DataPersistenceManager.Instance.nowSlot;

        _slot[slotNum].SlotName = _newPlayerName.text;
        _slot[slotNum].PlayTime = 0;
        
        _newPlayerName.text = "";
        _saveFile[slotNum] = true;

        DataPersistenceManager.Instance.nowSlot = slotNum;

        DataPersistenceManager.Instance.LoadGame();
        DataPersistenceManager.Instance.SaveGameData();
        DataPersistenceManager.Instance.SaveGlobalData();

        _create.gameObject.SetActive(false);
    }

    public void ShowDeletePopup()
    {
        _deletePopup.SetActive(true);
    }

    public void LoadData(GlobalData data)
    {
        for (int i = 0; i < _slot.Length; i++)
        {
            _saveFile[i] = data.existSaveFile[i];
            _slot[i].SlotName = data.SaveSlotName[i];
            _slot[i].PlayTime = data.SaveSlotPlayTime[i];
        }
    }

    public void SaveData(ref GlobalData data)
    {
        for (int i = 0; i < _slot.Length; i++)
        {
            data.existSaveFile[i] = _saveFile[i];
            data.SaveSlotName[i] = _slot[i].SlotName;
            data.SaveSlotPlayTime[i] = _slot[i].PlayTime;
        }
    }
}

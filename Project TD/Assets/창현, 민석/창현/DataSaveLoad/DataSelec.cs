using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;

public class DataSelec : MonoBehaviour
{
    public GameObject _create;
    public SaveSlot[] _slot;
    public TextMeshProUGUI _newPlayerName;

    bool[] _saveFile = new bool[3];

    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            if (File.Exists(Path.Combine("save", i.ToString())))
            {
                _saveFile[i] = true;
                _slot[i].slotName = DataPersistenceManager.instance.GameData._name;
            }
            else
                _slot[i].slotName = "비어있음";
        }
    }

    public void Slot(int number)
    {
        if (_saveFile[number])
        {
            DataPersistenceManager.instance.LoadGame(number);
        }
        else
            Create();
    }

    public void Create()
    {
        _create.gameObject.SetActive(true);
    }

    public void CreateName()
    {
        _slot[DataController.instance.nowSlot].slotName = _newPlayerName.text;
        _slot[DataController.instance.nowSlot].playTime = 0;
        _newPlayerName.text = "";
        _saveFile[DataController.instance.nowSlot] = true;

        _create.gameObject.SetActive(false);
    }

    

}

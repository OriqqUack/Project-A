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
    public TextMeshProUGUI[] _slotText;
    public TextMeshProUGUI _newPlayerName;

    bool[] _saveFile = new bool[3];

    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            if (File.Exists(Path.Combine("save", DataPersistenceManager.instance.nowSlot.ToString())))
            {
                _saveFile[i] = true;
                DataPersistenceManager.instance.nowSlot = i;
                DataPersistenceManager.instance.LoadGame();
                _slotText[i].text = DataPersistenceManager.instance.GameData._name;
            }
            else
                _slotText[i].text = "비어있음";
        }
        DataController.instance.DataClear();
    }

    public void Slot(int number)
    {
        DataController.instance.nowSlot = number;

        if (_saveFile[number])
        {
            DataController.instance.LoadData();
            GoGame();
        }
        else
            Create();
    }

    public void Create()
    {
        _create.gameObject.SetActive(true);
    }

    public void GoGame()
    {
        if (!_saveFile[DataController.instance.nowSlot])
        {
            DataController.instance._playerData.name = _newPlayerName.text;
            DataController.instance.SaveData();
        }
        SceneManager.LoadScene(1);
    }
}

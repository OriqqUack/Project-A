using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���� �����͸� ���������� ��ũ��Ʈ
public class PlayerPrefsUtility : MonoBehaviour
{
    [ContextMenu("DeleteSaveData")]
    private void DeleteSaveData()
    {
        PlayerPrefs.DeleteAll();
    }
}

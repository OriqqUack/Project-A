using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//저장 데이터를 삭제시켜줄 스크립트
public class PlayerPrefsUtility : MonoBehaviour
{
    [ContextMenu("DeleteSaveData")]
    private void DeleteSaveData()
    {
        PlayerPrefs.DeleteAll();
    }
}

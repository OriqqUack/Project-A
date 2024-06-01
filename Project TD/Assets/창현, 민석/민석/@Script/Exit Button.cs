using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public void GameExit()
    {
        GameObject go = GameObject.Find("@Manager");
        
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            GameObject.Destroy(go);
            Application.Quit();
        #endif
    }
}

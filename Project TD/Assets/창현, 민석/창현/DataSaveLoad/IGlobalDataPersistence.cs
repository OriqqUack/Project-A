using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGlobalDataPersistence
{
    void LoadData(GlobalData data);
    void SaveData(ref GlobalData data);
}

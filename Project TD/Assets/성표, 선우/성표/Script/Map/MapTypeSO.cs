using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapType_", menuName = "Scriptable Objects/Map/Map Type")]
public class MapTypeSO : ScriptableObject
{
    public string mapTypeName;

    #region Header Map Type List;
    [Space(10)]
    [Header("MAP BASIE SETTINGS")]
    #endregion Header Map Type List;
    public MapType mapType;
    public GameObject prefab;

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilitie.ValidateCheckNullValue(this, nameof(prefab), prefab);
    }
#endif
    #endregion Validation
}

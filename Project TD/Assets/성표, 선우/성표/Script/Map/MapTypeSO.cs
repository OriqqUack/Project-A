using KSP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapType_", menuName = "Scriptable Objects/Map/Map Type")]
public class MapTypeSO : ScriptableObject
{
    public string mapTypeName;

    #region Header Map Type List;
    [Space(10)]
    [Header("∏  ≈∏¿‘")]
    #endregion Header Map Type List;
    public MapType mapType;

    #region Header Map Type List;
    [Space(10)]
    [Header("∏  ∑π∫ß")]
    #endregion Header Map Type List;
    public MapLevel mapLevel;

    #region Header Map Template
    [Space(10)]
    [Header("∏  «¡∏Æ∆’")]
    #endregion Header Map Template
    public List<GameObject> prefabs;

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilite.ValidateCheckEnumerableValues(this, nameof(prefabs), prefabs);
    }
#endif
    #endregion Validation
}

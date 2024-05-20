using KSP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapTypeLevel_", menuName = "Scriptable Objects/Map/Map Level")]
public class MapLevelSO : ScriptableObject
{
    public string levelName;

    #region Header Map Type List;
    [Space(10)]
    [Header("∏  ∑π∫ß")]
    #endregion Header Map Type List;
    public MapLevel mapLevel;

    #region Header Map Type
    [Space(10)]
    [Header("∏  ≈∏¿‘")]
    #endregion Header Map Type
    public List<MapTypeSO> list;

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilite.ValidateCheckEnumerableValues(this, nameof(list), list);
    }
#endif
    #endregion Validation
}

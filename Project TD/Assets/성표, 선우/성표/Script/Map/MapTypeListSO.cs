using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapTypeList_", menuName = "Scriptable Objects/Map/Map Type List")]
public class MapTypeListSO : ScriptableObject
{
    #region Header Map Type
    [Space(10)]
    [Header("레벨 리스트")]
    #endregion Header Map Type
    public List<MapLevelSO> list;

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilitie.ValidateCheckEnumerableValues(this, nameof(list), list);
    }
#endif
    #endregion Validation
}

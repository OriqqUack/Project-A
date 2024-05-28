using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        if(list.Count < Settings.maxMapDepth)
        {
            Debug.Log("Null Error : List count must be greater than the maximum map depth.Current count - " + list.Count + ", Max depth - " + Settings.maxMapDepth);
        }

        HelperUtilitie.ValidateCheckEnumerableValues(this, nameof(list), list);
    }
#endif
    #endregion Validation
}

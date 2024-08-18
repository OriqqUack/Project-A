using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileDetails_", menuName = "Scriptable Objects/Map/TileDetails")]
public class TileDetailSO : ScriptableObject
{
    #region Header MAP BAISE DETAILS
    [Space(10)]
    [Header("MAP BAISE DETAILS")]
    #endregion Header MAP BAISE DETAILS;
    public MapLevel mapLevel;

    #region Header MAP SPAWN SETTINGS
    [Space(10)]
    [Header("MAP SPAWN SETTINGS")]
    #endregion Header MAP SPAWN SETTINGS
    public string dissolveName = "_DissolveAmount";
    public float materializeTime;
    public Shader materializeShader;
    [ColorUsage(true, true)]
    public Color tileMaterializeColor;
    public Material tileStandardMaterial;
}

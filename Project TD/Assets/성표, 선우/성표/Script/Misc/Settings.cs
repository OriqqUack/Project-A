using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings
{
    #region Map Create Settings
    public const int maxMapDepth = 3; // Level = Depth
    public const int maxMapTileCount = 30;
    #endregion Map Create  Settings

    #region Random Appear Tile Time Range
    public const float randomAppearTileTime1 = 60f;
    public const float randomAppearTileTime2 = 80f;
    #endregion Random Appear Tile Time Range

    #region PLAYER ANIMATION PARAMETERS
    public static float baseSpeedForPlayerAnimatons = 10f;
    #endregion PLAYER ANIMATION PARAMETERS
}
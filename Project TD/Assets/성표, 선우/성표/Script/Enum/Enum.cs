using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MapType
{
    None,
    Rocket,
    spiritForest,
    deadForest,
    corruptedForest,
    indianTerritory,
    CarpenterForest
}

public enum MapLevel
{
    None,
    level_1,
    level_2,
    level_3
}

public enum GameState
{
    gameStarted,
    playinglobby,
    playingInGame,
    gameWon,
    gameLost,
    gamePaused,
    restartGame
}

public enum AimDirection
{
    front,
    left,
    right
}
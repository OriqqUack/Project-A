using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MapType
{
    None,
    Rocket,
    spirit_forest,
    polluted_forest,
    corrupted_forest,
    indian_territory,
    Carpenter_forest
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
    playingGame,
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
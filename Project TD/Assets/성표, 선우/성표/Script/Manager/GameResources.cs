using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameResources : MonoBehaviour
{
    private static GameResources instance;

    public static GameResources Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<GameResources>("GameResources");
            }
            return instance;
        }
    }

    #region Header Player
    [Space(10)]
    [Header("PLAYER")]
    #endregion Header Player
    public CurrentPlayerSO currentPlayer;
}


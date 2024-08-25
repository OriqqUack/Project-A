using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

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

    #region Header SOUNDS
    [Space(10)]
    [Header("SOUNDS")]
    #endregion Header SOUNDS
    public AudioMixerGroup soundsMasterMixerGroup;
}


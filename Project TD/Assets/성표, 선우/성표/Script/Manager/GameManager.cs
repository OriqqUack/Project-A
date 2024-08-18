using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.WSA;

[DisallowMultipleComponent]
public class GameManager : SingletonMonoBehaivour<GameManager>
{
    #region Header MAP LEVEL DISIGN
    [Space(10)]
    [Header("MAP LEVEL DISIGN")]
    #endregion Header MAP LEVEL DISIGN
    public List<MapLevelSO> mapLevels;

    private PlayerDetailsSO playerDetails;
    private Player player;
    
    [HideInInspector]public int currentTileIndex;
    [HideInInspector] public GameState gameState;
    [HideInInspector] public GameState previousGameState;
    [HideInInspector] public float inGameTimer;

    protected override void Awake()
    {
        base.Awake();

        playerDetails = GameResources.Instance.currentPlayer.playerDetails;

        InstantiatePlayer();
    }

    private void Update()
    {
        HandleGameState();

    }

    private void InstantiatePlayer()
    {
        // Instatiate player
        GameObject playerGameobject = Instantiate(playerDetails.playerPrefab);

        // Initialize player
        player = playerGameobject.GetComponent<Player>();

        player.Initalize(playerDetails);
    }

    private void HandleGameState()
    {
        //Handle game state
        switch (gameState)
        {
            case GameState.gameStarted:
                StartInGameProcess();
                break;

            case GameState.playingInGame:
                break;

            case GameState.gameWon:

                if (previousGameState != GameState.gameWon)
                    StartCoroutine(GameWon());

                break;

            case GameState.gameLost:

                if (previousGameState != GameState.gameLost)
                {
                    StopAllCoroutines();
                    StartCoroutine(GameLost());
                }

                break;

            case GameState.restartGame:

                RestartGame();

                break;
        }
    }

    private void StartInGameProcess()
    {
        InitalizeInGame();
        
        GenerateMap();

        StartCoroutine(InGameTileControl());

        gameState = GameState.playingInGame;
    }

    private void InitalizeInGame()
    {
        inGameTimer = 0f;
    }

    private void GenerateMap()
    {
        bool SucessfulMapGenerate = MapBuilder.Instance.GenerateMap(mapLevels);

        currentTileIndex = 0;

        if (!SucessfulMapGenerate)
        {
            Debug.Log("error : ¸Ê »ý¼º ¿À·ù");
        }

        player.gameObject.transform.position = Vector3.zero;
    }

    private IEnumerator InGameTileControl()
    {
        float tileTimer = 0f
            ;
        while (MapBuilder.Instance.tileObjects[currentTileIndex] != null)
        {
            tileTimer -= Time.deltaTime;

            #region Map create test code 
            if (Input.GetKeyDown(KeyCode.P))
            {
                tileTimer = 0;
            }

            #endregion Map create test code 

            if (tileTimer <= 0f)
            {
                MapBuilder.Instance.tileObjects[currentTileIndex].SetActive(true);
                currentTileIndex++;
                tileTimer = UnityEngine.Random.Range(Settings.randomAppearTileTime1, Settings.randomAppearTileTime2);
            }

            yield return null;
        }
    }

    public Player ReturnPlayer()
    {
        return player;
    }

    public Tile GetTile(int index)
    {
        return MapBuilder.Instance.tileObjects[index].GetComponent<Tile>();
    }

    private IEnumerator GameWon()
    {
        previousGameState = GameState.gameWon;

        Debug.Log("Game Won! - Game will restart in 10 seconds");

        yield return new WaitForSeconds(10f);

        gameState = GameState.restartGame;
    }

    private IEnumerator GameLost()
    {
        previousGameState = GameState.gameLost;

        Debug.Log("Game Lost - Bad Luck!. Game will retart in 10 seconds");

        yield return new WaitForSeconds(10f);

        gameState = GameState.restartGame;
    }

    private void RestartGame()
    {
        SceneManager.LoadScene("MainGameScene");
    }

}

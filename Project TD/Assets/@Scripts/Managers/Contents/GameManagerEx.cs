using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManagerEx : SingletonMonoBehaivour<GameManagerEx>
{
    #region GameManagerEX
    GameObject _player;
    int _gold;
    //Dictionary<int, GameObject> _players = new Dictionary<int, GameObject>();
    HashSet<GameObject> _monsters = new HashSet<GameObject>();
    public GameObject _currentTower { get; set; }

    public Action<int> OnSpawnEvent;

    public GameObject GetPlayer() { return _player; }
    public GameObject GetTower() { return _currentTower; }

    public void GetGold(int gold)
    {
        if(_gold+gold>10000)
            return;
        _gold += gold;
    }

    public void SpendGold(int gold)
    {
        if (_gold - gold < 0)
        {
            Debug.Log("너무 비쌉니다"); // 고쳐야함
            return;
        }
        _gold -= gold;
    }

    public GameObject Spawn(Define.WorldObject type, string path, Transform parent = null)
    {
        GameObject go = Managers.Resource.Instantiate(path, parent);

        switch (type)
        {
            case Define.WorldObject.Monster:
                _monsters.Add(go);
                if (OnSpawnEvent != null)
                    OnSpawnEvent.Invoke(1);
                break;
            case Define.WorldObject.Player:
                _player = go;
                break;
            case Define.WorldObject.Tower:
                _currentTower = go;
                break;
        }
        return go;
    }

    public GameObject Spawn(Define.WorldObject type, string path, Vector3 position, Quaternion quaternion, Transform parent = null)
    {
        GameObject go = Managers.Resource.Instantiate(path, parent);

        switch (type)
        {
            case Define.WorldObject.Monster:
                _monsters.Add(go);
                if (OnSpawnEvent != null)
                    OnSpawnEvent.Invoke(1);
                break;
            case Define.WorldObject.Player:
                _player = go;
                break;
            case Define.WorldObject.Tower:
                _currentTower = go;
                break;
        }
        return go;
    }

    public Define.WorldObject GetWorldObjectType(GameObject go)
    {
        BaseController bc = go.GetComponent<BaseController>();
        if (bc == null)
            return Define.WorldObject.Unknown;

        return bc.WorldObjectType;
    }

    public void Despawn(GameObject go)
    {
        Define.WorldObject type = GetWorldObjectType(go);

        switch (type)
        {
            case Define.WorldObject.Monster:
                {
                    if (_monsters.Contains(go))
                    {
                        _monsters.Remove(go);
                        if (OnSpawnEvent != null)
							OnSpawnEvent.Invoke(-1);
					}   
                }
                break;
            case Define.WorldObject.Player:
                {
					if (_player == go)
						_player = null;
				}
                break;
        }

        Managers.Resource.Destroy(go);
    }
    #endregion GameManagerEX

    [HideInInspector] public GameState gameState;
    [HideInInspector] public float inGameTimer;
    private PlayerDetailsSO playerDetails;
    private Player player;
    private float tileTimer;

    private int currentTileIndex=0;

    private void Update()
    {
        HandleGameState();
        if (Input.GetKeyDown(KeyCode.R))
        {
            tileTimer = 0f;
        }

    }

    protected override void Awake()
    {
        base.Awake();
        
        playerDetails = GameResources.Instance.currentPlayer.playerDetails;

        InstantiatePlayer();

    }

    private void InstantiatePlayer()
    {
        // Instatiate player
        GameObject playerGameobject = Instantiate(playerDetails.playerPrefab);

        // Initialize player
        player = playerGameobject.GetComponent<Player>();

        player.Initalize(playerDetails);
    }

    private void Start()
    {
        gameState = GameState.gameStarted;
    }

    private void HandleGameState()
    {
        //Handle game state
        switch (gameState)
        {
            case GameState.gameStarted:
                PlayInGame();
                inGameTimer = 0f;
                gameState = GameState.playingGame;
                break;

            case GameState.playingGame:
                OnInGameTimer();
                InGameTileControl();
                break;
        }
    }

    private void PlayInGame()
    {
        bool SucessfulMapGenerate = MapBuilder.Instance.GenerateMap();

        if (!SucessfulMapGenerate)
        {
            Debug.Log("error : 맵 생성 오류");
        }

        player.gameObject.transform.position = Vector3.zero; //Vector3 -> 로켓앞으로 위치 변경 예정
    }

    private void OnInGameTimer()
    {
        inGameTimer += Time.deltaTime;
    }

    // 이벤트 메서드로 수정예정
    private void InGameTileControl()
    {
        if (currentTileIndex <= Settings.maxMapTileCount)
        {
            tileTimer -= Time.deltaTime;
        }

        if (tileTimer < 0f)
        {
            MapBuilder.Instance.tileObjects[currentTileIndex++].SetActive(true);

            tileTimer = UnityEngine.Random.Range(Settings.randomAppearTileTime1, Settings.randomAppearTileTime2);
        }
        
    }

    public Player ReturnPlayer()
    {
        return player;
    }

    
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;



[DisallowMultipleComponent]
public class MapBuilder : SingletonMonoBehaivour<MapBuilder>
{
    private List<Vector3> mapTilePostion;
    public Tilemap tilemap;
    public MapTypeListSO mapTypeList; //추후 변경
    private bool mapCreateSucessful;

    [HideInInspector] public Dictionary<int, List<Vector3>> mapTileDictionary = new Dictionary<int, List<Vector3>>();
    [HideInInspector] public List<GameObject> tileObjects = new List<GameObject>();

    protected override void Awake()
    {
        base.Awake();

        // 딕셔너리 초기화 및 메모리 할당 
        InitalizeTileDictionary();
    }


    /// <summary>
    /// 맵 타일을 저장하는 딕셔너리 초기화(메모리 할당)
    /// </summary>
    private void InitalizeTileDictionary()
    {

        mapTileDictionary.Clear();

        for (int i = 0; i <= Settings.maxMapDepth; i++)
        {
            if (!mapTileDictionary.ContainsKey(i))
            {
                mapTileDictionary[i] = new List<Vector3>();
            }
        }
    }

    /// <summary>
    /// GameResouce로 부터 맵 타입 리스트를 로드함
    /// </summary>
    private bool LoadMapTypeList()
    {
        mapTypeList = GameResources.Instance.mapTypeList;

        if (mapTypeList == null)
        {
            Debug.Log("GameResource map type list load fail");
            return false;
        }

        return true;
    }

    /// <summary>
    /// 맵 생성
    /// </summary>
    public bool GenerateMap()
    {
        // 맵 타일 위치리스트 생성
        CreateTilePostionList();

        // 맵 타일 프리팹 생성
        InstantiateTile();

        return mapCreateSucessful;
    }

    private void CreateTilePostionList()
    {

        int tileCount = 0;
        int currentDepth = 1000; // 최초 깊이 NULL
        Vector3Int startTile = Vector3Int.zero; // Vector3(0,0,0)

        // 타일의 Y 좌표값이 짝수일 경우
        List<Vector3Int> evenDepthDirections = new List<Vector3Int>
                {
                     new Vector3Int(-1, 0, 0), new Vector3Int(-1, 1, 0), new Vector3Int(0, 1, 0),
                     new Vector3Int(1, 0, 0), new Vector3Int(0, -1, 0), new Vector3Int(-1, -1, 0)
                };
        // 타일의 Y 좌표값이 홀수일 경우
        List<Vector3Int> oddDepthDirections = new List<Vector3Int>
                {
                     new Vector3Int(-1, 0, 0), new Vector3Int(0, 1, 0), new Vector3Int(1, 1, 0),
                     new Vector3Int(1, 0, 0), new Vector3Int(1, -1, 0), new Vector3Int(0, -1, 0)
                };

        //BFS
        Queue<(Vector3Int, int)> queue = new Queue<(Vector3Int, int)>();
        HashSet<Vector3Int> visit = new HashSet<Vector3Int>();

        queue.Clear();

        // queue에 타일맵의 좌표와 깊이 저장 
        queue.Enqueue((startTile, 0));
        visit.Add(startTile);

        while (queue.Count > 0)
        {
            var (current, depth) = queue.Dequeue();
            // 구한 좌표값과 BFS깊이를 딕셔너리에 저장
            AddTilePositionDictionary(current, depth);
            tileCount++;

            // 타일 개수 제한
            if (!(tileCount < Settings.maxMapTileCount)) return;

            if (currentDepth != depth)
            {
                currentDepth = depth;
                HelperUtilitie.Suffle(oddDepthDirections);
                HelperUtilitie.Suffle(evenDepthDirections);
            }

            // BFS를 실행는 타일의 Y좌표가 짝수일 경우
            if (current.y % 2 == 0)
            {
                foreach (Vector3Int direction in evenDepthDirections)
                {
                    Vector3Int neigbor = current + direction;
                    int newDepth = depth + 1;
                    if (!visit.Contains(neigbor) && newDepth <= Settings.maxMapDepth)
                    {
                        queue.Enqueue((neigbor, newDepth));
                        visit.Add(neigbor);
                    }
                }
            }
            // BFS를 실행는 타일의 Y좌표가 홀수일 경우
            else
            {
                foreach (Vector3Int direction in oddDepthDirections)
                {
                    Vector3Int neigbor = current + direction;
                    int newDepth = depth + 1;
                    if (!visit.Contains(neigbor) && newDepth <= Settings.maxMapDepth)
                    {
                        queue.Enqueue((neigbor, newDepth));
                        visit.Add(neigbor);
                    }
                }
            }
        }

    }

    /// <summary>
    /// 맵 타일 생성
    /// </summary>
    private void InstantiateTile()
    {
        int listCount = 0;

        for (int depth = 0; depth <= Settings.maxMapDepth; depth++)
        {
            if (!mapTileDictionary.ContainsKey(depth))
            {
                mapCreateSucessful = false;
                return;
            }

            foreach (Vector3 tilePosition in mapTileDictionary[depth])
            {
                if (mapTypeList.list[depth] == null)
                {
                    Debug.Log("tile map prefabs null error");
                    mapCreateSucessful = false;
                    return;
                }
                MapTypeSO mapType = mapTypeList.list[depth].list[Random.Range(0, mapTypeList.list[depth].list.Count)];
                tileObjects.Add(Instantiate(mapType.prefabs[Random.Range(0, mapType.prefabs.Count)]
                    , tilePosition, Quaternion.identity));

                tileObjects[listCount++].SetActive(false);
            }
        }

        mapCreateSucessful = true;
        return;
    }

    /// <summary>
    /// 타일맵의 좌표를 월드 좌표로 반환
    /// </summary>
    /// <param name="tilePosition"></param>
    /// <returns></returns>
    private Vector3 GetTileWorldPostion(Vector3Int tilePosition)
    {
        Vector3 worldPosition = tilemap.GetCellCenterWorld(tilePosition);
        return worldPosition;
    }

    /// <summary>
    /// 딕셔너리에 값 저장
    /// </summary>
    /// <param name="tilePostion"></param>
    /// <param name="depth"></param>
    private void AddTilePositionDictionary(Vector3Int tilePostion, int depth)
    {
        mapTileDictionary[depth].Add(GetTileWorldPostion(tilePostion));
    }
}


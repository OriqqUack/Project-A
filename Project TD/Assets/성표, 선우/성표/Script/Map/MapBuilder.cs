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
    public MapTypeListSO mapTypeList; //���� ����
    private bool mapCreateSucessful;

    [HideInInspector] public Dictionary<int, List<Vector3>> mapTileDictionary = new Dictionary<int, List<Vector3>>();
    [HideInInspector] public List<GameObject> tileObjects = new List<GameObject>();

    protected override void Awake()
    {
        base.Awake();

        // ��ųʸ� �ʱ�ȭ �� �޸� �Ҵ� 
        InitalizeTileDictionary();
    }


    /// <summary>
    /// �� Ÿ���� �����ϴ� ��ųʸ� �ʱ�ȭ(�޸� �Ҵ�)
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
    /// GameResouce�� ���� �� Ÿ�� ����Ʈ�� �ε���
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
    /// �� ����
    /// </summary>
    public bool GenerateMap()
    {
        // �� Ÿ�� ��ġ����Ʈ ����
        CreateTilePostionList();

        // �� Ÿ�� ������ ����
        InstantiateTile();

        return mapCreateSucessful;
    }

    private void CreateTilePostionList()
    {

        int tileCount = 0;
        int currentDepth = 1000; // ���� ���� NULL
        Vector3Int startTile = Vector3Int.zero; // Vector3(0,0,0)

        // Ÿ���� Y ��ǥ���� ¦���� ���
        List<Vector3Int> evenDepthDirections = new List<Vector3Int>
                {
                     new Vector3Int(-1, 0, 0), new Vector3Int(-1, 1, 0), new Vector3Int(0, 1, 0),
                     new Vector3Int(1, 0, 0), new Vector3Int(0, -1, 0), new Vector3Int(-1, -1, 0)
                };
        // Ÿ���� Y ��ǥ���� Ȧ���� ���
        List<Vector3Int> oddDepthDirections = new List<Vector3Int>
                {
                     new Vector3Int(-1, 0, 0), new Vector3Int(0, 1, 0), new Vector3Int(1, 1, 0),
                     new Vector3Int(1, 0, 0), new Vector3Int(1, -1, 0), new Vector3Int(0, -1, 0)
                };

        //BFS
        Queue<(Vector3Int, int)> queue = new Queue<(Vector3Int, int)>();
        HashSet<Vector3Int> visit = new HashSet<Vector3Int>();

        queue.Clear();

        // queue�� Ÿ�ϸ��� ��ǥ�� ���� ���� 
        queue.Enqueue((startTile, 0));
        visit.Add(startTile);

        while (queue.Count > 0)
        {
            var (current, depth) = queue.Dequeue();
            // ���� ��ǥ���� BFS���̸� ��ųʸ��� ����
            AddTilePositionDictionary(current, depth);
            tileCount++;

            // Ÿ�� ���� ����
            if (!(tileCount < Settings.maxMapTileCount)) return;

            if (currentDepth != depth)
            {
                currentDepth = depth;
                HelperUtilitie.Suffle(oddDepthDirections);
                HelperUtilitie.Suffle(evenDepthDirections);
            }

            // BFS�� ����� Ÿ���� Y��ǥ�� ¦���� ���
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
            // BFS�� ����� Ÿ���� Y��ǥ�� Ȧ���� ���
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
    /// �� Ÿ�� ����
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
    /// Ÿ�ϸ��� ��ǥ�� ���� ��ǥ�� ��ȯ
    /// </summary>
    /// <param name="tilePosition"></param>
    /// <returns></returns>
    private Vector3 GetTileWorldPostion(Vector3Int tilePosition)
    {
        Vector3 worldPosition = tilemap.GetCellCenterWorld(tilePosition);
        return worldPosition;
    }

    /// <summary>
    /// ��ųʸ��� �� ����
    /// </summary>
    /// <param name="tilePostion"></param>
    /// <param name="depth"></param>
    private void AddTilePositionDictionary(Vector3Int tilePostion, int depth)
    {
        mapTileDictionary[depth].Add(GetTileWorldPostion(tilePostion));
    }
}


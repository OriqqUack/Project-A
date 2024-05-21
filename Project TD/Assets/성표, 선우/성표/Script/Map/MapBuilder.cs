using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

namespace KSP
{

    [DisallowMultipleComponent]
    public class MapBuilder : SingletonMonoBehaivour<MapBuilder>
    {
        public List<Vector3> mapTilePostion;
        public Tilemap tilemap;
        private bool mapCreateSucessful;

        [HideInInspector] public Dictionary<int, List<Vector3>> mapTileDictionary = new Dictionary<int, List<Vector3>>();
        [HideInInspector] public List<GameObject> tileObjects = new List<GameObject>();
        
        //GameResource에서 가져오도록 추후 변경 예정
        public MapTypeListSO mapTypeList;

        protected override void Awake()
        {
            base.Awake();
            Initalize();
            //LoadMapTypeList();

            GenerateMap();
        }

        private void Initalize()
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

        /* GameResource에서 로드하도록 추후 확장 예정
        private void LoadMapTypeList()
        {
            mapTypeList = GameResources.Instance.mapTypeList;
        }
        */

        public void GenerateMap()
        {
            //mapCreateSucessful = false;

            CreateTilePostionList();

            InstantiateTile();
        }

        private void CreateTilePostionList()
        {

            int tileCount = 0;
            int currentDepth = 0;
            Vector3Int startTile = Vector3Int.zero;
            List<Vector3Int> evenDepthDirections = new List<Vector3Int>
                {
                     new Vector3Int(-1, 0, 0), new Vector3Int(-1, 1, 0), new Vector3Int(0, 1, 0),
                     new Vector3Int(1, 0, 0), new Vector3Int(0, -1, 0), new Vector3Int(-1, -1, 0)
                };

            List<Vector3Int> oddDepthDirections = new List<Vector3Int>
                {
                     new Vector3Int(-1, 0, 0), new Vector3Int(0, 1, 0), new Vector3Int(1, 1, 0),
                     new Vector3Int(1, 0, 0), new Vector3Int(1, -1, 0), new Vector3Int(0, -1, 0)
                };

            HelperUtilite.Suffle(oddDepthDirections);
            HelperUtilite.Suffle(evenDepthDirections);

            //BFS
            Queue<(Vector3Int, int)> queue = new Queue<(Vector3Int, int)>();
            HashSet<Vector3Int> visit = new HashSet<Vector3Int>();

            queue.Clear();

            queue.Enqueue((startTile, 0));
            visit.Add(startTile);

            while (queue.Count > 0)
            {
                var (current, depth) = queue.Dequeue();
                AddTilePositionDictionary(current, depth);
                tileCount++;

                if (!(tileCount < Settings.maxMapTileCount))
                {
                    return;
                }

                if (currentDepth != depth)
                {
                    currentDepth = depth;
                    HelperUtilite.Suffle(oddDepthDirections);
                    HelperUtilite.Suffle(evenDepthDirections);
                }

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
                    return;
                }

                foreach (Vector3 tilePosition in mapTileDictionary[depth])
                {
                    if (mapTypeList.list[depth] == null)
                    {
                        Debug.Log("tile map prefabs null");
                        return;
                    }
                    MapTypeSO mapType = mapTypeList.list[depth].list[Random.Range(0, mapTypeList.list[depth].list.Count)];
                    tileObjects.Add(Instantiate(mapType.prefabs[Random.Range(0, mapType.prefabs.Count)]
                        , tilePosition, Quaternion.identity));
                    
                    tileObjects[listCount++].SetActive(false);
                }
            }
        }

        private Vector3 GetTileWorldPostion(Vector3Int tilePosition)
        {
            Vector3 worldPosition = tilemap.GetCellCenterWorld(tilePosition);
            return worldPosition;
        }


        private int GetTileDepth(Vector3Int tilePostion)
        {
            float distance = Vector3.Distance(Vector3.zero, tilePostion);

            for (int depth = 0; depth <= Settings.maxMapDepth; depth++)
            {
                if (depth <= distance && distance < depth + 1)
                {
                    return depth;
                }
            }

            Debug.Log("unexist depth");

            return Settings.maxMapDepth;
        }

        private void AddTilePositionDictionary(Vector3Int tilePostion, int depth)
        {
            mapTileDictionary[depth].Add(GetTileWorldPostion(tilePostion));
        }
    }
}

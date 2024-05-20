using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

namespace KSP {

    [DisallowMultipleComponent]
    public class MapBuilder : SingletonMonoBehaivour<MapBuilder>
    {
        public List<Vector3> mapTilePostion;
        public Tilemap tilemap;
        private bool mapCreateSucessful;
        
        [HideInInspector]public Dictionary<int, List<Vector3>> mapTileDictionary = new Dictionary<int, List<Vector3>>();
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

            for (int i = 0; i < Settings.maxMapDepth; i++)
            {
                if (!mapTileDictionary.ContainsKey(i))
                {
                    mapTileDictionary[i] = new List<Vector3>();
                }
            }
        }

        /*
        private void LoadMapTypeList()
        {
            mapTypeList = GameResources.Instance.mapTypeList;
        }
        */

        public bool GenerateMap()
        {
            mapCreateSucessful = false;

            CreateTilePostionList();

            InstantiateTile();

            return mapCreateSucessful;
        }

        private void CreateTilePostionList()
        {
            Vector3Int startTile = Vector3Int.zero;
            int tileCount = 0;
            Vector3Int[] directions =
    {
                 new Vector3Int(+1, -1, 0), new Vector3Int(0, -1, 0), new Vector3Int(-1, 0, 0),
                 new Vector3Int(-1, +1, 0), new Vector3Int(0, +1, 0), new Vector3Int(+1, 0, 0)
            };

            Queue<(Vector3Int, int)> queue = new Queue<(Vector3Int, int)>();
            HashSet<Vector3Int> visit = new HashSet<Vector3Int>();

            queue.Clear();

            queue.Enqueue((startTile,0));
            visit.Add(startTile);

            while (queue.Count > 0)
            {
                var (current, depth) = queue.Dequeue();
                CategorizationMapDepthInList(current);
                tileCount++;

                foreach (Vector3Int direction in directions)
                {
                    Vector3Int neigbor = current + direction;
                    int newDepth = depth + 1;
                    if (!visit.Contains(neigbor) && Vector3Int.Distance(startTile, neigbor) < Settings.maxMapDepth + 1)
                    {
                        queue.Enqueue((neigbor, newDepth));
                        visit.Add(neigbor);
                    }
                }
            }
            
        }

        private void InstantiateTile()
        {
            int tileCount = 0;

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
                    GameObject tileObject = Instantiate(mapType.prefabs[Random.Range(0,mapType.prefabs.Count)]
                        ,tilePosition, Quaternion.identity);

                    tileCount++;

                    if (Settings.maxMapTileCount < tileCount)
                    {
                        return;
                    }
                }
            }
        }

        private Vector3 GetTileWorldPostion(Vector3Int tilePosition)
        {
            Vector3 worldPosition = tilemap.GetCellCenterWorld(tilePosition);
            return worldPosition;
        }


        private void CategorizationMapDepthInList(Vector3Int tilePostion)
        {
            float distance = Vector3.Distance(Vector3.zero, tilePostion);

            for (int i = 0; i <= Settings.maxMapDepth; i++)
            {
                float checkToDistanceA = Vector3.Distance(Vector3.zero, GetTileWorldPostion(new Vector3Int(i, 0)));
                float checkToDistanceB = Vector3.Distance(Vector3.zero, GetTileWorldPostion(new Vector3Int(i + 1, 0)));

                if( checkToDistanceA <= distance && distance < checkToDistanceB)
                {
                    mapTileDictionary[i].Add(GetTileWorldPostion(tilePostion));

                }
            }
        }
    }
}

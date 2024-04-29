using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Map
{
    public class IslandGeneration : MonoBehaviour
    {
        [SerializeField] GameObject[] _islandPrefab;
        [SerializeField] IslandPoint[] _islandPoint;

        TileGeneration tileGeneration;

        GameObject[] _islandPrefabs;

        private void Start()
        {
            init();
        }

        void init()
        {
            tileGeneration = GetComponent<TileGeneration>();
            tileGeneration.Shuffle(_islandPrefab);
            _islandPrefabs = new GameObject[_islandPoint.Length];
            CreateIsland();
        }
        
        void CreateIsland()
        {
            for (int i = 0; i < _islandPoint.Length; i++)
            {
                Vector3 position = _islandPoint[i].transform.position;
                Quaternion rotation = _islandPoint[i].transform.rotation;
                _islandPrefabs[i] = Instantiate(_islandPrefab[i], position, rotation);
                Debug.Log("123");
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

namespace Map
{
    public class TileGeneration : MonoBehaviour
    {
        [SerializeField] GameObject[] _tilePrefab;

        GameObject[] _tilePrefabs;
        List<TilePoint> _tilePoint = new List<TilePoint>();

        private void Start()
        {
            init();
        }

        void init()
        {
            FindTilePoint();
            Shuffle(_tilePrefab);
            _tilePrefabs = new GameObject[_tilePoint.Count];
            CreateTile();
        }

        void FindTilePoint()
        {
            _tilePoint.Clear();
            Transform[] childTileTransforms = new Transform[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                childTileTransforms[i] = transform.GetChild(i);
            }
            foreach (Transform transform in childTileTransforms)
            {
                TilePoint tilePoint = transform.GetComponent<TilePoint>();
                if (tilePoint != null)
                {
                    _tilePoint.Add(tilePoint);
                }
            }
        }

        void CreateTile()
        {
            for (int i = 0; i < _tilePoint.Count; i++)
            {
                Vector3 position = _tilePoint[i].transform.position;
                Quaternion rotation = _tilePoint[i].transform.rotation;
                _tilePrefabs[i] = Instantiate(_tilePrefab[i], position, rotation);
            }
        }

        public void Shuffle<T>(T[] array)
        {
            int n = array.Length;
            while (n > 1)
            {
                n--;
                int k = UnityEngine.Random.Range(0, n + 1);
                T value = array[k];
                array[k] = array[n];
                array[n] = value;
            }
        }
    }
}

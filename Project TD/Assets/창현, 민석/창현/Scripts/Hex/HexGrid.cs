using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class HexGrid : MonoBehaviour
    {
        private void Start()
        {
            foreach (Hex hex in FindObjectsOfType<Hex>())
            {
                Managers.Build.hexTileDict[hex.HexCoords] = hex;
            }
        }

    }

    public static class Direction
    {
        public static List<Vector3Int> directionsOffsetOdd = new List<Vector3Int>
    {
        new Vector3Int(-1,0,1), //N1
        new Vector3Int(0,0,1),  //N2
        new Vector3Int(1,0,0),  //E
        new Vector3Int(0,0,-1), //S2
        new Vector3Int(-1,0,-1),//S1
        new Vector3Int(-1,0,0), //W
    }; // È¦¼ö

        public static List<Vector3Int> directionsOffsetEven = new List<Vector3Int>
    {
        new Vector3Int(0,0,1), //N1
        new Vector3Int(1,0,1), //N2
        new Vector3Int(1,0,0), //E
        new Vector3Int(1,0,-1),//S2
        new Vector3Int(0,0,-1),//S1
        new Vector3Int(-1,0,0),//W
    }; //Â¦¼ö

        public static List<Vector3Int> GetDirectionList(int z)
                => z % 2 == 0 ? directionsOffsetEven : directionsOffsetOdd;
    }



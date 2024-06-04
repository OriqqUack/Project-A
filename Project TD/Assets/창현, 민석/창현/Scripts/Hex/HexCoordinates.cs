using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCoordinates : MonoBehaviour
{
    public float xOffset, yOffset, zOffset;
    int groundOffset = 10000;

    internal Vector3Int GetHexCoords()
        => offsetCoordinates;

    [Header("Offset coordinates")]
    [SerializeField]
    private Vector3Int offsetCoordinates;

    private void Awake()
    {
        offsetCoordinates = ConvertPositionToOffset(transform.position);   
    }

    private Vector3Int ConvertPositionToOffset(Vector3 position)
    {
        int x = Mathf.CeilToInt(position.x / xOffset);
        int y = Mathf.RoundToInt(position.y / yOffset);
        int z = Mathf.RoundToInt(position.z / zOffset);

        if (this.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            x += groundOffset;
            y += groundOffset;
            z += groundOffset;
        }
        
        return new Vector3Int(x, y, z);
    }
}

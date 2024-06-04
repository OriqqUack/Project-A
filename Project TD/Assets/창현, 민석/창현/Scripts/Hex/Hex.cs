using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[SelectionBase] //클릭시 먼저 선택됨
public class Hex : MonoBehaviour
{
    /*[SerializeField]
    private GlowHighlight highlight;*/
    private HexCoordinates hexCoordinates;
    protected GameObject turret;

    public Vector3 positionOffset;
    protected bool isUpgraded = false;

    public Vector3Int HexCoords => hexCoordinates.GetHexCoords();

    public List<Vector3Int> neighbours = new List<Vector3Int>();

    public Define.Tile _tile;
    private void Awake()
    {
        hexCoordinates = GetComponent<HexCoordinates>();
    }

    private void Start()
    {
        neighbours = BuildManager.Instance.GetNeighboursFor(HexCoords);
    }

    #region Highlight
   /* public void EnableHighlight()
    {
        highlight.ToggleGlow(true);
    }

    public void DisableHighlight()
    {
        highlight.ToggleGlow(false);
    }*/
    #endregion

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    public bool CheckValidPlace()
    {
        if (turret != null)
            return false;
        return true;
    }
}


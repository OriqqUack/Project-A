using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[SelectionBase] //클릭시 먼저 선택됨
public class Hex : MonoBehaviour
{
    [SerializeField]
    private GlowHighlight highlight;
    private HexCoordinates hexCoordinates;
    private Vector3 offsetPosition = new Vector3(0, 20, 0);
    private GameObject Wall;

    public Vector3 positionOffset;
    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public bool isUpgraded = false;

    public HexGrid hexGrid;

    public Vector3Int HexCoords => hexCoordinates.GetHexCoords();

    public List<Vector3Int> neighbours = new List<Vector3Int>();

    private Transform TowerParentTransform;
    private Transform WallParentTransform;
    private void Awake()
    {
        hexCoordinates = GetComponent<HexCoordinates>();
        highlight = GetComponent<GlowHighlight>();
    }

    private void Start()
    {
        TowerParentTransform = Managers.Build.TowerParent.GetComponent<Transform>();
        WallParentTransform = Managers.Build.WallParent.GetComponent<Transform>();
    }

    public void EnableHighlight()
    {
        highlight.ToggleGlow(true);
    }

    public void DisableHighlight()
    {
        highlight.ToggleGlow(false);
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) //ui가 누르면 true
            return;

        if (Wall != null)
            Managers.Resource.Destroy(Wall);

        DisableHighlight();

        if(Managers.Build._currentHex != null)
        {
            foreach (Vector3Int neighbour in Managers.Build._currentHex.neighbours)
            {
                Managers.Build.GetTileAt(neighbour).DisableHighlight();
            }
        }
        
        neighbours = Managers.Build.GetNeighboursFor(HexCoords);

        foreach (Vector3Int neighbour in neighbours)
        {
            Managers.Build.GetTileAt(neighbour).EnableHighlight();
        }

        Managers.Build._currentHex = this;

        if (turret != null)
        {
            Managers.Build.SelectNode(this);
            return;
        }

        if (!Managers.Build.CanBuild)
            return;

        BuildTurret(Managers.Build.GetTurretToBuild());
    }

    private void OnMouseEnter()
    {
        Managers.Build.ghostTowerObject.transform.position = GetBuildPosition();
        
        foreach(MeshRenderer meshRenderer in Managers.Build.ghostTower._meshRender)
        {
            meshRenderer.sharedMaterial = turret!=null ? Managers.Build.ghostTower._valid : Managers.Build.ghostTower._inValid;
        }
    }

    private void OnMouseExit()
    {

    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    public void BuildTurret(TurretBlueprint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("Not enough money to build that!");
            return;
        }

        PlayerStats.Money -= blueprint.cost;

        GameObject _turret = Managers.Resource.Instantiate(blueprint.prefab, GetBuildPosition()+offsetPosition, Quaternion.identity, TowerParentTransform);
        turret = _turret;

        //GameObject effect = (GameObject)Instantiate(Managers.Build.buildEffect, GetBuildPosition(), Quaternion.identity);
        //Destroy(effect, 5f);

        foreach (Vector3Int neighbour in Managers.Build._currentHex.neighbours)
        {
            Managers.Build.GetTileAt(neighbour).BuildWall(Managers.Build.GetWallToBuild());
        }

        Debug.Log("Turret build!");
        Managers.Build.ClearTurret();
        
    }

    public void BuildWall(WallBlueprint blueprint) //WallBluePrint를 나중에 파라미터로 받아야 할 수도 있음
    {
        if (!CheckValidPlace())
            return;
        Wall = Managers.Resource.Instantiate(blueprint.prefab, GetBuildPosition() + offsetPosition, Quaternion.identity, WallParentTransform);
    }

    private bool CheckValidPlace()
    {
        if (Wall != null || turret != null)
            return false;
        return true;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class HexGrid : Hex
{
    private void OnMouseDown()
    {
        if (!BuildManager.Instance.canBuild)
            return;

        if (EventSystem.current.IsPointerOverGameObject()) //ui가 누르면 true
            return;

        #region Highligh
        /*DisableHighlight();

        if (BuildManager.Instance._currentHex != null)
        {
            foreach (Vector3Int neighbour in BuildManager.Instance._currentHex.neighbours)
            {
                BuildManager.Instance.GetTileAt(neighbour).DisableHighlight();
            }
        }

        neighbours = BuildManager.Instance.GetNeighboursFor(HexCoords);

        foreach (Vector3Int neighbour in neighbours)
        {
            BuildManager.Instance.GetTileAt(neighbour).EnableHighlight();
        }*/
        #endregion

        BuildManager.Instance._currentHex = this;

        if (turret != null)
        {
            BuildManager.Instance.SelectNode(this);
            return;
        }

        if (!BuildManager.Instance.CanBuild)
            return;

        turret = BuildManager.Instance.BuildTurret(GetBuildPosition());
        BuildManager.Instance.canBuild = false;
        BuildManager.Instance.ghostTowerObject.SetActive(false);
    }

    private void OnMouseEnter()
    {
        if (!BuildManager.Instance.canBuild)
            return;

        BuildManager.Instance.ghostTowerObject.SetActive(true);

        BuildManager.Instance.ghostTowerObject.transform.position = GetBuildPosition();

        foreach (MeshRenderer meshRenderer in BuildManager.Instance.ghostTower._meshRender)
        {
            meshRenderer.sharedMaterial = turret == null ? BuildManager.Instance.ghostTower._valid : BuildManager.Instance.ghostTower._inValid;
        }
    }

    private void OnMouseExit()
    {

    }
}

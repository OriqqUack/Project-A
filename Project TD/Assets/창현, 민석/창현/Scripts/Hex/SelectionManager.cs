using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;

    public LayerMask selectionMask;

    public HexGrid hexGrid;

    List<Vector3Int> neighbours = new List<Vector3Int>();

    private void Awake()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    public void HandleClick(Vector3 mousePosition)
    {
        HighlightControl(mousePosition);

    }

    private bool FindTarget(Vector3 mousePosition, out GameObject result)
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out hit, selectionMask))
        {
            Debug.Log("Raycast hit!");
            result = hit.collider.gameObject;
            Managers.Game._currentTower = result;
            return true;
        }
        result = null;
        return false;
    }

    private void HighlightControl(Vector3 mousePosition)
    {
        GameObject result;
        if (FindTarget(mousePosition, out result))
        {
            Hex selectedHex = result.GetComponent<Hex>();
            Debug.Log(result.transform.position);
            selectedHex.DisableHighlight();

            foreach (Vector3Int neighbour in neighbours)
            {
                Managers.Build.GetTileAt(neighbour).DisableHighlight();
            }

            neighbours = Managers.Build.GetNeighboursFor(selectedHex.HexCoords);

            foreach (Vector3Int neighbour in neighbours)
            {
                Managers.Build.GetTileAt(neighbour).EnableHighlight();
            }

            Debug.Log($"{selectedHex.HexCoords}");
        }
    }
}

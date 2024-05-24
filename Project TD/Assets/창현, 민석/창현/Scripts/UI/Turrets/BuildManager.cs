using UnityEngine;
using System.Collections.Generic;
using System;

public class BuildManager : MonoBehaviour {

	[HideInInspector]
	public Dictionary<Vector3Int, Hex> hexTileDict = new Dictionary<Vector3Int, Hex>();
	[HideInInspector]
	public Dictionary<Vector3Int, List<Vector3Int>> hexTileNeighboursDict = new Dictionary<Vector3Int, List<Vector3Int>>();
	[HideInInspector]
	public Hex _currentHex;

	public GameObject buildEffect;
	public GameObject sellEffect;
	public GameObject ghostTowerObject;
	public GhostTower ghostTower;

	private TurretBlueprint turretToBuild;
	private WallBlueprint wallToBuild;

	private Material possibleMat;
	private Material ImpossibleMat;
	private Hex selectedNode;

	public GameObject UI;
	public BuildUI buildUI;

	public bool CanBuild { get { return turretToBuild != null; } }
	public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

	public GameObject TowerParent;
	public GameObject WallParent;

    public void Init()
    {
        /*UI = Managers.Resource.Instantiate("TD UI/BuildUI");
		buildUI = UI.GetComponent<BuildUI>();
		ghostTowerObject = Managers.Resource.Instantiate("TD UI/PossibleTowerGhost");
		ghostTower = ghostTowerObject.GetComponent<GhostTower>();*/

		TowerParent = new GameObject();
        TowerParent.name = "Towers";
		WallParent = new GameObject();
		WallParent.name = "Walls";
    }

    public Hex GetTileAt(Vector3Int hexCoordinates)
	{
		Hex result = null;
		hexTileDict.TryGetValue(hexCoordinates, out result);
		return result;
	}

	public List<Vector3Int> GetNeighboursFor(Vector3Int hexCoordinates)
	{
		if (hexTileDict.ContainsKey(hexCoordinates) == false)
			return new List<Vector3Int>();

		if (hexTileNeighboursDict.ContainsKey(hexCoordinates))
			return hexTileNeighboursDict[hexCoordinates];

		hexTileNeighboursDict.Add(hexCoordinates, new List<Vector3Int>());

		foreach (Vector3Int direction in Direction.GetDirectionList(hexCoordinates.z))
		{
			if (hexTileDict.ContainsKey(hexCoordinates + direction))
			{
				hexTileNeighboursDict[hexCoordinates].Add(hexCoordinates + direction);
			}
		}
		return hexTileNeighboursDict[hexCoordinates];
	}

	public void SelectNode(Hex node)
	{
		if (selectedNode == node)
		{
			DeselectNode();
			return;
		}

		selectedNode = node;
		turretToBuild = null;

		buildUI.SetTarget(node);
	}

	public void DeselectNode()
	{
		selectedNode = null;
		//buildUI.Hide();
	}

	public void SelectTurretToBuild (TurretBlueprint turret)
	{
		turretToBuild = turret;

		DeselectNode();
	}

	public void SetWall(WallBlueprint wall)
    {
		wallToBuild = wall;
    }

	public TurretBlueprint GetTurretToBuild ()
	{
		return turretToBuild;
	}

	public WallBlueprint GetWallToBuild()
    {
		return wallToBuild;
    }

	public void ClearTurret()
    {
		turretToBuild = null;
	}


}

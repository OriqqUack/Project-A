using UnityEngine;
using System.Collections.Generic;
using System;

public class BuildManager : MonoBehaviour 
{
	public static BuildManager instance;
	public static BuildManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = FindObjectOfType<BuildManager>();
				if (instance == null)
				{
					instance = new GameObject("BuildManager").AddComponent<BuildManager>();
					DontDestroyOnLoad(instance.gameObject);
				}
			}
			return instance;
		}
	}

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
	public bool canBuild = false;

	private TurretBlueprint turretToBuild;
	private WallBlueprint wallToBuild;

	private Material possibleMat;
	private Material ImpossibleMat;
	private Hex selectedNode;

	public GameObject UI;
	public BuildUI buildUI;

	private Vector3 offsetPosition = new Vector3(0, 20, 0);

	public bool CanBuild { get { return turretToBuild != null; } }
	public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

	public GameObject TowerParent;
	public GameObject WallParent;

    private void Awake()
    {
        //UI = Managers.Resource.Instantiate("TD UI/BuildUI");
		//buildUI = UI.GetComponent<BuildUI>();
		//ghostTowerObject = Managers.Resource.Instantiate("TD UI/PossibleTowerGhost");
		//ghostTower = ghostTowerObject.GetComponent<GhostTower>();

		TowerParent = new GameObject();
        TowerParent.name = "Towers";
		WallParent = new GameObject();
		WallParent.name = "Walls";

		foreach (Hex hex in FindObjectsOfType<Hex>())
		{
			hexTileDict[hex.HexCoords] = hex;
		}
	}

    private void Start()
    {
		
	}

	//coordinate µÈ ÁÂÇ¥ÀÇ Hex¸¦ °¡Á®¿È
    public Hex GetTileAt(Vector3Int hexCoordinates)
	{
		Hex result = null;
		hexTileDict.TryGetValue(hexCoordinates, out result);
		return result;
	}

	//hexCoordinatesµÈ ÁÂÇ¥ÀÇ HexÀÇ ÀÌ¿ôÀ» °¡Á®¿È
	public List<Vector3Int> GetNeighboursFor(Vector3Int hexCoordinates)
	{
		if (hexTileDict.ContainsKey(hexCoordinates) == false)
			return new List<Vector3Int>();

		if (hexTileNeighboursDict.ContainsKey(hexCoordinates))
			return hexTileNeighboursDict[hexCoordinates];

		hexTileNeighboursDict.Add(hexCoordinates, new List<Vector3Int>());

		/*foreach (Vector3Int direction in Direction.GetDirectionList(hexCoordinates.z))
		{
			if (hexTileDict.ContainsKey(hexCoordinates + direction))
			{
				hexTileNeighboursDict[hexCoordinates].Add(hexCoordinates + direction);
			}
		}*/
		return hexTileNeighboursDict[hexCoordinates];
	}

	public void SelectNode(Hex node)
	{
		if (selectedNode == node)
			return;

		selectedNode = node;
		turretToBuild = null;
	}


	public void SelectTurretToBuild (TurretBlueprint turret)
	{
		turretToBuild = turret;
	}

	public void SetWall(WallBlueprint wall)
    {
		wallToBuild = wall;
    }


	public WallBlueprint GetWallToBuild() => wallToBuild;

	public void ClearTurret()
    {
		turretToBuild = null;
	}

	public GameObject BuildTurret(Vector3 position)
	{
		if (PlayerStats.Money < turretToBuild.cost)
		{
			Debug.Log("Not enough money to build that!");
			return null;
		}

		PlayerStats.Money -= turretToBuild.cost;

		GameObject _turret = Managers.Resource.Instantiate(turretToBuild.prefab, position + offsetPosition, Quaternion.identity, TowerParent.transform);

		//GameObject effect = (GameObject)Instantiate(BuildManager.Instance.buildEffect, GetBuildPosition(), Quaternion.identity);
		//Destroy(effect, 5f);
		Debug.Log("Turret build!");
		ClearTurret();

		return _turret;
	}

}

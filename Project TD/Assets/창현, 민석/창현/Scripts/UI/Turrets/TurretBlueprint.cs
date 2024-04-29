using UnityEngine;
using System.Collections;

[System.Serializable]
public class TurretBlueprint {

	public GameObject prefab;
	public GameObject possibleGhostPrefab;
	public GameObject ImpossibleGhostPrefab;
	public int cost;

	public string description;

	public GameObject upgradedPrefab;
	public int upgradeCost;

	public int GetSellAmount ()
	{
		return cost / 2;
	}

}

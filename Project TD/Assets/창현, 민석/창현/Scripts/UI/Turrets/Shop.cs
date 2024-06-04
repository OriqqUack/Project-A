using UnityEngine;

public class Shop : MonoBehaviour {

	public TurretBlueprint standardTurret;
	public TurretBlueprint missileLauncher;
	public TurretBlueprint laserBeamer;

	public ToolTip toolTip;

	public WallBlueprint wall;

    private void Start()
    {
		BuildManager.Instance.SetWall(wall);
    }

    public void SelectStandardTurret ()
	{
		BuildManager.Instance.canBuild = true;
		BuildManager.Instance.SelectTurretToBuild(standardTurret);
		//BuildManager.Instance._currentHex.BuildTurret(standardTurret);
		toolTip.SetupTooltip(standardTurret.prefab.name, standardTurret.description);

		/*foreach (Vector3Int neighbour in BuildManager.Instance._currentHex.neighbours)
		{
			BuildManager.Instance.GetTileAt(neighbour).BuildWall(wall);
		}*/
	}

	public void SelectMissileLauncher()
	{
		Debug.Log("Missile Launcher Selected");
		toolTip.SetupTooltip(standardTurret.prefab.name, standardTurret.description);
		//BuildManager.SelectTurretToBuild(missileLauncher);
	}

	public void SelectLaserBeamer()
	{
		Debug.Log("Laser Beamer Selected");
		toolTip.SetupTooltip(standardTurret.prefab.name, standardTurret.description);
		//BuildManager.SelectTurretToBuild(laserBeamer);
	}

}

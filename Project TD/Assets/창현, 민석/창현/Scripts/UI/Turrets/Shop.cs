using UnityEngine;

public class Shop : MonoBehaviour {

	public TurretBlueprint standardTurret;
	public TurretBlueprint missileLauncher;
	public TurretBlueprint laserBeamer;

	public ToolTip toolTip;

	public WallBlueprint wall;

    private void Start()
    {
		Managers.Build.SetWall(wall);
    }

    public void SelectStandardTurret ()
	{
		Managers.Build.SelectTurretToBuild(standardTurret);
		//Managers.Build._currentHex.BuildTurret(standardTurret);
		toolTip.SetupTooltip(standardTurret.prefab.name, standardTurret.description);

		/*foreach (Vector3Int neighbour in Managers.Build._currentHex.neighbours)
		{
			Managers.Build.GetTileAt(neighbour).BuildWall(wall);
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

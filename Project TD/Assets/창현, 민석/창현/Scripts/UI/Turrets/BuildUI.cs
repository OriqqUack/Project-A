using UnityEngine;
using UnityEngine.UI;

public class BuildUI : MonoBehaviour {

	public GameObject ui;

	private Hex target;

	public void SetTarget (Hex _target)
	{
		target = _target;

		transform.position = target.GetBuildPosition();

		ui.SetActive(true);
	}

	public void Hide ()
	{
		ui.SetActive(false);
	}

	/*public void Upgrade ()
	{
		//target.UpgradeTurret();
		BuildManager.Instance.DeselectNode();
	}

	public void Sell ()
	{
		//target.SellTurret();
		BuildManager.Instance.DeselectNode();
	}*/

}

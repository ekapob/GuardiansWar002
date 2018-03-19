using UnityEngine;

public class Shop : MonoBehaviour {

	public TurretBlueprint stdTurret;
	public TurretBlueprint misTurret;
	public TurretBlueprint lasTurret;

	Manager buildManager;

	void Start ()
	{
		buildManager = Manager.instance;
	}
		
	public void SelectStdTurret()
	{
		buildManager.SelectTurretToBuild (stdTurret,"std");
	}

	public void SelectMisTurret()
	{
		buildManager.SelectTurretToBuild (misTurret,"mis");
	}
	public void SelectLasTurret()
	{
		buildManager.SelectTurretToBuild (lasTurret,"las");
	}
}

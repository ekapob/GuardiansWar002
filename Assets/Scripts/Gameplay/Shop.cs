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
		//Debug.Log ("StdTurret Selected");
		buildManager.SelectTurretToBuild (stdTurret);
	}

	public void SelectMisTurret()
	{
		//Debug.Log ("MisTurret Selected");
		buildManager.SelectTurretToBuild (misTurret);
	}
	public void SelectLasTurret()
	{
		//Debug.Log ("LasTurret Selected");
		buildManager.SelectTurretToBuild (lasTurret);
	}
}

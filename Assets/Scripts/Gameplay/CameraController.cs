using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CameraController : Photon.MonoBehaviour {

	public static CameraController Instance;
	private bool doMovement = true;
	public float panSpeed = 30f;
	public float panBorderThickness = 10f;
	public float scrollSpeed = 5f;
	public float minY = 10f;
	public float maxY = 80f;
	private PhotonView PhotonView;
	public bool UseTransformView = true;
	private Vector3 TargetPosition;
	private Quaternion TargetRotation;
	private GameObject turretToBuildx; // prefab of tower need to add more
	public GameObject standardTurretPrefabx;
	public int currentClickNode;
	public GameObject[] tower = new GameObject[42];

	void Start (){
		Instance = this;
		PhotonView = GetComponent<PhotonView> ();
		CanvasGameplayControl.Instance.loadingImg.SetActive (false);
		photonView.RPC ("RPC_SendSideGameplay",PhotonTargets.All);
		turretToBuildx = standardTurretPrefabx;
	}
	void Update ()
	{
		if (!PauseAndExitButton.Instance.pause && photonView.isMine) {
			MoveCode ();
		}
	}

	private void MoveCode(){
		if (Input.GetKeyDown (KeyCode.Escape))
			doMovement = !doMovement;

		if (!doMovement)
			return;

		if (Input.GetKey ("w") /*|| Input.GetKey (KeyCode.UpArrow)/*|| Input.mousePosition.y >= Screen.height - panBorderThickness*/) {
			transform.Translate (Vector3.forward * panSpeed * Time.deltaTime, Space.World);
		}
		if (Input.GetKey ("s") /*|| Input.GetKey (KeyCode.DownArrow)|| Input.mousePosition.y <= panBorderThickness*/) {
			transform.Translate (Vector3.back * panSpeed * Time.deltaTime, Space.World);
		}
		if (Input.GetKey ("d") /*|| Input.GetKey (KeyCode.RightArrow)|| Input.mousePosition.x >= Screen.width - panBorderThickness*/) {
			transform.Translate (Vector3.right * panSpeed * Time.deltaTime, Space.World);
		}
		if (Input.GetKey ("a") /*|| Input.GetKey (KeyCode.LeftArrow)|| Input.mousePosition.x <= panBorderThickness*/) {
			transform.Translate (Vector3.left * panSpeed * Time.deltaTime, Space.World);
		}

		float scroll = Input.GetAxis ("Mouse ScrollWheel");

		Vector3 pos = transform.position;

		pos.y += scroll * 1000 * scrollSpeed * Time.deltaTime;
		pos.y = Mathf.Clamp (pos.y, minY, maxY);

		transform.position = pos;
	}

	private void SmoothMove(){
		if (UseTransformView) {
			return;
		}
		transform.position = Vector3.Lerp (transform.position, TargetPosition, 0.25f);
		transform.rotation = Quaternion.RotateTowards (transform.rotation, TargetRotation, 500 * Time.deltaTime);
	}
	private void OnPhotonSerializeView(PhotonStream stream,PhotonMessageInfo info){
		if (UseTransformView) {
			return;
		}
		if (stream.isWriting) {
			stream.SendNext (transform.position);
			stream.SendNext (transform.rotation);
		} else {
			TargetPosition = (Vector3)stream.ReceiveNext ();
			TargetRotation = (Quaternion)stream.ReceiveNext ();
		}
	}


	[PunRPC]
	private void RPC_SendSideGameplay(){
		//Bug
		//CanvasGameplayControl.Instance.sidePlayer[MotherScript.Instance.currentGameSide]++;
	}

	public void CreateTower(string name){
		if (PlayerNetwork.Instance.joinRoomNum == 1) {
			photonView.RPC ("RPC_CreateTowerP1", PhotonTargets.MasterClient,currentClickNode,Manager.instance.buildName);
		}
		else if (PlayerNetwork.Instance.joinRoomNum == 2) {
			photonView.RPC ("RPC_CreateTowerP2", PhotonTargets.MasterClient,currentClickNode,Manager.instance.buildName);
		}
	}

	//P1 Missile
	[PunRPC]
	private void RPC_CreateTowerP1(int currentNode,string turretName){
		GameObject objTurret = PhotonNetwork.Instantiate (Path.Combine ("Prefabs", turretName), TestNode1.Instance.node[currentNode].transform.position, TestNode1.Instance.node[currentNode].transform.rotation, 0);
	}
	[PunRPC]
	private void RPC_CreateTowerP2(int currentNode,string turretName){
		GameObject objTurret = PhotonNetwork.Instantiate (Path.Combine ("Prefabs", turretName), TestNode2.Instance.node[currentNode].transform.position, TestNode2.Instance.node[currentNode].transform.rotation, 0);
	}
}
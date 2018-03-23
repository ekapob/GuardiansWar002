using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNode : MonoBehaviour {

	public int nodeNo;
	public Color hoverColor;
	[SerializeField]
	private string turret;
	private Renderer rend;
	private Color startColor;
	public static TestNode Instance;

	// Use this for initialization
	void Start () {
		Instance = this;
		turret = null;
		rend = GetComponent<Renderer> ();
		startColor = rend.material.color;
	}


	public void OnMouseEnter(){
		if (!CanvasGameplayControl.Instance.winStat) {
			if (turret == null) {
				if (PlayerNetwork.Instance.joinRoomNum.ToString () == tag)
					rend.material.color = hoverColor;
			}
		}
	}

	public void OnMouseDown(){
		if (!CanvasGameplayControl.Instance.winStat) {
			if (Manager.instance.buildName == null) {
				return;
			}
			if (PlayerNetwork.Instance.joinRoomNum.ToString () == tag) {
				if (turret != null) {
					return;
				}
				CameraController.Instance.currentClickNode = nodeNo;
				CameraController.Instance.CreateTower (Manager.instance.buildName);
				turret = Manager.instance.buildName;
				Manager.instance.buildName = null;
			}
		}
	}

	public void OnMouseExit(){
		rend.material.color = startColor;
	}

	public void SetNodeToNull(){
		turret = null;
	}
}
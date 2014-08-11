using UnityEngine;
using System.Collections;

public class GameToggler : MonoBehaviour {

	public bool Fascist;
	public bool local;
	public GameObject OculusCameraController;
	public GameObject MainCamera;
	public Light lamp1;
	public Light lamp2;

	public void Start() {
		Application.runInBackground = true;
		if (Fascist) {
			OculusCameraController.SetActive(true);
			MainCamera.SetActive(false);	
			Network.incomingPassword = "Klockerkarkopf";
			Network.InitializeServer(1, 1337, false);
			lamp1.flare = null;
			lamp2.flare=null;
		} else {
			OculusCameraController.SetActive(false);
			MainCamera.SetActive(true);
			Destroy (OculusCameraController);
			Application.runInBackground = true;
			Invoke("connectToServer",15);
		}
	}

	private void connectToServer()  {
		Network.Connect("127.0.0.1", 1337, "Klockerkarkopf");
	}

}

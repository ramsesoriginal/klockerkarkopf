using UnityEngine;
using System.Collections;

public class TowerSpotlight : MonoBehaviour {
	public Transform target;

	// Use this for initialization

	// Update is called once per frame
	void Update () {
		transform.LookAt (target);
	}
}

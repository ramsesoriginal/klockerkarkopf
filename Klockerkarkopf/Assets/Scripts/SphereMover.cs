using UnityEngine;
using System.Collections;

public class SphereMover : MonoBehaviour {

	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
		var h = Input.GetAxis ("Horizontal");
		var v = Input.GetAxis ("Vertical");
		var p = transform.position;
		var newP = new Vector3 (p.x + v, p.y, p.z + h);
		transform.position = newP;
	}
}

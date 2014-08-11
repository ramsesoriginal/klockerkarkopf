using UnityEngine;
using System.Collections;

public class FocusMover : MonoBehaviour {
	public Transform target;
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.forward, out hit)) {
			target.position = Vector3.Lerp(target.position, hit.point,  Time.deltaTime);
		}
	}
}

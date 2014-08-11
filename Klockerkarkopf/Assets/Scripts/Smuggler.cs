using UnityEngine;
using System.Collections;

public class Smuggler : MonoBehaviour {

	public Item item;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.GetComponent<Item>()!=null && item==null) {
			item = other.gameObject.GetComponent<Item>();
			other.gameObject.renderer.enabled = false;;
		}
	}
}

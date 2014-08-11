using UnityEngine;
using System.Collections;

public class SmugglerTarget : MonoBehaviour {
	public bool isRunning;
	public bool noItem;
	public Item item;
	public Smuggler smuggler;

	// Use this for initialization
	void Start () {
		isRunning = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.GetComponent<Smuggler>()!=null) {
			smuggler = other.gameObject.GetComponent<Smuggler>();
			if (smuggler.item != null) {
				item = smuggler.item;
				isRunning = false;
			} else {
				noItem = true;
			}
		}
	}

	void OnGUI()
	{
		if (!isRunning) {
			GUI.BeginGroup (new Rect (Screen.width / 2 - 100, Screen.height / 2 - 20, 200, 40));
			
			GUI.Box(new Rect(0,0,200,40),"SMUGGLER WINS!");
			GUI.EndGroup ();

			Time.timeScale = 0;
			Invoke("Application.Quit",5);
		}
		if (noItem) {
			GUI.BeginGroup (new Rect (20, 20, 200, 40));
			
			GUI.Box(new Rect(0,0,200,40),"YOU FORGOT AN ITEM");
			GUI.EndGroup ();
		}
	}
}

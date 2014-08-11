using UnityEngine;
using System.Collections;

public class GuiManager : MonoBehaviour {

	public Texture2D Mountain;
	public Font MainFont;
	public System.DateTime startTime;

	void OnStart() {
		startTime = System.DateTime.Now; //.ToString ("yyyy-MM-dd HH-mm-ss-FFF");
	}

	void OnGUI() {
		GUI.BeginGroup (new Rect (0, Screen.height - 200, 250, 200));
		GUIStyle myStyle = new GUIStyle();
		myStyle.font = MainFont;
		myStyle.normal.textColor = Color.white;
		myStyle.fontSize = 40;
		GUI.DrawTexture(new Rect(0, 0, 250, 200), Mountain, ScaleMode.ScaleToFit, true, 1.25F);
		var difference = System.DateTime.Now - startTime;
		GUI.Label(new Rect(30,100, 200, 60), Time.realtimeSinceStartup.ToString("f2"), myStyle);
		GUI.EndGroup ();
	}
}

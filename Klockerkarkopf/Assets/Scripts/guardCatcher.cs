using UnityEngine;
using System.Collections;

public class guardCatcher : MonoBehaviour {
	public string namesuffix = "smuggler";

	public Material TargetMat;
	public Color canSeeColor;
	public Color originalTargetColor ;


	public GameObject playerObject; // the player
	public float fieldOfViewRange;// in degrees (I use 68, this gives the enemy a vision of 136 degrees)
	public float minPlayerDetectDistance;// the distance the player can come behind the enemy without being deteacted
	public float rayRange;// distance the enemy can "see" in front of him
	public float rayLongRange;// distance the enemy can "see" in front of him

	public Vector3 rayDirection = Vector3.zero;
	public bool isRunning;

	
	void Start () {
		isRunning = true;
	}

	// Update is called once per frame
	void Update () {
		//if (Input.GetButton ("Fire1" + namesuffix)) {
		//	Debug.Log("Fire1 pressed");
			if (CanSeePlayer (rayRange)) {
		//		Debug.Log("IN RANGE");
				isRunning = false;
			}
		//}
		if (CanSeePlayer ()) {
			if (originalTargetColor!= canSeeColor){
				TargetMat.color = canSeeColor;
			}
		}
		else {
			if (TargetMat.color != originalTargetColor){
				TargetMat.color = originalTargetColor;
			}
		}
	}

	bool CanSeePlayer() {
		return CanSeePlayer (rayLongRange);
	}
	
	
	bool CanSeePlayer(float range) {
		RaycastHit hit;
		rayDirection = playerObject.transform.position - transform.position;;
		var distanceToPlayer = Vector3.Distance(transform.position, playerObject.transform.position);
		if(Physics.Raycast (transform.position, rayDirection, out hit)){ // If the player is very close behind the enemy and not in view the enemy will detect the player
			if((hit.transform.gameObject.GetComponent<Smuggler>() != null) && (distanceToPlayer <= minPlayerDetectDistance)){
				//Debug.Log("Caught player sneaking up behind! " + Time.frameCount.ToString());
				return true;
			}
		}

		if((Vector3.Angle(rayDirection, transform.forward)) < fieldOfViewRange){ // Detect if player is within the field of view
			if (Physics.Raycast (transform.position, rayDirection, out hit, range)) {
				
				if (hit.transform.gameObject.GetComponent<Smuggler>() != null) {
					//Debug.Log("Can see player " + Time.frameCount.ToString());
					return true;
				}else{
					//Debug.Log("Can not see player " + Time.frameCount.ToString());
					return false;
				}
			}
		}

		return false;

		/*
		 * if((Vector3.Angle(rayDirection, transform.forward)) < fieldOfViewRange){ // Detect if player is within the field of view
        if (Physics.Raycast (transform.position, rayDirection, hit, rayRange)) {
 
            if (hit.transform.tag == "Player") {
                //Debug.Log("Can see player");
                return true;
            }else{
                //Debug.Log("Can not see player");
                return false;
            }
        }
    }
		 * 
		 * */
	}

	void OnDrawGizmosSelected ()
	{
		// Draws a line in front of the player and one behind this is used to visually illustrate the detection ranges in front and behind the enemy
		Gizmos.color = Color.magenta; // the color used to detect the player in front
		Gizmos.DrawRay (transform.position, transform.forward * rayRange*rayLongRange);
		Gizmos.color = Color.white; // the color used to detect the player in front
		Gizmos.DrawRay (transform.position, transform.forward * rayRange);
		Gizmos.color = Color.yellow; // the color used to detect the player from behind
		Gizmos.DrawRay (transform.position, transform.forward * -minPlayerDetectDistance);      
	}

	
	void OnGUI()
	{
		if (!isRunning) {
			GUI.BeginGroup (new Rect (Screen.width / 2 - 100, Screen.height / 2 - 20, 200, 40));
			
			GUI.Box(new Rect(0,0,200,40),"GUARDS WIN!");
			GUI.EndGroup ();
			
			Time.timeScale = 0;
			Invoke("Application.Quit",5);
		}
	}

}

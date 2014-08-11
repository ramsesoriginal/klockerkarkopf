using UnityEngine;
using System.Collections;

public class PlayerMover : MonoBehaviour {
	public string namesuffix = "smuggler";
	public float speed = 6.0F;
	public float jumpSpeed = 8.0F; 
	public float gravity = 20.0F;
	public float rotateSpeed = 4.0F;
	public GameToggler toggler;
	private Vector3 moveDirection = Vector3.zero;

	public Animator anim;
	
	int jumpHash = Animator.StringToHash("Jump");
	int runStateHash = Animator.StringToHash("Base Layer.Run");
	Vector3 velocity;
	Vector3 localVelocity;
	Vector3 lastPos;
	float localspeed;
	float calculatedSpeed;
	float lastActivity;

	void Start(){
		lastPos = transform.position;
	}

	void FixedUpdate(){
		var newspeed = (transform.position - lastPos).magnitude / Time.deltaTime;
		if (newspeed > 0.1) {
			velocity = (transform.position - lastPos) / Time.deltaTime;
			calculatedSpeed = ((transform.position - lastPos).magnitude / Time.deltaTime) / (2 * speed);
			localVelocity = transform.InverseTransformDirection (velocity);
			lastPos = transform.position;
			if (newspeed < 3) {
					calculatedSpeed = 0;
			}
			lastActivity = Time.time;
		}
		else
		{
			if (Time.time - lastActivity >= 0.3f) {
				calculatedSpeed = 0;
			}
		}

	}
	
	void OnGUI()

	{
		/*GUI.BeginGroup (new Rect (20, 20, 200, 300));
		
		GUI.Box(new Rect(0,0,200,50),"Velocity: " + velocity.ToString());
		GUI.Box(new Rect(0,50,200,50),"localVelocity: " + localVelocity.ToString());
		GUI.Box(new Rect(0,100,200,50),"lastPos: " + lastPos.ToString());
		GUI.Box(new Rect(0,150,200,50),"frameCount: " + Time.frameCount.ToString());
		GUI.Box(new Rect(0,200,200,50),"localspeed: " + localspeed.ToString());
		GUI.Box(new Rect(0,250,200,50),"calculatedSpeed: " + calculatedSpeed.ToString());
		GUI.EndGroup ();*/
	}

	// Update is called once per frame
	void Update () {
		CharacterController controller = GetComponent<CharacterController>();
		float triggerspeed;
		triggerspeed = Input.GetAxis ("Vertical"+namesuffix);
		if (!toggler.local) {
			triggerspeed = calculatedSpeed;
		}
			// is the controller on the ground?
		if (controller.isGrounded) {
			//Feed moveDirection with input.
			moveDirection = new Vector3 (0, 0, triggerspeed);

			anim.SetFloat ("Speed", triggerspeed * speed);
			localspeed = triggerspeed * speed;
			moveDirection = transform.TransformDirection (moveDirection);
			//Multiply it by speed.
			moveDirection *= speed;
			//Jumping
			if ( Input.GetButton ("Jump"+namesuffix)) {
					moveDirection.y = jumpSpeed;
					anim.SetTrigger (jumpHash);
			}

		}
		//Applying gravity to the controller
		moveDirection.y -= gravity * Time.deltaTime;
		//Making the character move
		if (toggler.local) {
			controller.Move (moveDirection * Time.deltaTime);
		} else {
			controller.Move (moveDirection * 0.0001f);
		}
		triggerspeed = Input.GetAxis ("Horizontal"+namesuffix);
		controller.transform.Rotate (0, triggerspeed * rotateSpeed, 0);
		if (Input.GetKeyDown ("f")) {
			//Debug.LogWarning(System.IO.Path.GetFullPath (".") + "\\Screenshots\\" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.FFF") + ".png");
			Application.CaptureScreenshot (System.IO.Path.GetFullPath (".") + "\\Screenshots\\" + System.DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss-FFF") + ".png", 4);
		}
		
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}

	}
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Bird : MonoBehaviour {

	public static Bird instance;

	[SerializeField]
	private Rigidbody2D myRigidbody;
	[SerializeField]
	private Animator anim;
	private float forwardSpeed = 3f;
	private float bounceSpeed = 4f;
	private bool didFlap;
	[HideInInspector]
	public bool isAlive;
	private Button flapButton;
	[SerializeField]
	private AudioSource audioSource;
	[SerializeField]
	private AudioClip flapClip, pointClip, diedClip;

	public int score;

	void Awake(){
		if(instance == null){
			instance = this;
		}
		isAlive = true;
		flapButton = GameObject.FindGameObjectWithTag ("FlapButton").GetComponent<Button> ();
		flapButton.onClick.AddListener (() => FlapTheBird ());
		SetCameraX ();

		score = 0;
	}

	void FixedUpdate(){
		if(isAlive){
			Vector3 temp = transform.position;
			temp.x += forwardSpeed * Time.deltaTime;
			transform.position = temp;

			if(didFlap){
				didFlap = false;
				myRigidbody.velocity = new Vector2 (0, bounceSpeed);
				audioSource.PlayOneShot(flapClip);
				anim.SetTrigger ("Flap");
			}

			if(myRigidbody.velocity.y >= 0){
				transform.rotation = Quaternion.Euler (0, 0, 0);
			} else {
				float angle = 0;
				angle = Mathf.Lerp (0, -75, -myRigidbody.velocity.y / 7);
				transform.rotation = Quaternion.Euler (0, 0, angle);
			}
		}
	}

	void SetCameraX(){
		CameraScript.offsetX = (Camera.main.transform.position.x - transform.position.x);
	}

	public float GetPositionX(){
		return transform.position.x;
	}

	public void FlapTheBird(){
		didFlap = true;
	}

	void OnCollisionEnter2D(Collision2D target){
		if(target.gameObject.tag == "Ground" || target.gameObject.tag == "Pipe"){
			if (isAlive) {
				isAlive = false;
				anim.SetTrigger ("Died");
				audioSource.PlayOneShot (diedClip);
				GameplayController.instance.PlayerDied (score);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "PipeHolder"){
			score++;
			GameplayController.instance.SetScore (score);
			audioSource.PlayOneShot (pointClip);
		}
	}
}

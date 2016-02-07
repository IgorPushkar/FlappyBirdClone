using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public static float offsetX;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Bird.instance != null){
			if(Bird.instance.isAlive){
				MoveCamera ();
			}
		}
	}

	void MoveCamera(){
		Vector3 temp = transform.position;
		temp.x = Bird.instance.GetPositionX () + offsetX;
		transform.position = temp;
	}
}

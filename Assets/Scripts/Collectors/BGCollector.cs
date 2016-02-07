using UnityEngine;
using System.Collections;

public class BGCollector : MonoBehaviour {

	private GameObject[] backgrounds;
	private GameObject[] grounds;

	private float lastBGX;
	private float lastGroundX;

	void Awake () {
		backgrounds = GameObject.FindGameObjectsWithTag ("Background");
		grounds = GameObject.FindGameObjectsWithTag ("Ground");

		lastBGX = backgrounds [0].transform.position.x;
		lastGroundX = grounds [0].transform.position.x;

		foreach(GameObject obj in backgrounds){
			if(lastBGX < obj.transform.position.x){
				lastBGX = obj.transform.position.x;
			}
		}

		foreach(GameObject obj in grounds){
			if(lastGroundX < obj.transform.position.x){
				lastGroundX = obj.transform.position.x;
			}
		}
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Background"){
			Vector3 temp = other.transform.position;
			float width = ((BoxCollider2D)other).size.x;

			temp.x = lastBGX + width;
			other.transform.position = temp;
			lastBGX = temp.x;
		} else if(other.tag == "Ground"){
			Vector3 temp = other.transform.position;
			float width = ((BoxCollider2D)other).size.x;

			temp.x = lastGroundX + width;
			other.transform.position = temp;
			lastGroundX = temp.x;
		}
	}
}

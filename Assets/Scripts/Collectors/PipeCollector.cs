using UnityEngine;
using System.Collections;

public class PipeCollector : MonoBehaviour {

	private GameObject[] pipeHolders;
	private float distance = 3.0f;
	private float lastPipesX;
	private float pipeMax = 2.3f;
	private float pipeMix = -1.9f;

	void Awake(){
		pipeHolders = GameObject.FindGameObjectsWithTag ("PipeHolder");
		lastPipesX = pipeHolders [0].transform.position.x;

		foreach(GameObject obj in pipeHolders){
			if(lastPipesX < obj.transform.position.x){
				lastPipesX = obj.transform.position.x;
			}

			Vector3 temp = obj.transform.position;
			temp.y = Random.Range (pipeMix, pipeMax);
			obj.transform.position = temp;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "PipeHolder"){
			Vector3 temp = other.transform.position;
			temp.x = lastPipesX + distance;
			temp.y = Random.Range (pipeMix, pipeMax);
			other.transform.position = temp;
			lastPipesX = temp.x;
		}
	}
}

using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public static GameController instance;

	private const string HIGH_SCORE = "High Score";
	private const string SELECTED_BIRD = "Selected Bird";
	private const string GREEN_BIRD = "Green Bird";
	private const string RED_BIRD = "Red Bird";

	void Awake(){
		MakeSingleton ();
//		PlayerPrefs.DeleteAll ();
		InitializeValues ();
	}

	void MakeSingleton(){
		if(instance != null){
			Destroy (gameObject);
		} else {
			instance = this;
			DontDestroyOnLoad (gameObject);
		}
	}

	void InitializeValues(){
		if(!PlayerPrefs.HasKey("InitializeValues")){
			PlayerPrefs.SetInt (HIGH_SCORE, 0);
			PlayerPrefs.SetInt (SELECTED_BIRD, 0);
			PlayerPrefs.SetInt (GREEN_BIRD, 0);
			PlayerPrefs.SetInt (RED_BIRD, 0);
			PlayerPrefs.SetInt ("InitializeValues", 1);
		}
	}

	#region High Score
	public void SetHighscore(int score){
		PlayerPrefs.SetInt (HIGH_SCORE, score);
	}

	public int GetHighscore(){
		return PlayerPrefs.GetInt (HIGH_SCORE);
	}
	#endregion

	#region Selected Bird
	public void SetSelectedBird(int index){
		PlayerPrefs.SetInt (SELECTED_BIRD, index);
	}

	public int GetSelectedBird(){
		return PlayerPrefs.GetInt (SELECTED_BIRD);
	}
	#endregion

	#region Green Bird
	public void UnlockGreenBird(){
		PlayerPrefs.SetInt (GREEN_BIRD, 1);
	}

	public bool IsGreenBirdUnlocked(){
		return PlayerPrefs.GetInt (GREEN_BIRD) == 1 ? true : false;
	}
	#endregion

	#region Green Bird
	public void UnlockRedBird(){
		PlayerPrefs.SetInt (RED_BIRD, 1);
	}

	public bool IsRedBirdUnlocked(){
		return PlayerPrefs.GetInt (RED_BIRD) == 1 ? true : false;
	}
	#endregion
}

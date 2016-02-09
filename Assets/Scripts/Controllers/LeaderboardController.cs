using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class LeaderboardController : MonoBehaviour {

	public static LeaderboardController instance;

	private const string LEADERBOARDS_SCORE = "CgkIqZjGkeESEAIQBg";

	void Awake() {
		MakeSingleton ();
	}

	void MakeSingleton(){
		if(instance != null){
			Destroy (gameObject);
		} else {
			instance = this;
			DontDestroyOnLoad (gameObject);
		}
	}

	void Start(){
		PlayGamesPlatform.Activate ();
	}

	void OnLevelWasLoaded(){
		ReportScore (GameController.instance.GetHighscore ());
	}

	public void ConnectOrDisconnectOnGooglePlayGames(){
		if (Social.localUser.authenticated) {
			PlayGamesPlatform.Instance.SignOut ();
		} else {
			Social.localUser.Authenticate ((bool success) => {});
		}
	}

	public void OpenLeaderboardsScore(){
		if (Social.localUser.authenticated) {
			PlayGamesPlatform.Instance.ShowLeaderboardUI (LEADERBOARDS_SCORE);
		}
	}

	void ReportScore(int score){
		if (Social.localUser.authenticated) {
			Social.ReportScore (score, LEADERBOARDS_SCORE, (bool success) => {});
		}
	}
}

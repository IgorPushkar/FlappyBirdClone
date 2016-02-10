using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuController : MonoBehaviour {

	public static MenuController instance;

	[SerializeField]
	private GameObject[] birds;

	private bool isGreenBirdUnlocked, isRedBirdUnlocked;

	[SerializeField]
	private Animator notificationAnimator;

	[SerializeField]
	private Text notificationText;

	void Awake(){
		MakeInstance ();
	}

	void Start(){
		birds [GameController.instance.GetSelectedBird ()].SetActive (true);
		CheckIfBirdsUnlocked ();
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.Escape)){
			Quit ();
		}
	}

	void MakeInstance(){
		if(instance == null){
			instance = this;
		}
	}

	void CheckIfBirdsUnlocked(){
		isGreenBirdUnlocked = GameController.instance.IsGreenBirdUnlocked ();
		isRedBirdUnlocked = GameController.instance.IsRedBirdUnlocked ();
	}

	public void PlayGame(){
		SceneFader.instance.FadeIn ("Gameplay");
	}

	public void ConnectOnGooglePlayGames(){
		LeaderboardController.instance.ConnectOrDisconnectOnGooglePlayGames ();
	}

	public void OpenLeaderboardScoreUI(){
		LeaderboardController.instance.OpenLeaderboardsScore ();
	}

	public void ConnectOnTwitter(){
		SoomlaMediaController.instance.LoginLogoutTwitter ();
	}

	public void ShareOnTwitter(){
		SoomlaMediaController.instance.ShareOnTwitter ();
	}

	public void ChangeBird(){
		if (GameController.instance.GetSelectedBird () == 0) {
			if (isGreenBirdUnlocked) {
				birds [0].SetActive (false);
				GameController.instance.SetSelectedBird (1);
				birds [GameController.instance.GetSelectedBird ()].SetActive (true);
			}
		} else if (GameController.instance.GetSelectedBird () == 1) {
			if (isRedBirdUnlocked) {
				birds [1].SetActive (false);
				GameController.instance.SetSelectedBird (2);
				birds [GameController.instance.GetSelectedBird ()].SetActive (true);
			} else {
				birds [1].SetActive (false);
				GameController.instance.SetSelectedBird (0);
				birds [GameController.instance.GetSelectedBird ()].SetActive (true);
			}
		} else if (GameController.instance.GetSelectedBird () == 2) {
			birds [2].SetActive (false);
			GameController.instance.SetSelectedBird (0);
			birds [GameController.instance.GetSelectedBird ()].SetActive (true);
		}
	}

	public void Quit(){
		Application.Quit ();
	}

	public void NotificationMessage(string message){
		StartCoroutine ("AnimateNotificationPanel", message);
	}

	IEnumerator AnimateNotificationPanel(string message){
		notificationText.text = message;
		notificationAnimator.Play ("SlideIn");
		yield return StartCoroutine (MyCoroutine.WaitForRealSeconds (2f));
		notificationAnimator.Play ("SlideOut");
	}
}

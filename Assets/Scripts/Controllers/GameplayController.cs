using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameplayController : MonoBehaviour {

	public static GameplayController instance;

	[SerializeField]
	private Text scoreText, endScore, bestScore, gameOverText;
	[SerializeField]
	private Button restartButton, instructionsButton;
	[SerializeField]
	private GameObject pausePanel;
	[SerializeField]
	private GameObject[] birds;
	[SerializeField]
	private Sprite[] medals;
	[SerializeField]
	private Image medalImage;

	void Awake(){
		MakeInstance ();
		Time.timeScale = 0.0f;
	}

	void MakeInstance(){
		if(instance == null){
			instance = this;
		}
	}

	public void PauseGame(){
		if(Bird.instance != null){
			if(Bird.instance.isAlive){
				pausePanel.SetActive (true);
				gameOverText.gameObject.SetActive (false);
				endScore.text = "" + Bird.instance.score;
				bestScore.text = "" + GameController.instance.GetHighscore ();
				Time.timeScale = 0.0f;
				restartButton.onClick.RemoveAllListeners ();
				restartButton.onClick.AddListener (() => ResumeGame ());
			}
		}
	}

	public void GoToMenu(){
		SceneFader.instance.FadeIn ("MainMenu");
	}

	public void ResumeGame(){
		pausePanel.SetActive (false);
		Time.timeScale = 1.0f;
	}

	public void RestartGame(){
		SceneFader.instance.FadeIn ("Gameplay");
	}

	public void PlayGame(){
		scoreText.gameObject.SetActive (true);
		birds [GameController.instance.GetSelectedBird ()].SetActive (true);
		instructionsButton.gameObject.SetActive (false);
		Time.timeScale = 1.0f;
	}

	public void SetScore(int score){
		scoreText.text = "" + score;
	}

	public void PlayerDied(int score){
		pausePanel.SetActive (true);
		gameOverText.gameObject.SetActive (true);
		scoreText.gameObject.SetActive (false);
		endScore.text = "" + score;

		if(score > GameController.instance.GetHighscore()){
			GameController.instance.SetHighscore (score);
		}
		bestScore.text = "" + GameController.instance.GetHighscore ();

		if(score <= 20){
			medalImage.sprite = medals [0];
		} else if (score > 20 && score < 40){
			medalImage.sprite = medals [1];
			if(!GameController.instance.IsGreenBirdUnlocked()){
				GameController.instance.UnlockGreenBird();
			}
		} else {
			medalImage.sprite = medals [2];
			if(!GameController.instance.IsGreenBirdUnlocked()){
				GameController.instance.UnlockGreenBird();
			}
			if(!GameController.instance.IsRedBirdUnlocked()){
				GameController.instance.UnlockRedBird();
			}
		}
		restartButton.onClick.RemoveAllListeners ();
		restartButton.onClick.AddListener (() => RestartGame ());
	}
}

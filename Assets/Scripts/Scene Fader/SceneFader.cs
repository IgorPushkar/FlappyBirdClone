using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFader : MonoBehaviour {

	public static SceneFader instance;

	[SerializeField]
	private GameObject fadeCanvas;

	[SerializeField]
	private Animator anim;

	void Awake(){
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

	public void FadeIn(string level){
		StartCoroutine (FadeInAnimation(level));
	}

	public void FadeOut(){
		StartCoroutine (FadeOutAnimation());
	}

	IEnumerator FadeInAnimation(string level){
		fadeCanvas.SetActive (true);
		anim.Play("FadeIn");
		yield return new WaitForSeconds (0.7f);
		SceneManager.LoadScene (level);
		FadeOut ();
	}

	IEnumerator FadeOutAnimation(){
		anim.Play ("FadeOut");
		yield return new WaitForSeconds (1f);
		fadeCanvas.SetActive (false);
	}
}

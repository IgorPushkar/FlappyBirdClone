using UnityEngine;
using System.Collections;
using Soomla;
using Soomla.Profile;
using UnityEngine.SceneManagement;

public class SoomlaMediaController : MonoBehaviour {

	public static SoomlaMediaController instance;



	void Awake(){
		MakeSingleton ();
	}

	void Start(){
		SoomlaProfile.Initialize ();
	}

	public void OnEnable (){
		ProfileEvents.OnLoginFinished += onLoginFinished;
		ProfileEvents.OnLoginFailed += onLoginFailed;
		ProfileEvents.OnLoginCancelled += onLoginCancelled;
		ProfileEvents.OnLogoutFinished += onLogoutFinished;
		ProfileEvents.OnLogoutFailed += onLogoutFailed;
		ProfileEvents.OnSocialActionFinished += onSocialActionFinished;
		ProfileEvents.OnSocialActionFailed += onSocialActionFailed;
		ProfileEvents.OnSocialActionCancelled += onSocialActionCancelled;
	}

	public void OnDisable(){
		ProfileEvents.OnLoginFinished -= onLoginFinished;
		ProfileEvents.OnLoginFailed -= onLoginFailed;
		ProfileEvents.OnLoginCancelled -= onLoginCancelled;
		ProfileEvents.OnLogoutFinished -= onLogoutFinished;
		ProfileEvents.OnLogoutFailed -= onLogoutFailed;
		ProfileEvents.OnSocialActionFinished -= onSocialActionFinished;
		ProfileEvents.OnSocialActionFailed -= onSocialActionFailed;
		ProfileEvents.OnSocialActionCancelled -= onSocialActionCancelled;
	}

	void MakeSingleton(){
		if(instance != null){
			Destroy (gameObject);
		} else {
			instance = this;
			DontDestroyOnLoad (gameObject);
		}
	}

	public void LoginLogoutTwitter(){
		if(SoomlaProfile.IsLoggedIn(Provider.TWITTER)){
			SoomlaProfile.Logout (Provider.TWITTER);
		} else {
			SoomlaProfile.Login (Provider.TWITTER);
		}
	}

	public void ShareOnTwitter(){
		if(SoomlaProfile.IsLoggedIn(Provider.TWITTER)){
			SoomlaProfile.UpdateStory (
				Provider.TWITTER,
				"All your base are belong to us",
				null,
				null,
				null,
				"WWW.link.com",
				null,
				null,
				null
			);
		} else if (SceneManager.GetActiveScene().name == "MainMenu"){
			MenuController.instance.NotificationMessage ("Please connect in order to post");
		}
	}

	void onLoginFinished(UserProfile userProfileJson, bool autoLogin, string payload) {
		if(SceneManager.GetActiveScene().name == "MainMenu"){
			MenuController.instance.NotificationMessage ("Connected");
		}
	}

	public void onLoginCancelled(Provider provider, bool autoLogin, string payload) {
		if(SceneManager.GetActiveScene().name == "MainMenu"){
			MenuController.instance.NotificationMessage ("Log In canceled");
		}
	}

	public void onLoginFailed(Provider provider, string message, bool autoLogin, string payload) {
		if(SceneManager.GetActiveScene().name == "MainMenu"){
			MenuController.instance.NotificationMessage ("Log In failed");
		}
	}

	public void onLogoutFinished(Provider provider) {
		if(SceneManager.GetActiveScene().name == "MainMenu"){
			MenuController.instance.NotificationMessage ("Disconnected");
		}
	}

	public void onLogoutFailed(Provider provider, string message) {
		if(SceneManager.GetActiveScene().name == "MainMenu"){
			MenuController.instance.NotificationMessage ("Could not disconnect");
		}
	}

	public void onSocialActionFinished(Provider provider, SocialActionType action, string payload) {
		if(provider == Provider.TWITTER){
			if(action == SocialActionType.UPDATE_STORY){
				if(SceneManager.GetActiveScene().name == "MainMenu"){
					MenuController.instance.NotificationMessage ("Thank you for Sharing");
				}
			}
		}
	}

	public void onSocialActionCancelled(Provider provider, SocialActionType action, string payload) {
		if(provider == Provider.TWITTER){
			if(action == SocialActionType.UPDATE_STORY){
				if(SceneManager.GetActiveScene().name == "MainMenu"){
					MenuController.instance.NotificationMessage ("Could not post");
				}
			}
		}
	}

	public void onSocialActionFailed(Provider provider, SocialActionType action, string message, string payload) {
		if(provider == Provider.TWITTER){
			if(action == SocialActionType.UPDATE_STORY){
				if(SceneManager.GetActiveScene().name == "MainMenu"){
					MenuController.instance.NotificationMessage ("Could not post");
				}
			}
		}
	}
}

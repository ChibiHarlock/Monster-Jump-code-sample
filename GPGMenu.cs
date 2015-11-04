using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine.SocialPlatforms;

/** Attach this script and NerdGPG script to the Main Camera for Google API to work.
GPG means Google Play Games**/
public class GPGMenu : MonoBehaviour { 
	public GameObject go_ScoreBoard,go_MainMenu;           		 // Reference to the scoreboard and main menu of the game
	
	/**Google Play Games Variable Declarations**/
	public GameObject btn_LogIn, btn_LogOut, btn_LeaderBoard, 
					  btn_PostScore, btn_AchievementBoard;       // Gameobjects used to access features on Google Play Games
	private string lb_BestScore    = "Leaderboard ID";           // String that identifies a GPG leaderboard for the game
	private string achv_Welcome    = "Achievement ID";           // Achievement for signing in the first time on GPG
	private string achv_1          = "Achievement ID";           // Acheivement for reaching a game score of 1  
	private string achv_3          = "Achievement ID";           // Achievement for reaching a game score of 3  
	private string achv_10         = "Achievement ID";           // Achievement for reaching a game score of 10  
	private string achv_15         = "Achievement ID";           // Achievement for reaching a game score of 15  
	private string achv_25         = "Achievement ID";           // Achievement for reaching a game score of 25  
	private string achv_30         = "Achievement ID";           // Achievement for reaching a game score of 30
	private string achv_40         = "Achievement ID";           // Achievement for reaching a game score of 40
	private string achv_50         = "Achievement ID";           // Achievement for reaching a game score of 50
	private string achv_100        = "Achievement ID";           // Achievement for reaching a game score of 100
	
	private int gameScore;                                       // Reference to get score variable from PlayerPrefs
	
	void OnUnlockAC(bool result){}                               // Function for unlock achievement callback
	void OnLoadAC(IAchievement[] achievements){}                 // Load all the achievements in an array
	void OnLoadACDesc(IAchievementDescription[] acDesc){}        // Load all the achievement array's description           
	void OnSubmitScore(bool result){							 // Function to callback if the score was submitted
		Debug.Log("GPGUI: OnSubmitScore: " + result);
	}                            
	void OnAuthCB(bool result){}                                 // Function to callback if the user authenticated

	// This function runs first before any other function
	void Awake() {
		// Without this line, the Google API will not work
		Social.Active = new UnityEngine.SocialPlatforms.GPGSocial();
	}
	
	void Start() {
		gameScore = PlayerPrefs.GetInt("BestScore");             // Retrieving the BestScore integer from PlayerPrefs
	}
	
	/**Show the scorePost button after a few seconds only
    if the user signed in and after the iTween animation plays
    for the scoreboard object to become active **/                                                                                                                           
	IEnumerator ShowButton() {                       
		if(go_ScoreBoard.activeInHierarchy == true) {
			if(Social.localUser.authenticated) {                  
				yield return new WaitForSeconds(2.5f);            
				btn_PostScore.SetActive(true);
			}
			else{
				btn_PostScore.SetActive(false);
			}	
		}
		
		// If the main menu is active, turn off all GPG buttons in the scene
		if(go_MainMenu.activeInHierarchy == false) {
			btn_LogOut.SetActive(false);
			btn_LeaderBoard.SetActive(false);
			btn_LogIn.SetActive(false);
			btn_AchievementBoard.SetActive(false);
		}
		
		// Unlock Achievements when the game score reaches a certain number
		if(gameScore == 1) 		{Social.ReportProgress(achv_1, 100.0, OnUnlockAC);}	
		if(gameScore == 3) 		{Social.ReportProgress(achv_3, 100.0, OnUnlockAC);}
		if(gameScore == 10)	 	{Social.ReportProgress(achv_10, 100.0, OnUnlockAC);}
		if(gameScore == 15) 	{Social.ReportProgress(achv_15, 100.0, OnUnlockAC);}
		if(gameScore == 25) 	{Social.ReportProgress(achv_25, 100.0, OnUnlockAC);}
		if(gameScore == 30) 	{Social.ReportProgress(achv_30, 100.0, OnUnlockAC);}
		if(gameScore == 40) 	{Social.ReportProgress(achv_40, 100.0, OnUnlockAC);}
		if(gameScore == 50) 	{Social.ReportProgress(achv_50, 100.0, OnUnlockAC);}
		if(gameScore == 100) 	{Social.ReportProgress(achv_100, 100.0, OnUnlockAC);}
	}
	
	void Update() {
		StartCoroutine(ShowButton()); // Check every frame so the score Post button doesn't turn off	
	}
	
	/**Button Functions with GPG**/
	
	public void LogInButton() {
		Social.localUser.Authenticate(OnAuthCB);
		UserSignedIn();	
	}
	
	public void LogOutButton() {
		NerdGPG.Instance().signOut();
		UserNotSignedIn();
	}
	
	public void PostScore(int score) {
		score = PlayerPrefs.GetInt("BestScore");
		Social.ReportScore(score, lb_BestScore, OnSubmitScore);
		LeaderBoard();
	}
	
	public void AchievementBoard() {
		Social.LoadAchievements(OnLoadAC);
		Social.LoadAchievementDescriptions(OnLoadACDesc);
		Social.ShowAchievementsUI();	
	}
	
	public void LeaderBoard() {
		Social.ShowLeaderboardUI();
	}
	
	// If the user is not signed in then show only the log in button
	void UserNotSignedIn() {
			btn_LogOut.SetActive(false);
			btn_LeaderBoard.SetActive(false);
			btn_LogIn.SetActive(true);
			btn_AchievementBoard.SetActive(false);
	}
	/** If the user is signed in then show every button except the log in button
	Unlock the first achievement for signing in for the first time**/
	void UserSignedIn() {
			Social.ReportProgress(achv_Welcome, 100.0, OnUnlockAC);
			btn_LogIn.SetActive(false);
			btn_LogOut.SetActive(true);
			btn_LeaderBoard.SetActive(true);
			btn_AchievementBoard.SetActive(true);
	}
}

// This code sample allows you to post the score to the game's Leaderboard using Google API
/**GPG means Google Play Games**/
private string lb_BestScore    = "Leaderboard ID";           // String that identifies a GPG leaderboard for the game
...
  // Function for posting the score to the Leaderboard
  public void PostScore(int score) {                      
		score = PlayerPrefs.GetInt("BestScore");                  // Get the BestScore integer from the PlayerPrefs class
		Social.ReportScore(score, lb_BestScore, OnSubmitScore);   // Sends a callback to know if the score was reported
		LeaderBoard();                                            // Function to show the Google Leaderboard built-in UI window
	}
	public void LeaderBoard() {
		Social.ShowLeaderboardUI();                               // Accessing Unity's Social class to run the function
	}
	void OnSubmitScore(bool result){                                  // Function to callback if the score was submitted
		Debug.Log("GPGUI: OnSubmitScore: " + result);
	}                            	  
	...
}

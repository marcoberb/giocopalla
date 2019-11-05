using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System;

public class PlayGamesController : MonoBehaviour
{
    public GameObject MainCamera;

    private MenuScene MenuScene;

    public void Start()
    {
        this.MenuScene = this.MainCamera.GetComponent<MenuScene>();
        this.AuthenticateUser();
    }

    private void AuthenticateUser()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate((bool success) =>
        {
            if (success == true)
            {
                Debug.Log("Logged in Google Play Services");
                this.UpdateLocalHighScore();
            }
            else
            {
                Debug.Log("Unable to sign in to Google Play Games Services");
            }
        });
    }

    private void UpdateLocalHighScore()
    {
        string id = Social.localUser.id;
        this.GetPlayerHighScore(id, (status) => 
        {
            int playHighScore = (int)status;
            int localHighScore = PlayerPrefs.GetInt("high_score");

            if (playHighScore > localHighScore)
            {
                PlayerPrefs.SetInt("high_score", playHighScore);
                this.MenuScene.HighScore.text = "HIGH SCORE: " + PlayerPrefs.GetInt("high_score").ToString();
            }
            else if (playHighScore < localHighScore)
            {
                PostPlayerScoreToLeaderboard(localHighScore);
            }
            
        });
    }


    public static void PostPlayerScoreToLeaderboard(int newScore)
    {
        Social.ReportScore(newScore, GPGSIds.leaderboard_global_leaderboard, (bool success) =>
        {
            if (success)
            {
                Debug.Log("Posted new score to leaderboard");
            }
            else
            {
                Debug.Log("Unable to post new score to leaderboard");
            }
        });
    }

    private void GetPlayerHighScore(string user, Action<long> playerHighScore)
    {
        Social.LoadScores(GPGSIds.leaderboard_global_leaderboard, scores =>
        {
            if (scores.Length > 0)
            {
                Debug.Log("Retrieved " + scores.Length + " scores");
                for(int i=0; i < scores.Length; i++)
                {
                    if (scores[i].userID == user)
                    {
                        playerHighScore(scores[i].value);
                        break;
                    }
                }
            }
            else
            {
                Debug.Log("No scores in global leaderboard");
            }
        });
    }

    public static void ShowGlobalLeaderboard()
    {
        PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_global_leaderboard);
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScene : MonoBehaviour {

    public Text HighScore;          // high score of player written in "Menu" scene 

    public GameObject InfoPanel;            // panel that shows how to play
    public GameObject CreditsPanel;         // panel that shows credits information and rating button

    public GameObject BackgroundHoverBlack;         // when panels are open darken the rest of screen

    public void Start()
    {
        // objects that need to be deactivated at start
        this.InfoPanel.SetActive(false);
        this.CreditsPanel.SetActive(false);
        this.BackgroundHoverBlack.SetActive(false);

        // high score loaded from player prefs. If there isn't one is set to 0
        this.HighScore.text = "HIGH SCORE: " + PlayerPrefs.GetInt("high_score", 0).ToString();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            this.QuitGame();
    }

    /* void PlayGame(): loads game scene */
    public void PlayGame()
	{
        SceneManager.LoadScene("Game");
	}

    /* void ResetHighScore(): resets the high score to 0 */
    public void ResetHighScore()
    {
        PlayerPrefs.DeleteKey("high_score");
        this.HighScore.text = "HIGH SCORE: " + PlayerPrefs.GetInt("high_score", 0).ToString();
    }

    /* void QuitGame(): closes the app */
    public void QuitGame()
    {
        Application.Quit();
    }

    /* void ShowInfo(): opens "InfoPanel" panel */
    public void ShowInfo()
    {
        PlayerPrefs.SetString("first_time", "false");
        this.InfoPanel.SetActive(true);
        this.BackgroundHoverBlack.SetActive(true);
    }

    /* void HideInfo(): closes "InfoPanel" panel */
    public void HideInfo()
    {
        this.InfoPanel.SetActive(false);
        this.BackgroundHoverBlack.SetActive(false);
    }

    /* void ShowCredits(): opens "CreditsPanel" panel */
    public void ShowCredits()
    {
        this.CreditsPanel.SetActive(true);
        this.BackgroundHoverBlack.SetActive(true);
    }

    /* void HideCredits(): closes "CreditsPanel" panel */
    public void HideCredits()
    {
        this.CreditsPanel.SetActive(false);
        this.BackgroundHoverBlack.SetActive(false);
    }

    /* void RateUs(): redirects to market page of app */
    public void RateUs()
    {
        Application.OpenURL("market://details?id=" + Application.identifier);
    }
}
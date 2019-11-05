using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameScene : MonoBehaviour
{
    public float GameTime { get; set; }         // maximum time to catch the new circle

    public int CurrentScore { get; set; }           // score of the current game

    public int HighScore { get; set; }          // high score of the player

    public GameObject LosingPanel;          // panel shown when losing and "CurrentScore" is less than "HighScore"
    public Text LosingPanelCurrentScoreText;            // text of "CurrentScore" at losing time
    public Text LosingPanelHighScoreText;           // text of "HighScore" of the player

    public GameObject WinningPanel;         // panel shown when losing and "CurrentScore" is higher than "HighScore"
    public Text WinningPanelNewHighScoreText;           // text of new "HighScore" of the player

    public GameObject BackToMenu;           // button that brings back to menu

    public Text DisplayedScoreText;         // text of "CurrentScore" in scene when playing

    private GoogleAdMobBanners GoogleAdMobBanners;          // object that manages banners

    public void Start()
    {
        this.GoogleAdMobBanners = this.gameObject.GetComponent<GoogleAdMobBanners>();

        // objects that need to be deactivated at start
        this.LosingPanel.gameObject.SetActive(false);
        this.WinningPanel.gameObject.SetActive(false);
        this.BackToMenu.gameObject.SetActive(false);

        // sets "GameTime" using "EstimateTimer" function
        this.GameTime = this.EstimateTimer();
        Debug.Log("gameTime: " + this.GameTime);

        // sets "CurrentScore" to zero
        this.CurrentScore = 0;

        // retrieves "HighScore" from player preferences or zero
        this.HighScore = PlayerPrefs.GetInt("high_score", 0);

        this.GoogleAdMobBanners.LoadBanners();
        //this.GoogleAdMobBanners.ShowBanners();
    }

    public void Update()
    {
        // updates "DisplayedScoreText" with "CurrentScore"
        this.DisplayedScoreText.text = this.CurrentScore.ToString();
    }

    /* float EstimateTimer(): determines game time based on dimensions of the screen */
    private float EstimateTimer()   // TROVA UN ALGORITMO CHE CALCOLI IL TEMPO GIUSTO SU OGNI SCHERMO
    {
        float screenWidth;
        float screenHeight;
        float gameTime = 0.46f;

        Debug.Log("width: " + Screen.width + " height: " + Screen.height + " dpi: " + Screen.dpi);

        if (Screen.dpi != 0)
        {
            screenWidth = Screen.width / Screen.dpi;
            screenHeight = Screen.height / Screen.dpi;
            gameTime += (Mathf.Sqrt(Mathf.Pow(screenWidth, 2) + Mathf.Pow(screenHeight, 2))) / 100f;
        }
        else
        {
            gameTime = 0.51f;
        }

        return gameTime;
    }

    /* void CheckHighScore(): loads and shows banners, then checks if player has made new high score and shows related panel */
    public void CheckHighScore()
    {
        //this.GoogleAdMobBanners.LoadBanners();
        this.GoogleAdMobBanners.ShowBanners();

        if ( this.CurrentScore > this.HighScore )
        {
            PlayerPrefs.SetInt("high_score", this.CurrentScore);
            this.HighScore = PlayerPrefs.GetInt("high_score");

            PlayGamesController.PostPlayerScoreToLeaderboard(this.HighScore);

            ShowNewHighScoreBox();
        }
        else
        {
            ShowLosingBox();
        }
    }

    /* void ShowNewHighScore(): shows "WinningPanel" and related */
    private void ShowNewHighScoreBox()
    {
        WinningPanelNewHighScoreText.text = this.HighScore.ToString();

        WinningPanel.gameObject.SetActive(true);
        BackToMenu.gameObject.SetActive(true);

        // to avoid ugly graphical effects
        this.DisplayedScoreText.gameObject.SetActive(false);
    }

    /* void ShowLosingBox(): show "LosingPanel" and related */
    private void ShowLosingBox()
    { 
        LosingPanelCurrentScoreText.text = this.CurrentScore.ToString();
        LosingPanelHighScoreText.text = "HIGH SCORE: " + this.HighScore.ToString();

        LosingPanel.gameObject.SetActive(true);
        BackToMenu.gameObject.SetActive(true);

        // to avoid ugly graphical effect
        this.DisplayedScoreText.gameObject.SetActive(false);
    }

    /* void TryAgain(): sets scene for new game */
    public void TryAgain()
    {
        LosingPanel.gameObject.SetActive(false);
        WinningPanel.gameObject.SetActive(false);
        BackToMenu.gameObject.SetActive(false);

        this.DisplayedScoreText.gameObject.SetActive(true);

        this.GoogleAdMobBanners.LoadBanners();

        this.CurrentScore = 0;
    }

    /* void GoBacktoMenu(): loads menu scene */
    public void GoBackToMenu()
    {
        this.GoogleAdMobBanners.HideBanners();

        SceneManager.LoadScene("Menu");
    }
}

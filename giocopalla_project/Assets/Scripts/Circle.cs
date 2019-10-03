using UnityEngine;

public class Circle : MonoBehaviour 
{
    private bool isWaitingToPlay;            // indicates if game is waiting to start
    private bool isGameLost;         // indicates if player has lost
    private bool isPressed;          // indicates if this circle is pressed

    public float ActiveTimer { get; set; }          // active timer to reach new position of circle

	private Vector2 auxCircle;           // indicates new position of circle

    public GameObject MainCamera;
    private GameScene GameScene;

    void Start ()
    {
        this.GameScene = this.MainCamera.GetComponent<GameScene>();

        this.InitializeGame();
    }

    void Update () 
	{
        // if player is playing 
        if ( !isWaitingToPlay ) //Se non sono in attesa di giocare e non ho perso
		{
            // decreases "ActiveTimer"
			this.ActiveTimer -= Time.deltaTime;
		}

        // if "ActiveTimer" is expired
        if (this.ActiveTimer < 0.01f)
        {
            // player has lost
            isGameLost = true;

            // reset "ActiveTimer"
            this.ActiveTimer = this.GameScene.GameTime;
        }
        
        // if player has lost the game
		if (isGameLost)
		{
            this.InitializeGame();
            this.GameScene.CheckHighScore();
		}
	}

    /* void InitializeGame(): sets the variable to start a new game */
    private void InitializeGame()
    {
        // initializes variable
        this.isWaitingToPlay = true;
        this.isGameLost = false;
        this.isPressed = false;
        this.ActiveTimer = this.GameScene.GameTime;

        // initializes position of this circle and aux circle
        transform.position = new Vector2(0, 0);
        this.auxCircle = new Vector2(0, 0);
    }

    void OnMouseDown ()
    {
        // if winning or losing panels are closed
        if ( !this.GameScene.BackToMenu.gameObject.activeInHierarchy )
        {
            this.isWaitingToPlay = false;
            this.isGameLost = false;
            this.isPressed = true;
        }
	}
	
	void OnMouseOver()
	{
		if(isPressed)
		{
            // to avoid that "ActiveTimer" espires while generating new position
            this.ActiveTimer = float.MaxValue;

            // generates random position until is quite far from the current
			do{
				this.auxCircle = new Vector2(UnityEngine.Random.Range (-2.2f, 2.2f), UnityEngine.Random.Range (-4.4f, 4.4f));
			}while(this.auxCircle.x > transform.position.x-1.575f && this.auxCircle.x < transform.position.x+1.575f && this.auxCircle.y > transform.position.y-1.575f && this.auxCircle.y < transform.position.y+1.575f); 

            // moves circle to new position
			transform.position = this.auxCircle;

            // resets ActiveTimer
            this.ActiveTimer = this.GameScene.GameTime;

            // increases "CurrentScore"
            this.GameScene.CurrentScore++;

            // to avoid ugly graphical effects
            if (this.GameScene.CurrentScore > 99)
                this.GameScene.DisplayedScoreText.fontSize = 200;
        } 
	}

	void OnMouseUp () 
	{
        this.isGameLost = true;
	}
}
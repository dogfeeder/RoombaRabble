using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public static int difficulty;

	public GameObject[] tiles;
	public static int tileCount = 0;
	public static int cleanTiles = 0;
	public Text tileCountText;

    public GameObject winGUI;
    public GameObject menuGUI;
    public Text timerGUI;
    public Text timeTakenGUI;
    public float timer = 0;
    public bool gameOver;

    public AudioClip win;
    private bool played = false;
    private bool showingMainMenu;
    private float winPercentage;
    private float percentageCalc;

    public AudioClip clickSound;
    public GameObject clickControl;
    private AudioSource clickAS;
    private AudioSource thisAudioSource;
    private static bool musicMuted;

    public Sprite speakerIcon;
    public Sprite speakerMuted;
    public Image speakerImage;

    private int totalTiles;

    // Use this for initialization
    void Start() {
        clickAS = clickControl.GetComponent<AudioSource>();
        thisAudioSource = GetComponent<AudioSource>();
        Application.targetFrameRate = 60;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 1;
        cleanTiles = 0;
        tiles = GameObject.FindGameObjectsWithTag("tile");
        totalTiles = tiles.Length;
        tileCount = tiles.Length;
        tileCountText.text = tileCount.ToString();

        if (musicMuted) { 
            thisAudioSource.Stop();
            speakerImage.sprite = speakerMuted;
        } else {
            speakerImage.sprite = speakerIcon;
        }

        switch (difficulty)
        {
            case 1:
                winPercentage = 60f;
                break;
            case 2:
                winPercentage = 80f;
                break;
            case 3:
                winPercentage = 100f;
                break;
            default:
                winPercentage = 60f;
                break;
        }
	}
	
	// Update is called once per frame
	void Update () {

        //MainMenu showing
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (showingMainMenu)
            {
                hideMainMenu();
            }
            else
            {
                showMainMenu();
            }
        }

        //Timer Stuff
        string minutes = Mathf.Floor(timer / 60).ToString("00");
        string seconds = Mathf.Floor(timer % 60).ToString("00");

        if (!gameOver)
        {
            timer += Time.deltaTime;
            timerGUI.text = minutes + ":" + seconds;
        } else
        {
            timeTakenGUI.text = "Time Taken: " + minutes + ":" + seconds;
        }

        //Finding Tiles
        tiles = GameObject.FindGameObjectsWithTag("tile");
		tileCount = tiles.Length - cleanTiles;
		//tileCountText.text = "Tiles Left: " + tileCount.ToString() + "/" + totalTiles.ToString ();
		percentageCalc = 100 - (float)tileCount / (float)totalTiles * 100;
		percentageCalc = Mathf.Floor (percentageCalc);
		tileCountText.text = percentageCalc.ToString() + "% Clean";

		if (percentageCalc >= winPercentage) {
            winGUI.SetActive(true);
            Cursor.visible = true;
            if (!played)
            {
                GetComponent<AudioSource>().PlayOneShot(win, 1.0f);
                GameObject.Find("Player").GetComponent<AudioSource>().Stop();
                played = true;
            }
            tileCountText.color = Color.green;
            gameOver = true;
            Time.timeScale = 0;
		}

	}
	//Restart Game
	public void RestartGame() {
        Time.timeScale = 1;
        Scene scene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(scene.name);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void showMainMenu()
    {
        menuGUI.SetActive(true);
        showingMainMenu = true;
        Time.timeScale = 0;
        Cursor.visible = true;
    }

    public void hideMainMenu()
    {
        menuGUI.SetActive(false);
        showingMainMenu = false;
        Time.timeScale = 1;
        Cursor.visible = false;
    }

    public void ClickSound()
    {
        clickAS.PlayOneShot(clickSound, 1.0f);
    }

    public void MuteMusic()
    {
        if (musicMuted)
        {
            thisAudioSource.Play();
            speakerImage.sprite = speakerIcon;
            musicMuted = false;
        } else
        {
            thisAudioSource.Stop();
            speakerImage.sprite = speakerMuted;
            musicMuted = true;
        }
    }
}

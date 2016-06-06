﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuScript : MonoBehaviour {
	public Canvas quitMenu;
	public Button startText;
	public Button exitText;

    public GameObject difficultyMenu;

    private int difficulty;
	// Use this for initialization
	void Start () {
		quitMenu = quitMenu.GetComponent<Canvas> ();
		startText = startText.GetComponent<Button> ();
		exitText = exitText.GetComponent<Button> ();
		quitMenu.enabled = false;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void showDifficultyMenu()
    {
        difficultyMenu.SetActive(true);
    }

    public void hideDifficultyMenu()
    {
        difficultyMenu.SetActive(false);
    }

    public void setDifficulty(int diff)
    {
        difficulty = diff;
        StartLevel();
    }

	public void ExitPress(){
		quitMenu.enabled = true;
		startText.enabled = false;
		exitText.enabled = false;
	}

	public void NoPress(){
		quitMenu.enabled = false;
		startText.enabled = true;
		exitText.enabled = true;
	}

	public void StartLevel (){
        GameController.difficulty = difficulty;
		SceneManager.LoadScene(1);

	}

    public void ExitGame(){
		Application.Quit ();
	}
}

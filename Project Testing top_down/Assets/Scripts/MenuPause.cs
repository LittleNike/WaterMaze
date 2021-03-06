﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPause : MonoBehaviour {

    // Use this for initialization
    public static bool GameIsPaused = false;

    public GameObject PauseMenuUI;

    public Button BtnResume;
    public Button BtnOptions;
    public Button BtnMenu;
    public Button BtnQuit;

    public void Start()
    {
    }


	// Update is called once per frame
	void Update () {

        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("joystick button 7"))
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
	}

    //add to sources https://www.youtube.com/watch?v=JivuXdrIHK0

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        BtnResume.OnSelect(null);
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void OpenOptions()
    {
        Debug.Log("Opening Options");
    }

    public void LoadMenu()
    {
        Debug.Log("Loading Menu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}

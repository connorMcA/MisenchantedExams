using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public GameObject titleScreen;
    public GameManager gameManager;
    public GameObject mainMenu;
    public GameObject levelSelect;
    // Start is called before the first frame update
    void Start()
    {
        titleScreen = GameObject.Find("TitleScreen");
        mainMenu = GameObject.Find("MainMenu");
        levelSelect = GameObject.Find("LevelSelect");
        levelSelect.SetActive(false);
        gameManager = GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartLevel(int levelNumber)
    {
        titleScreen.SetActive(false);
        gameManager.StartGame(levelNumber);

    }

    public void ResetMainMenu()
    {
        gameManager.Stop();
        titleScreen.SetActive(true);
        Time.timeScale = 1;
        mainMenu.SetActive(true);
        levelSelect.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

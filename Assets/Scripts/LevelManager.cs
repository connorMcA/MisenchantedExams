using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public GameObject titleScreen;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        titleScreen = GameObject.Find("TitleScreen");
        gameManager = GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartLevel()
    {
        titleScreen.SetActive(false);
        gameManager.StartGame();

    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

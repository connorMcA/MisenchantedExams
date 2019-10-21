using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public GameObject levelObjects;
    public GameObject titleScreen;
    public GameObject cauldron;

    // Start is called before the first frame update
    void Start()
    {
        levelObjects = GameObject.Find("LevelObjects");
        levelObjects.SetActive(false);
        titleScreen = GameObject.Find("TitleScreen");
        cauldron = GameObject.Find("Cauldron");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartLevel()
    {
        titleScreen.SetActive(false);
        levelObjects.SetActive(true);
        GetComponent<Spell>().enabled = true;
        cauldron.GetComponent<Cauldron>().enabled = true;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

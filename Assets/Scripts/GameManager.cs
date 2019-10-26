using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    Timer timer;
    Cauldron cauldron;
    GameObject pauseMenu;
    GameObject[] levels;
    GameObject everyLevelObjects;
    Box openBox;
    int levelCount = 3;

    // Start is called before the first frame update
    void Start()
    {
        cauldron = GameObject.Find("Cauldron").GetComponent<Cauldron>();
        cauldron.enabled = false;

        timer = GameObject.Find("Timer").GetComponent<Timer>();
        timer.enabled = false;

        pauseMenu = GameObject.Find("PauseMenu");
        pauseMenu.SetActive(false);

        openBox = GameObject.Find("Open Box").GetComponent<Box>();

        everyLevelObjects = GameObject.Find("EveryLevelObjects");
        everyLevelObjects.SetActive(false);

        levels = new GameObject[3];

        for (int i = 0; i < levelCount; i++)
        {

            levels[i] = GameObject.Find("Level" + i.ToString() + "Objects");
            levels[i].SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMode();
        }
    }

    public void StartGame(int levelNumber)
    {
        GameObject.Find("Bubble").GetComponent<ParticleSystem>().Stop();
        cauldron.enabled = true;
        cauldron.ResetCauldron();
        openBox.Reset();
        timer.enabled = true;
        timer.RestartTimer();
        levels[levelNumber].SetActive(true);
        Spell spell = levels[levelNumber].GetComponent<Spell>();
        spell.Reset();
        spell.ingredients = GetComponentInChildren<Text>();
        cauldron.spell = spell;
        everyLevelObjects.SetActive(true);
        Time.timeScale = 1;
    }

    public void Stop()
    {
        cauldron.enabled = false;
        timer.enabled = false;
        foreach (GameObject levelObjects in levels)
        {
            levelObjects.SetActive(false);
        }
        everyLevelObjects.SetActive(false);
    }

    public void TogglePauseMode()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        Time.timeScale = Time.timeScale == 1 ? 0 : 1;
    }
}

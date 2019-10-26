using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Timer timer;
    Cauldron cauldron;
    Spell spell;
    GameObject pauseMenu;
    GameObject levelObjects;
    Box openBox;


    // Start is called before the first frame update
    void Start()
    {
        cauldron = GameObject.Find("Cauldron").GetComponent<Cauldron>();
        cauldron.enabled = false;

        spell = GetComponent<Spell>();
        spell.enabled = false;

        timer = GameObject.Find("Timer").GetComponent<Timer>();
        timer.enabled = false;

        pauseMenu = GameObject.Find("PauseMenu");
        pauseMenu.SetActive(false);

        openBox = GameObject.Find("Open Box").GetComponent<Box>();

        levelObjects = GameObject.Find("LevelObjects");
        levelObjects.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMode();
        }
    }

    public void StartGame()
    {
        GameObject.Find("Bubble").GetComponent<ParticleSystem>().Stop();
        spell.enabled = true;
        spell.Reset();
        cauldron.enabled = true;
        cauldron.ResetCauldron();
        openBox.Reset();
        timer.enabled = true;
        timer.RestartTimer();
        levelObjects.SetActive(true);
        Time.timeScale = 1;
    }

    public void Stop()
    {
        spell.enabled = false;
        cauldron.enabled = false;
        timer.enabled = false;
        levelObjects.SetActive(false);
    }

    public void TogglePauseMode()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        Time.timeScale = Time.timeScale == 1 ? 0 : 1;
    }
}

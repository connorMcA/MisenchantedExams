using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Timer timer;
    Cauldron cauldron;
    Spell spell;
    GameObject pauseMenu;

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
        spell.enabled = true;
        cauldron.enabled = true;
        timer.enabled = true;
        timer.ResetTimer();
    }

    public void TogglePauseMode()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        Time.timeScale = Time.timeScale == 1 ? 0 : 1;
    }
}

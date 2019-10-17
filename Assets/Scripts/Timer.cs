using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Author: Julianna Apicella
/// This script facilitates the timer.
/// This should be attached to a 3D Text object in the scene.
/// </summary>
public class Timer : MonoBehaviour
{
    //Time in seconds.
    double remainingTime;
    //Reference to the TextMesh component on a 3D Text Object.
    TextMesh textDisplay;

    //Get reference to cauldron to access GameOver() method.
    public Cauldron script;
    public double RemainingTime { get => remainingTime; set => remainingTime = value; }

    // Start is called before the first frame update
    void Start()
    {
        //Set timer to 10 minutes (for now).
        remainingTime = 600.0f;

        textDisplay = gameObject.GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        //Subtract time passed from total time.
        remainingTime -= Time.deltaTime;

        //Move to "Game Over" state.
        if (remainingTime <= 0)
        {
            script.GameOver();
        }

        //Display timer.
        //Debug.Log(remainingTime);
        textDisplay.text = TimeSpan.FromSeconds(remainingTime).ToString(format: @"mm\:ss");
    }
}

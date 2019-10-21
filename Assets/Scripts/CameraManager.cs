using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    Vector3 titleScreenPosition;
    Quaternion titleScreenRotation;
    Vector3 instructionsPosition;
    Quaternion instructionsRotation;
    Vector3 gamePosition;
    Quaternion gameRotation;

    Vector3 targetPosition;
    Quaternion targetRotation;
    Vector3 startPosition;
    Quaternion startRotation;
    float startTime;
    float totalTime = 1.0f;

    Camera camera;

    public enum CameraLocation
    {
        TitleScreen,
        Instructions,
        Game
    }

    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
        titleScreenPosition = new Vector3(0, -3.22f, -4.44f);
        titleScreenRotation = Quaternion.Euler(15.94f, 0, 0);

        gamePosition = new Vector3(0, -.5f, -12.25f);
        gameRotation = Quaternion.Euler(13.425f, 0, 0);

        TransitionTo("TitleScreen");

        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float percentComplete = (Time.time - startTime) / totalTime;

        if( percentComplete <= 1.0f)
        {
            camera.transform.position = Vector3.Lerp(startPosition, targetPosition, percentComplete);
            camera.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, percentComplete);
        }
    }

    public void TransitionTo(string locationName)
    {
        CameraLocation location;
        
        if (!Enum.TryParse(locationName, out location))
            return;

        startTime = Time.time;
        startPosition = camera.transform.position;
        startRotation = camera.transform.rotation;
        switch(location)
        {
            case CameraLocation.TitleScreen:

                targetPosition = titleScreenPosition;
                targetRotation = titleScreenRotation;
                break;
            case CameraLocation.Game:
                targetPosition = gamePosition;
                targetRotation = gameRotation;
                break;
            default:
                targetPosition = instructionsPosition;
                targetRotation = instructionsRotation;
                break;
        }
    }
}

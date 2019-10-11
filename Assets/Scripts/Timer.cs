using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    float time;
    // Start is called before the first frame update
    void Start()
    {
        time = 600.0f;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;

        if (time <= 0)
        {
            //whatever happens here...
        }

        Debug.Log(time);
    }

    private void OnGUI()
    {
        //how to display timer
    }
}

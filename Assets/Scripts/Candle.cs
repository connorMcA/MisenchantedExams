using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour
{
    public ParticleSystem particles;
    bool active = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Reset()
    {
        active = true;
        particles.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool isActive()
    {
        return active;
    }

    public void BlowOut()
    {
        active = false;
		// do a blow out animation
        particles.Stop();
    }
}
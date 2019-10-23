using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public InteractionType Action;


    public enum InteractionType
    {
        STIR,
        HEAT,
        COOL,
        TAP
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        Cauldron cauldron = collider.gameObject.GetComponent<Cauldron>();
        if (cauldron != null)
        {
            cauldron.AddInteraction(this);
            GetComponent<Draggable>().ResetPosition();
        }

    }
}

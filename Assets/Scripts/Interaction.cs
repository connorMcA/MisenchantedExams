using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : Draggable
{
    public InteractionType Action;
    public ParticleSystem effect;

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

    protected override void HandleCollision(Collider collider)
    {
        Cauldron cauldron = collider.gameObject.GetComponent<Cauldron>();
        if (cauldron != null)
        {
            cauldron.AddInteraction(this);
            if(effect != null)
            {
                effect.Play();
                Invoke("StopEffect", 1);
            }
        }
    }

    protected void StopEffect()
    {
        if (effect != null)
        {
            effect.Stop();
        }
    }
}

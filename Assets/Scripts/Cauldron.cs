using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    public Color liquidColor;
    public ParticleSystem liquidEffect;
    public Spell spell;
    bool isInValidState = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddIngredient(Ingredient ingredient)
    {
        if (isInValidState && spell.VerifyIngredient(ingredient))
        {
            ChangeState();
        }
        else
        {
            BlowOutCandle();
        }
    }

    public void AddInteraction(Interaction interaction)
    {
        ChangeState();
    }

    void ChangeState()
    {
        // change the color and effect of the cauldron based on something need
        // to figure out how we want to do this so that it will match with the
        // rules.
        // Make sure to set isInValidState = true; if the state goes back to
        // being clear.
    }

    void BlowOutCandle()
    {
        // We need to be able to blow out a candle when a player adds the 
        // incorrect ingredient. If all the candles are then blown out, this 
        // method is responsible for starting the end of game process.

    }
}

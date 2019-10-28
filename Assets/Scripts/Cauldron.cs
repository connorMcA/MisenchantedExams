using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Interaction;

public class Cauldron : MonoBehaviour
{
    public Color liquidColor = CLEAR;
    public ParticleSystem liquidEffect = null;
    public Spell spell;
    public List<Candle> candles;
    public GameObject liquidObject;

    bool isInValidState = true;
    Ingredient lastIngredient;
    List<Ingredient> correctIngredients = new List<Ingredient>();

    public Timer timer;

    public static Color BLUE = Color.blue;
    public static Color RED = Color.red;
    public static Color WHITE = Color.white;
    public static Color CLEAR = Color.clear;
    public List<Color> colors = new List<Color> { BLUE, RED, WHITE, CLEAR };

    Box[,] boxes = new Box[3,3];
    public List<Box> publicBoxes;

    public ParticleSystem BUBBLING;
    public ParticleSystem STEAMING;
    public ParticleSystem SPARKLING;
    public List<ParticleSystem> effects;

    public AudioSource bubblingSound;

    public int fontSize = 20;

    // Canvas object for Game Over screen
    public GameObject gameOverScreen;

    // Start is called before the first frame update
    void Start()
    {
        effects = new List<ParticleSystem> { BUBBLING, STEAMING, null };
        liquidObject.GetComponent<Renderer>().material.color = liquidColor;
        foreach (Box box in publicBoxes)
        {
            boxes[box.row, box.col] = box;
        }
        bubblingSound.Stop();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddIngredient(Ingredient ingredient)
    {
        lastIngredient = ingredient;
        if (isInValidState && spell.VerifyIngredient(ingredient))
        {
            correctIngredients.Add(ingredient);
            RandomState();
        }
        else
        {
            BlowOutCandle();
        }
    }

    private void RandomState()
    {
        ClearCauldron();

        liquidColor = colors[UnityEngine.Random.Range(0, colors.Count - 1)];
        liquidObject.GetComponent<Renderer>().material.color = liquidColor;
        int max = liquidColor == CLEAR ? effects.Count - 1 : effects.Count;
        liquidEffect = effects[UnityEngine.Random.Range(0, max)];
        if (liquidEffect != null)
        {
            liquidEffect.Play();
            if (liquidEffect == BUBBLING)
            {
                bubblingSound.Play();
            }
            else{
                if (bubblingSound.isPlaying)
                {
                    bubblingSound.Stop();
                }
            }
        }
        isInValidState = false;
    }

    public void AddInteraction(Interaction interaction)
    {
        if (liquidColor == WHITE)
        {
            ChangeFromWhite(interaction);
        }
        else if (liquidColor == RED)
        {
            ChangeFromRed(interaction);
        }
        else if (liquidColor == BLUE)
        {
            ChangeFromBlue(interaction);
        }
        else
        {
            ChangeFromClear(interaction);
        }
    }

    private void ChangeFromClear(Interaction interaction)
    {
        int ingredientNum = spell.CurrentIngredientIdx;
        switch (interaction.Action)
        {
            case InteractionType.TAP:
                if (liquidEffect == STEAMING && (ingredientNum == 0 || ingredientNum == 2))
                {
                    ClearCauldron();
                }
                else
                {
                    RandomState();
                }
                break;
            case InteractionType.HEAT:
                if ((liquidEffect == SPARKLING && ingredientNum == 0) || (liquidEffect == BUBBLING && ingredientNum == 1))
                {
                    ClearCauldron();
                }
                else
                {
                    RandomState();
                }
                break;
            case InteractionType.COOL:
                if ((liquidEffect == SPARKLING && ingredientNum == 1) || (liquidEffect == BUBBLING && ingredientNum == 2))
                {
                    ClearCauldron();
                }
                else
                {
                    RandomState();
                }
                break;
            case InteractionType.STIR:
                if ((liquidEffect == BUBBLING && ingredientNum == 0) || (liquidEffect == STEAMING && ingredientNum == 1)
                    || (liquidEffect == SPARKLING && ingredientNum == 2))
                {
                    ClearCauldron();
                }
                else
                {
                    RandomState();
                }
                break;
            default:
                RandomState();
                break;
        }
    }

    private void ChangeFromBlue(Interaction interaction)
    {
        if (liquidEffect == BUBBLING)
        {
            if ((interaction.Action == InteractionType.STIR && boxes[0, 0] != null && boxes[2, 2] != null))
            {
                ClearCauldron();
            }
            else if (interaction.Action == InteractionType.TAP && spell.requiredIngredients.Count > 3)
            {
                ClearCauldron();
            }
            else if (interaction.Action == InteractionType.COOL && lastIngredient != null && lastIngredient.name.ToLower().Contains("w"))
            {
                ClearCauldron();
            }
            else if (interaction.Action == InteractionType.HEAT)
            {
                ClearCauldron();
            }
            else
            {
                RandomState();
            }
        }
        else if (liquidEffect == STEAMING)
        {
            if (interaction.Action == InteractionType.HEAT)
            {
                int top = 0;
                int bottom = 0;
                for (int i = 0; i < 3; i++)
                {
                    if (boxes[0,i] != null)
                    {
                        top++;
                    }
                    if (boxes[2,i] != null)
                    {
                        bottom++;
                    }
                }
                if (top == bottom)
                {
                    ClearCauldron();
                }
                else
                {
                    RandomState();
                }
            }
            else if (interaction.Action == InteractionType.COOL && correctIngredients.Count > 0 && correctIngredients[0].box.row == 2)
            {
                ClearCauldron();
            }
            else if (interaction.Action == InteractionType.TAP && spell.requiredIngredients.Count - spell.CurrentIngredientIdx < 2)
            {
                ClearCauldron();
            }
            else if (interaction.Action == InteractionType.STIR)
            {
                ClearCauldron();
            }
            else
            {
                RandomState();
            }
        }
        else if (liquidEffect == SPARKLING)
        {
            if (interaction.Action == InteractionType.TAP && lastIngredient != null && lastIngredient.name.ToLower().Contains("hair"))
            {
                ClearCauldron();
            }
            else if (interaction.Action == InteractionType.HEAT && publicBoxes.Count % 2 == 1)
            {
                ClearCauldron();
            }
            else if (interaction.Action == InteractionType.STIR && correctIngredients.Count > 0)
            {
                Box box = correctIngredients[0].box;
                bool blueVial = false;
                foreach (Ingredient ingredient in box.ingredients)
                {
                    if (ingredient.vialColor == Color.blue)
                    {
                        blueVial = true;
                    }
                }
                if (blueVial)
                {
                    RandomState();
                }
                else
                {
                    ClearCauldron();
                }
            }
            else if (interaction.Action == InteractionType.COOL)
            {
                ClearCauldron();
            }
            else
            {
                RandomState();
            }
        }
        else
        {
            if (interaction.Action == InteractionType.COOL)
            {
                int count = 0;
                for (int i = 0; i < 3; i++)
                {
                    if (boxes[2,i] != null)
                    {
                        count++;
                    }
                }
                if (lastIngredient != null && lastIngredient.box.row == 1 && count == 2)
                {
                    ClearCauldron();
                }
                else
                {
                    RandomState();
                }
            }
            else if (interaction.Action == InteractionType.TAP && correctIngredients.Count > 0 && correctIngredients[0].vialColor == WHITE)
            {
                ClearCauldron();
            }
            else if (interaction.Action == InteractionType.STIR && spell.requiredIngredients.Count % 2 == 1)
            {
                ClearCauldron();
            }
            else if (interaction.Action == InteractionType.HEAT)
            {
                ClearCauldron();
            }
            else
            {
                RandomState();
            }
        }

    }

    private void ChangeFromRed(Interaction interaction)
    {
        if(liquidEffect == BUBBLING)
        {
            List<int> ids = new List<int>{12, 11, 7, 10};
            InteractionType[] actions = { InteractionType.HEAT, InteractionType.TAP, InteractionType.COOL, InteractionType.STIR };
            RedHelper(ids, actions, 2, interaction);
        }
        else if (liquidEffect == STEAMING)
        {
            List<int> ids = new List<int>{13, 14, 5, 0};
            InteractionType[] actions = { InteractionType.COOL, InteractionType.STIR, InteractionType.TAP, InteractionType.HEAT };
            RedHelper(ids, actions, 1, interaction);
        }
        else if (liquidEffect == SPARKLING)
        {
            List<int> ids = new List<int>{1, 3, 8, 4};
            InteractionType[] actions = { InteractionType.TAP, InteractionType.HEAT, InteractionType.STIR, InteractionType.COOL };
            RedHelper(ids, actions, 0, interaction);
        }
        else
        {
            // bottom right, vertical then left
            List<int> ids = new List<int> { 0, 1, 3, 4, 5, 2, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15};
            InteractionType[] actions = { InteractionType.STIR, InteractionType.TAP, InteractionType.COOL, InteractionType.HEAT };
            bool found = false;
            for(int i = 2; i >=0; i--)
            {
                if (found) break;
                for(int j = 2; j >=0; j--)
                {
                    if(boxes[i, j] != null && ids.Contains(boxes[i, j].symbolId))
                    {
                        if (actions[(int)Math.Floor((double)ids.IndexOf(boxes[i,j].symbolId) / 4.0)] == interaction.Action)
                        {
                            ClearCauldron();
                        }
                        else
                        {
                            RandomState();
                        }
                        found = true;
                        break;
                    }
                }
            }

        }
    }

    private void RedHelper(List<int> ids, InteractionType[] actions, int rowId, Interaction interaction)
    {
        for(int i = 0; i < 3; i++)
        {
            if(boxes[rowId, i] != null && ids.Contains(boxes[rowId, i].symbolId))
            {
                if (actions[ids.IndexOf(boxes[rowId, i].symbolId)] == interaction.Action)
                {
                    ClearCauldron();
                }
                else
                {
                    RandomState();
                }
                break;
            }
        }
    }

    private void ChangeFromWhite(Interaction interaction) {

        string seconds = (Math.Truncate(timer.RemainingTime % 60 * 1000) / 1000).ToString();
        string minutes = Math.Truncate(timer.RemainingTime / 60).ToString();
        switch (interaction.Action)
        {
            case InteractionType.TAP:
                if (liquidEffect == SPARKLING && (seconds.Contains("9") || minutes.Contains("9")))
                {
                    ClearCauldron();
                }
                else
                {
                    RandomState();
                }
                break;
            case InteractionType.HEAT:
                if (liquidEffect == BUBBLING && (seconds.Contains("3") || minutes.Contains("3")))
                {
                    ClearCauldron();
                }
                else
                {
                    RandomState();
                }
                break;
            case InteractionType.COOL:
                if(liquidEffect == STEAMING && (seconds.Contains("5") || minutes.Contains("5")))
                {
                    ClearCauldron();
                }
                else
                {
                    RandomState();
                }
                break;
            case InteractionType.STIR:
                if (liquidEffect == null && (seconds.Contains("2") || minutes.Contains("2")))
                {
                    ClearCauldron();
                }
                else
                {
                    RandomState();
                }
                break;
            default:
                RandomState();
                break;
        }
    }

    public void SprigganAttack()
    {
        BlowOutCandle();
    }

    private void ClearCauldron()
    {
        if (liquidEffect != null)
        {
            liquidEffect.Stop();
            if (bubblingSound.isPlaying)
            {
                bubblingSound.Stop();
            }
        }
        liquidEffect = null;
        liquidColor = CLEAR;
        isInValidState = true;
        liquidObject.GetComponent<Renderer>().material.color = liquidColor;

    }

    void BlowOutCandle()
    {
        // We need to be able to blow out a candle when a player adds the
        // incorrect ingredient. If all the candles are then blown out, this
        // method is responsible for starting the end of game process.
        for (int i = 0; i < candles.Count; i++)
        {
            if (candles[i].isActive())
            {
                candles[i].BlowOut();
                // if its the last candle, games over
                if (i == candles.Count - 1)
                {
                    GameOver();
                }
                return;
            }
        }
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
    }

    public void ResetCauldron() {
        ClearCauldron();
        lastIngredient = null;
        foreach (Box b in publicBoxes)
        {
            b.Reset();
        }
        foreach(Candle c in candles)
        {
            c.Reset();
        }
    }

    void OnGUI()
    {
        
    }
}

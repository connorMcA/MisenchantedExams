using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{

    public List<Ingredient> requiredIngredients;
    public string spellName;
    public Material symbol;
    private int currentIngredientIdx;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // display ingredients on spell board
        // cross off any that are already done
    }

    // verifies whether the ingredient is the correct ingredient to be added at this point
    // if true, it will also move on to the next expected ingredient
    public bool VerifyIngredient(Ingredient ingredient)
    {
        if(ingredient.ingredientName.Equals(requiredIngredients[currentIngredientIdx].ingredientName))
        {
            currentIngredientIdx++;
            return true;
        }
        return false;
    }

    void OnGUI()
    {
        GUI.Box(new Rect(0, 0, 100, 100), spellName);
    }
}

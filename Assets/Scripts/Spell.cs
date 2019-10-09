using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{

    public List<Ingredient> RequiredIngredients;
    public string Name;
    public Material Symbol;
    private int CurrentIngredientIdx;

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
    bool VerifyIngredient(Ingredient ingredient)
    {
        if(ingredient.Name.Equals(RequiredIngredients[CurrentIngredientIdx].Name))
        {
            CurrentIngredientIdx++;
            return true;
        }
        return false;
    }
}

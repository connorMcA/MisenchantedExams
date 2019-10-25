using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spell : MonoBehaviour
{

    public List<Ingredient> requiredIngredients;
    public string spellName;
    public Material symbol;
    public int fontSize = 30;
    int currentIngredientIdx;

    public int CurrentIngredientIdx { get => currentIngredientIdx; set => currentIngredientIdx = value; }

    // Canvas object for victory screen
    public GameObject victoryScreen;
    public GameObject victoryScreenText;
    Text title;

    // Text for ingredients.
    public Text ingredients;

    // Start is called before the first frame update
    void Start()
    {
        title = victoryScreenText.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        // display ingredients on spell board
        // cross off any that are already done
        if (CurrentIngredientIdx == requiredIngredients.Count) {
            title.text = "You Won!";
            victoryScreen.SetActive(true);
            ingredients.text = "";
        }
    }

    public void Reset()
    {
        currentIngredientIdx = 0;
    }

    // verifies whether the ingredient is the correct ingredient to be added at this point
    // if true, it will also move on to the next expected ingredient
    public bool VerifyIngredient(Ingredient ingredient)
    {
        if(ingredient.ingredientName.Equals(requiredIngredients[CurrentIngredientIdx].ingredientName))
        {
            ingredients.text += "Added " + requiredIngredients[CurrentIngredientIdx].ingredientName + "!\n";
            CurrentIngredientIdx++;
            return true;
        }
        return false;
    }

    void OnGUI()
    {
        GUIStyle guiStyle = new GUIStyle();
        guiStyle.fontSize = fontSize;
        guiStyle.normal.textColor = Color.white;
        GUI.Box(new Rect(10, 10, 210, 210), spellName, guiStyle);
    }
}

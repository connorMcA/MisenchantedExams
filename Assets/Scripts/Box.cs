using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// author: Aaron Feld
/// This script functions to regiulate the box objects on the page including keeping track of the ingrediants inside, and haveing a method for "opening" the box
/// should be attached to Box components
/// </summary>

public class Box : MonoBehaviour
{
    // list of ingredients in the box
    public List<Ingredient> ingredients;
    // name will be the 
    public string name;

    // each box object has a property saying whether the box is open or not - either a single box is open, in which cases all other boxes are set to closed, or 
    bool isOpen = false;

    // seter for whether the box is open or not
    public bool IsOpen
    {
        set { isOpen = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnGUI()
    {

        if (isOpen) {
            for (int i = 0; i < ingredients.Count; i++) {
                GUI.Box(new Rect(Screen.width - 50 - i * 10, Screen.height - 50 - i*10, Screen.width, Screen.height), ingredients[i].name);
            }
        }
    }
}

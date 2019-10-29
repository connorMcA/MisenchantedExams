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
    public int symbolId;
    public int row;
    public int col;
    public Box openBox;
    public bool isOpenBox;
    public List<Ingredient> openBoxIngredients;

    // box opening sound
    public AudioSource boxOpenSound;


    // each box object has a property saying whether the box is open or not - either a single box is open, in which cases all other boxes are set to closed, or
    bool isOpen = false;
    private Vector3[] positions = {
        new Vector3(.0035f, .0159f, -.0035f),
        new Vector3(-.0243f, .0159f, -.0035f),
        new Vector3(-.0506f, .0159f, -.0035f)
        };
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

    public void Reset()
    {
        if (isOpenBox)
        {
            ingredients[0] = openBoxIngredients[0];
            ingredients[1] = openBoxIngredients[1];
            ingredients[2] = openBoxIngredients[2];
        }

        for (int i = 0; i < ingredients.Count; i++)
        {
            ingredients[i].transform.parent = ingredients[i].box.transform;
            ingredients[i].transform.localPosition = positions[i];
        }
        
    }

    void OnMouseDown()
    {
        if (!isOpenBox && ingredients[0] != openBox.ingredients[0])
        {
            boxOpenSound.Play();
            for (int i = 0; i < ingredients.Count; i++)
            {
                ingredients[i].GetComponent<Draggable>().enabled = true;
                ingredients[i].transform.parent = openBox.transform;
                ingredients[i].transform.localPosition = positions[i];
                openBox.ingredients[i].GetComponent<Draggable>().enabled = false;
                openBox.ingredients[i].transform.parent = openBox.ingredients[i].box.transform.parent;
                openBox.ingredients[i].transform.localPosition = positions[i];
                openBox.ingredients[i] = ingredients[i];
            }
        }
    }

}

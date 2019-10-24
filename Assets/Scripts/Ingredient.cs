using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : Draggable
{

    public string ingredientName;
    public Box box;
    public Color vialColor;
    public Mesh vialShape;

    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material.color = vialColor;
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetIngrenientPosition()
    {
        transform.position = startPosition;
    }

    protected override void HandleCollision(Collider collider)
    {
        Cauldron cauldron = collider.gameObject.GetComponent<Cauldron>();
        if (cauldron != null)
        {
            cauldron.AddIngredient(this);
        }
    }
}

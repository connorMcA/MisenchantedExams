using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    // starting position
    Vector3 startingPos;
    bool stillDraggable = true;

    // reference to cauldron
    public GameObject cauldron;

    // Stores object's position prior to dragging.
    private void OnMouseDown()
    {
        startingPos = gameObject.transform.position;
        stillDraggable = true;
    }

    // Sets object position to mouse point.
    void OnMouseDrag()
    {
        if (stillDraggable)
        {
            float distance_to_screen = Camera.main.WorldToScreenPoint(cauldron.transform.position).z;
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));
        }
    }

    // Resets position when let go.
    private void OnMouseUp()
    {
        transform.position = startingPos;
    }

    public void ResetPosition()
    {
        transform.position = startingPos;
        stillDraggable = false;
    }
}

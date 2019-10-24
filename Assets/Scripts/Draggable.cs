using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Draggable : MonoBehaviour
{
    // starting position
    Vector3 startingPos;
    bool stillDraggable = true;
    bool colliding = false;
    Collider collider;

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
            float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));
        }
    }

    // Resets position when let go.
    private void OnMouseUp()
    {
        if(colliding)
        {
            HandleCollision(collider);
        }
        ResetPosition();
    }

    void OnTriggerEnter(Collider collider)
    {
        colliding = true;
        this.collider = collider;
    }

    void OnTriggerExit(Collider collider)
    {
        colliding = false;
        this.collider = null;
    }


    public void ResetPosition()
    {
        transform.position = startingPos;
        stillDraggable = false;
    }

    protected abstract void HandleCollision(Collider collider);
}

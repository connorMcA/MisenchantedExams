using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spriggan : MonoBehaviour
{

    Vector3[] positions = {
        new Vector3(23.06f, -6.07f, 14.63f),
        new Vector3(11.44f, -6.07f, 14.63f),
        new Vector3(4.18f, 5.85f, 23.4f),
        new Vector3(-5.53f, 5.85f, 23.4f),
        new Vector3(-7.66f,-6.94f, 16.57f),
        new Vector3(-7.66f,-6.94f, 16.57f),
        new Vector3(0, -5f, 3.14f)};

    float[] timeToMoveToNextPosition = {
        5.0f,
        .5f,
        3f,
        .5f,
        1f,
        .5f
        };
    float currentTransitionTime;


    double delay;
    double MAX_DELAY = 10;

    int positionIndex;

    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        if (delay > 0)
        {
            delay -= Time.deltaTime;
        }
        else if(currentTransitionTime <= timeToMoveToNextPosition[positionIndex])
        {
            currentTransitionTime += Time.deltaTime;
            float percent = currentTransitionTime / timeToMoveToNextPosition[positionIndex];
            transform.position = Vector3.Lerp(positions[positionIndex], positions[positionIndex + 1], percent);
        }
        else
        {
            positionIndex++;
            if (positionIndex >= positions.Length)
            {
                Reset();
                return;
            }
            Vector3 target = transform.position - (transform.position != positions[positionIndex + 1] ? positions[positionIndex + 1] : positions[positionIndex + 2]);
            target.y = 0;
            transform.rotation = Quaternion.LookRotation(target);
            currentTransitionTime = 0;
        }
    }

    void Reset()
    {
        delay = MAX_DELAY;
        positionIndex = 0;
        currentTransitionTime = 0;
        transform.position = positions[0];
        transform.rotation = Quaternion.LookRotation(transform.position - positions[positionIndex + 1]);
    }

    private void OnMouseDown()
    {
        Reset();
    }

    void OnTriggerEnter(Collider collider)
    {
        Cauldron cauldron = collider.gameObject.GetComponent<Cauldron>();
        if (cauldron != null)
        {
            cauldron.SprigganAttack();
            Reset();
        }
        
    }
}

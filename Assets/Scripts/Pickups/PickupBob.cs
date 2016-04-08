using UnityEngine;
using System.Collections;

public class PickupBob: MonoBehaviour
{
    public float rotationSpeed = 10;
    public float bobHeight = 10;
    float timer = 0;
    Vector3 defaultPosition;
    // Use this for initialization
    void Start()
    {
        defaultPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        transform.Rotate(Vector3.forward, Time.deltaTime * rotationSpeed);
        if (timer <= 1)
        {
            transform.position = new Vector3(transform.position.x, defaultPosition.y + timer * bobHeight);
        }
        else if (timer <= 2)
        {
            transform.position = new Vector3(transform.position.x, defaultPosition.y + 1 * bobHeight - (timer - 1) * bobHeight);
        }
        else
        {
            timer = 0;
        }
    }
}

﻿using UnityEngine;
using System.Collections;

public class Waypoint : MonoBehaviour
{
    // The start waypoint, this is initialized in Awake.
    // This variable is static thus all instances
    // of the waypoint script share it.
    static Waypoint start;
    // The next waypoint, this variable needs to be
    // assigned in the inspector.
    // You can select all waypoints to see the
    // full waypoint path.
    public Waypoint next;
    // This determines where the start waypoint is.
    public bool isStart = false;
    // Returns where the AI should drive towards.
    // position is the current position of the car.
    Vector3 CalculateTargetPosition(Vector3 position)
    {
        // If we are getting close to the waypoint,
        // we return the next waypoint.
        // This gives us better car behaviour when
        // cars don’t exactly hit the waypoint
        if (Vector3.Distance(transform.position, position)
        < 6)
        {
            return next.transform.position;
        }
        // We are still far away from the next waypoint,
        // just return the waypoints position
        else {
            return transform.position;
        }
    }
    // This initializes the start and goal static variables.
    // We have to inside Awake because the waypoints need
    // to be initialized before the AI scripts use it
    // All Awake function are always called before all
    // Start functions.
    void Awake()
    {
        if (!next)
            Debug.Log("This waypoint is not connected,you need to set the next waypoint!", this);
        if (isStart)
            start = this;
    }
    // Draw the waypoint lines only when you select
    // one of the waypoints
    void OnDrawGizmos()
    {
        if (next)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, next.transform.
            position);

        }
    }
}

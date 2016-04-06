using UnityEngine;
using System.Collections;

public class AIMain : MonoBehaviour
{
    // These variables are just for applying torque to the wheels and shifting gears.
    // using the defined Max and Min Engine RPM, the script can determine what gear the
    // car needs to be in.
    Rigidbody playersRigidbody;
    public WheelCollider[] drivingWheels;
    public WheelCollider[] steeringWheels;
    public float accelerationPower = 100;
    public float brakingPower = 12;
    public float maxSpeed = 20;
    public float turnSpeed = 2.5f;
    public float angularSpeedMax = 2;
    public GameObject cameraPoint;
    public GameObject centerOfMass;

    Vector3 com;
    Vector3 rotation = new Vector3(0, 100, 0);

    public float actualSpeed;
    float actualSqrSpeed;
    Vector3 angularSpeed;

    float defaultAccelerationPower;
    float defaultBrakingPower;
    float defaultMaxSpeed;
    float defaultTurnSpeed;

    public GameObject groundCheck;

    // Here's all the variables for the AI, the waypoints are determined in the "GetWaypoints" function.
    // the waypoint container is used to search for all the waypoints in the scene, and the current
    // waypoint is used to determine which waypoint in the array the car is aiming for.
    public Transform waypointContainer;
    Transform[] waypoints = null;
    private int currentWaypoint = 0;
    float randomDistance = 0;
    // input steer and input torque are the values substituted out for the player input. The 
    // "NavigateTowardsWaypoint" function determines values to use for these variables to move the car
    // in the desired direction.
    private float inputSteer = 0.0f;
    private float inputTorque = 0.0f;

    void Start()
    {


        foreach (WheelCollider wheel in drivingWheels)
        {
            wheel.ConfigureVehicleSubsteps(5, 15, 80);
            wheel.ConfigureVehicleSubsteps(20, 80, 160);
        }
        foreach (WheelCollider wheel in steeringWheels)
        {
            wheel.ConfigureVehicleSubsteps(5, 15, 80);
            wheel.ConfigureVehicleSubsteps(20, 80, 80);
        }
        playersRigidbody = GetComponent<Rigidbody>();
        defaultMaxSpeed = maxSpeed;
        defaultTurnSpeed = turnSpeed;
        defaultBrakingPower = brakingPower;
        defaultAccelerationPower = accelerationPower;
        playersRigidbody.centerOfMass -= new Vector3(0, 0.9f, 0f);
    }

    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
            return;
        }

        Transform visualWheel = collider.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }

    void FixedUpdate()
    {
        // Call the funtion to determine the desired input values for the car. This essentially steers and
        // applies gas to the engine.
        NavigateTowardsWaypoint();

        float motor = 0;
        motor = accelerationPower * inputTorque;

        float steering = turnSpeed * inputSteer;

        foreach (WheelCollider wheel in drivingWheels)
        {
            if (motor > 0)
            {
                wheel.brakeTorque = 0f;
                playersRigidbody.drag = 0f;
                wheel.motorTorque = motor;
            }
            else if (motor < 0)
            {
                wheel.brakeTorque = 0f;
                playersRigidbody.drag = 0f;
                wheel.motorTorque = motor * brakingPower;
            }
            else
            {
                playersRigidbody.drag = 1f;
                if (playersRigidbody.velocity.magnitude > 0f)
                {
                    wheel.motorTorque = -brakingPower * 2f;
                }
            }

            ApplyLocalPositionToVisuals(wheel);
        }
        foreach (WheelCollider wheel in steeringWheels)
        {
            wheel.steerAngle = steering;
            ApplyLocalPositionToVisuals(wheel);
        }

        actualSpeed = playersRigidbody.velocity.magnitude;
        actualSqrSpeed = playersRigidbody.velocity.sqrMagnitude;
        angularSpeed = playersRigidbody.angularVelocity;

    }

    void GetWaypoints()
    {
        // Now, this function basically takes the container object for the waypoints, then finds all of the transforms in it,
        // once it has the transforms, it checks to make sure it's not the container, and adds them to the array of waypoints.
        Transform[] potentialWaypoints = waypointContainer.GetComponentsInChildren<Transform>();
        for (int i = 0; i < potentialWaypoints.Length; i++)
        {
            waypoints[i] = potentialWaypoints[i];
        }
    }

    public void SetWaypoints(Transform[] waypointsArray)
    {
        waypoints = waypointsArray;
    }

    void Update()
    {
        if (playersRigidbody.velocity.magnitude >= maxSpeed)
        {
            playersRigidbody.velocity = playersRigidbody.velocity.normalized * maxSpeed;
        }
    }

    void NavigateTowardsWaypoint()
    {
        //if (waypoints != null)
        //{
        // now we just find the relative position of the waypoint from the car transform,
        // that way we can determine how far to the left and right the waypoint is.
        Vector3 RelativeWaypointPosition = transform.InverseTransformPoint(new Vector3(
                                                    waypoints[currentWaypoint].position.x,
                                                    transform.position.y,
                                                    waypoints[currentWaypoint].position.z));


        // by dividing the horizontal position by the magnitude, we get a decimal percentage of the turn angle that we can use to drive the wheels
        inputSteer = RelativeWaypointPosition.x / RelativeWaypointPosition.magnitude + randomDistance;

        // now we do the same for torque, but make sure that it doesn't apply any engine torque when going around a sharp turn...
        if (Mathf.Abs(inputSteer) < 0.5)
        {
            inputTorque = RelativeWaypointPosition.z / RelativeWaypointPosition.magnitude;
        }
        else {
            if (playersRigidbody.velocity.magnitude > 1)
            {
                inputTorque = 0.0f;
            }
            else
            {
                inputTorque = 0.7f;
            }
        }

        // this just checks if the car's position is near enough to a waypoint to count as passing it, if it is, then change the target waypoint to the
        // next in the list.
        if (RelativeWaypointPosition.magnitude < 1)
        {
            currentWaypoint++;
            randomDistance = Random.Range(-0.1f, 0.1f);
            if (currentWaypoint >= waypoints.Length)
            {
                currentWaypoint = 0;
            }
        }
        // }
    }
}

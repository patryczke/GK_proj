using UnityEngine;
using System.Collections;

public class CarControllerWheelCollider : MonoBehaviour {

    Rigidbody playersRigidbody;
    public WheelCollider[] drivingWheels;
    public WheelCollider[] steeringWheels;
    public float accelerationPower = 100;
    public float brakingPower = 12;
    public float maxSpeed = 20;
    public float turnSpeed = 2.5f;
    public GameObject cameraPoint;

    public float actualSpeed;
    float actualSqrSpeed;
    Vector3 angularSpeed;

    float defaultAccelerationPower;
    float defaultBrakingPower;
    float defaultMaxSpeed;
    float defaultTurnSpeed;

    bool grounded;
    // Use this for initialization
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
        float motor = 0;
        motor = accelerationPower * drive();

        float steering = turnSpeed * turn();

        foreach(WheelCollider wheel in drivingWheels)
        {
            if(motor > 0)
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
                if (isGroundedWheels())
                {
                    playersRigidbody.drag = 1f;
                }
                else
                {
                    playersRigidbody.drag = 0f;
                }
                if (playersRigidbody.velocity.magnitude > 0f)
                {
                    wheel.motorTorque = -brakingPower * 2f;
                }
                //wheel.brakeTorque = 1200f;
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
       
        grounded = isGroundedWheels();
        if (!isGroundedWheels())
        {
            playersRigidbody.angularVelocity = Vector3.zero;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (playersRigidbody.velocity.magnitude >= maxSpeed)
        {
            playersRigidbody.velocity = playersRigidbody.velocity.normalized * maxSpeed;
        }
        /*
        if (turn() != 0)
        {
            playersRigidbody.AddTorque(transform.up * Input.GetAxis("Horizontal") * turnSpeed * 10);
        }
        */
    }


    public float turn()
    {
        return Input.GetAxis("Horizontal");
    }

    public float drive()
    {
        return Input.GetAxis("Vertical");
    }

    void OnCollisionEnter(Collision col)
    {
        /*
        if (col.gameObject.GetComponent<GroundType>())
        {
            if (col.gameObject.GetComponent<GroundType>().type == GroundTypes.Grass)
            {
                accelerationPower = defaultAccelerationPower / 8;
                brakingPower = defaultBrakingPower * 0.9f;
                turnSpeed = defaultTurnSpeed / 1.5f;
                maxSpeed = defaultMaxSpeed;
            }
            else
            {
                accelerationPower = defaultAccelerationPower;
                brakingPower = defaultBrakingPower;
                turnSpeed = defaultTurnSpeed;
                maxSpeed = defaultMaxSpeed;
            }
        }
        */
    }

    public bool isGroundedWheels()
    {
        WheelHit hit;
        foreach (WheelCollider wheel in drivingWheels) { 
            if (wheel.GetGroundHit(out hit))
            {
                return true;
            }
        }
        return false;
    }
}

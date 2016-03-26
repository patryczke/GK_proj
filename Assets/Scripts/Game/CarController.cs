using UnityEngine;
using System.Collections;

public class CarController : MonoBehaviour
{
    Rigidbody playersRigidbody;
    public float accelerationPower = 100;
    public float brakingPower = 12;
    public float maxSpeed = 20;
    public float turnSpeed = 2.5f;
    public float angularSpeedMax = 2;
    public GameObject cameraPoint;

    Vector3 rotation = new Vector3(0, 100, 0);

    public float actualSpeed;
    float actualSqrSpeed;
    Vector3 angularSpeed;

    float defaultAccelerationPower;
    float defaultBrakingPower;
    float defaultMaxSpeed;
    float defaultTurnSpeed;

    public GameObject groundCheck;


    // Use this for initialization
    void Start()
    {
        playersRigidbody = GetComponent<Rigidbody>();
        defaultMaxSpeed = maxSpeed;
        defaultTurnSpeed = turnSpeed;
        defaultBrakingPower = brakingPower;
        defaultAccelerationPower = accelerationPower;
    }

    void FixedUpdate()
    {
        if (isGrounded())
        {
            actualSpeed = playersRigidbody.velocity.magnitude;
            actualSqrSpeed = playersRigidbody.velocity.sqrMagnitude;
            angularSpeed = playersRigidbody.angularVelocity;
            if (drive() != 0)
            {
                if (drive() > 0)
                {
                    playersRigidbody.AddForce(transform.forward * Input.GetAxis("Vertical") * accelerationPower);
                }
                else
                {
                    playersRigidbody.AddForce(transform.forward * Input.GetAxis("Vertical") * brakingPower);
                }
            }
            if ((angularSpeed.y >= angularSpeedMax) || (angularSpeed.y <= -angularSpeedMax))
            {
                if (angularSpeed.y > 0)
                {
                    angularSpeed.y = angularSpeedMax;
                }
                else
                {
                    angularSpeed.y = -angularSpeedMax;
                }
                playersRigidbody.angularVelocity = new Vector3(angularSpeed.x, angularSpeed.y, angularSpeed.z);
            }
        }
        else
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
        if (turn() != 0)
        {
            playersRigidbody.AddTorque(transform.up * Input.GetAxis("Horizontal") * turnSpeed * 10);
        }
    }


    float turn()
    {
        return Input.GetAxis("Horizontal");
    }

    float drive()
    {
        return Input.GetAxis("Vertical");
    }

    void OnCollisionEnter(Collision col)
    {
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
    }

    public bool isGrounded()
    {
        if (!Physics.Linecast(playersRigidbody.position, groundCheck.transform.position))
        {
            return false;
        }
        return true;
    }
}

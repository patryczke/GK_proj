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
    Vector3 targetRotation;

    public float actualSpeed;
    float actualSqrSpeed;
    Vector3 angularSpeed;

    float defaultAccelerationPower;
    float defaultBrakingPower;
    float defaultMaxSpeed;
    float defaultTurnSpeed;
    float defaultDrag;

    public GameObject groundCheck;
    bool grounded = false;

    // Use this for initialization
    void Start()
    {
        targetRotation = transform.rotation.eulerAngles;
        playersRigidbody = GetComponent<Rigidbody>();
        defaultMaxSpeed = maxSpeed;
        defaultTurnSpeed = turnSpeed;
        defaultBrakingPower = brakingPower;
        defaultAccelerationPower = accelerationPower;
        defaultDrag = playersRigidbody.drag;
        playersRigidbody.centerOfMass -= new Vector3(0, 1.0f, 0);
    }

    void FixedUpdate()
    {
        if (isGrounded())
        {
            playersRigidbody.AddForce(-transform.up*100f);
            actualSpeed = playersRigidbody.velocity.magnitude;
            actualSqrSpeed = playersRigidbody.velocity.sqrMagnitude;
            angularSpeed = playersRigidbody.angularVelocity;
            if (drive() != 0)
            {
                playersRigidbody.drag = defaultDrag;
                if (drive() > 0)
                {
                    playersRigidbody.AddForce(transform.forward * Input.GetAxis("Vertical") * accelerationPower);
                }
                else
                {
                    playersRigidbody.AddForce(transform.forward * Input.GetAxis("Vertical") * brakingPower);
                }
            }
            else
            {
                playersRigidbody.drag = defaultDrag / 20f;
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
            if (playersRigidbody.velocity.magnitude >= maxSpeed)
            {
                playersRigidbody.velocity = playersRigidbody.velocity.normalized * maxSpeed;
            }
            if (turn() != 0)
            {
                playersRigidbody.AddTorque(transform.up * turn() * turnSpeed * 10);
            }
        }
        else
        {
            //playersRigidbody.angularVelocity = Vector3.zero;
            playersRigidbody.drag = 0;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(targetRotation.x,transform.eulerAngles.y,targetRotation.z), Time.fixedDeltaTime * 10f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        grounded = isGrounded();
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
                accelerationPower = defaultAccelerationPower / 2;
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
        if (!Physics.Linecast(transform.position, groundCheck.transform.position))
        {
            return false;
        }
        return true;
    }
}

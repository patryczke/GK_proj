using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{
    float speedValue = 0.0f;
    float speedPointerAngle = POINTER_INIT_Z_ROTATION;

    Text speedTextValue;
    CarController car;
    CarControllerWheelCollider carW;
    CameraFollow mainCamera;

    private Transform speedometerDigitalValueCmp;
    private Transform speedometerPointerCmp;

    private static readonly float POINTER_INIT_Z_ROTATION = 262.0f;
    private static readonly float POINTER_MAX_Z_ROTATION = 82.0f;
    private static readonly float POINTER_MAX_SPEED_LOCAL = 60.0f;

    private static readonly string CHILD_CMP_SPEEDOMETER_DIG_VAL = "SpeedometerDigitalValue";
    private static readonly string CHILD_CMP_SPEEDOMETER_POINTER = "SpeedometerPointer";
    

    private Vector3 pointerCurrentAngle;

    void Start() {
        speedometerDigitalValueCmp = gameObject.transform.FindChild(CHILD_CMP_SPEEDOMETER_DIG_VAL);
        speedometerPointerCmp = gameObject.transform.FindChild(CHILD_CMP_SPEEDOMETER_POINTER);

        if (speedometerDigitalValueCmp == null || speedometerPointerCmp == null) {
            Debug.LogError("Not found speedometer components!");
            return;
        }

        speedTextValue = speedometerDigitalValueCmp.GetComponent<Text>();
        speedometerPointerCmp.transform.eulerAngles.Set(0.0f, 0.0f, POINTER_INIT_Z_ROTATION);

        mainCamera = FindObjectOfType<CameraFollow>();
        carW = mainCamera.target.transform.parent.GetComponent<CarControllerWheelCollider>();
        speedValue = carW.actualSpeed;
    }
    void Update() {
        if (carW) {
            speedValue = carW.actualSpeed;
        }

        speedTextValue.text = ((int)speedValue).ToString();

        float newAngle = (POINTER_MAX_Z_ROTATION - POINTER_INIT_Z_ROTATION) / (POINTER_MAX_SPEED_LOCAL) * (speedValue) + POINTER_INIT_Z_ROTATION;
        Vector3 temp = speedometerPointerCmp.rotation.eulerAngles;
        temp.z = newAngle;
        speedometerPointerCmp.rotation = Quaternion.Euler(temp);
    }
}

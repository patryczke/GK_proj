using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CustomPickupCollider : MonoBehaviour {
    public static readonly string PICKUPABLE_GAMEOBJECTS_TAG = "PickupubleObjectTag";

    void Start () {
	}

    void OnDestroy() {
        Destroy(transform.parent.gameObject);
    }

    void FixedUpdate () {
        GameObject closestObj = FindClosestPickupableObject();
        if (closestObj != null) {
            bool isCollisionEnter = CheckCustomCollision(closestObj);
            if (isCollisionEnter) {
                //TODO: do custom stuff on collision detection
                OnCollisionEnterCustom(closestObj);
            }
        }

    }

    private GameObject FindClosestPickupableObject() {
        GameObject closestObject = null;
        GameObject[] withTagObjects = GameObject.FindGameObjectsWithTag(PICKUPABLE_GAMEOBJECTS_TAG);

        if (withTagObjects != null || withTagObjects.Length > 0) {
            float closestDistance = Mathf.Infinity;
            
            Vector3 myPosition = gameObject.transform.position;
            foreach (var withTagObj in withTagObjects) {
                Vector3 positionDiff = withTagObj.transform.position - myPosition;
                float distance = positionDiff.sqrMagnitude;

                if (distance < closestDistance) {
                    closestObject = withTagObj;
                    closestDistance = distance;
                }
            }
        }

        return closestObject;
    }

    private bool CheckCustomCollision(GameObject obj) {
        var thisRenderer = gameObject.GetComponent<Renderer>();
        var objRenderer = obj.GetComponent<Renderer>();

        if (thisRenderer == null || objRenderer == null) {
            Debug.LogError("No renderers found in CheckCustomCollision!");
            return false;
        }

        Bounds thisBounds = thisRenderer.bounds;
        Bounds objBounds = objRenderer.bounds;

        if ((objBounds.max.z >= thisBounds.min.z && objBounds.max.z <= thisBounds.max.z) || (objBounds.min.z >= thisBounds.min.z && objBounds.min.z <= thisBounds.max.z)) {
            if ((objBounds.max.y >= thisBounds.min.y && objBounds.max.y <= thisBounds.max.y) || (objBounds.min.y >= thisBounds.min.y && objBounds.min.y <= thisBounds.max.y)) {
                if ((objBounds.max.x >= thisBounds.min.x && objBounds.max.x <= thisBounds.max.x) || (objBounds.min.x >= thisBounds.min.x && objBounds.min.x <= thisBounds.max.x)) {

                    return true;
                }
            }
        } else if ((thisBounds.max.z >= objBounds.min.z && thisBounds.max.z <= objBounds.max.z) || (thisBounds.min.z >= objBounds.min.z && thisBounds.min.z <= objBounds.max.z)) {
            if ((thisBounds.max.y >= objBounds.min.y && thisBounds.max.y <= objBounds.max.y) || (thisBounds.min.y >= objBounds.min.y && thisBounds.min.y <= objBounds.max.y)) {
                if ((thisBounds.max.x >= objBounds.min.x && thisBounds.max.x <= objBounds.max.x) || (thisBounds.min.x >= objBounds.min.x && thisBounds.min.x <= objBounds.max.x)) {

                    return true;
                }
            }
        }

        return false;
    }

    private void OnCollisionEnterCustom(GameObject obj) {

       if (obj.gameObject.transform.parent.GetComponent<PickupHolder>() != null) {
            obj.gameObject.transform.parent.GetComponent<PickupHolder>().RandomPickup();
       }

       Destroy(gameObject);
    }
}

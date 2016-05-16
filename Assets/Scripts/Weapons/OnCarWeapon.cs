using UnityEngine;
using System.Collections;

public class OnCarWeapon : MonoBehaviour {
    public GameObject parent;
    public Renderer objectRenderer;
    public float activeTime = 5;
    float timer = 0;
	// Use this for initialization
	void Start () {

        objectRenderer.enabled = false;
        SetParent(parent);
        foreach (Transform child in transform.parent)
        {
            if (child.name == "Shield(Clone)" && child != this.transform)
            {
                Destroy(gameObject);
            }
        }
        //parent.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ;
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (parent)
        {
        
        transform.position = parent.transform.position;
        transform.rotation = parent.transform.rotation;
            if (!objectRenderer.enabled)
            {
                objectRenderer.enabled = true;
            }
        }
        if (timer >= activeTime - 2)
        {
            if(timer%.3f < 0.15f)
            {
                objectRenderer.enabled = false;
            }
            else
            {
                objectRenderer.enabled = true;
            }
        }
        
        if(timer >= activeTime)
        {
            //parent.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            Destroy(gameObject);
        }
	}

    public void SetParent(GameObject parentTemp)
    {
        parent = parentTemp;
        transform.parent = parent.transform;
        if (objectRenderer.enabled)
        {
            objectRenderer.enabled = false;
        }
    }
}

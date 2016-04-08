using UnityEngine;
using System.Collections;

public class ParticleMain : MonoBehaviour {
    public float destroyTime;
    float timer = 0;
    public float scaleFactor;
	// Use this for initialization
	void Start () {
        transform.localScale *= scaleFactor;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        timer += Time.deltaTime;
        transform.position = transform.position + -transform.forward * 0.02f;
        
        if(timer >= destroyTime)
        {
            Destroy(gameObject);
        }
            Color c = GetComponent<Renderer>().material.GetColor("_TintColor");
            c.a = 1 - timer/destroyTime;
            GetComponent<Renderer>().material.SetColor("_TintColor", c);
        
    }
}

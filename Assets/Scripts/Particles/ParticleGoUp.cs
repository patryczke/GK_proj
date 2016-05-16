using UnityEngine;
using System.Collections;

public class ParticleGoUp : MonoBehaviour {
    public GameObject particle;
    float timer;
    public float delay;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if(timer >= delay)
        {
            //particle.transform.localScale = Vector3.zero;
            Instantiate(particle, new Vector3(transform.position.x + Random.Range(-0.1f,0.1f), transform.position.y, transform.position.z), Quaternion.identity);
            timer = 0;
        }
	}
}

using UnityEngine;
using System.Collections;

public class ParticleExplosion : MonoBehaviour {
    public GameObject fireParticle;
    public GameObject smokeParticle;
    public int howManySpawned = 3;

    // Use this for initialization
    void OnEnable () {
        for (int i = 0; i < howManySpawned; i++)
        {
            Instantiate(fireParticle, transform.position - transform.forward, Quaternion.identity);
            Instantiate(smokeParticle, transform.position, Quaternion.identity);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

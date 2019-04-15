using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

    Animator explosion;

	void Start () {
        explosion = GetComponent<Animator>();
	}
	
	void Update () {
        
        if (explosion.GetCurrentAnimatorStateInfo(0).IsName("Done"))
            Destroy(gameObject);

	}
}

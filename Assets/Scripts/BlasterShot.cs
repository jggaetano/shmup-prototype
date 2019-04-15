using UnityEngine;
using System.Collections;

public class BlasterShot : MonoBehaviour {

    public float speed;
    public float timer;
    public Vector2 direction;

	void Start () {
	}
	
	void Update () {

        float distThisFrame = speed * Time.deltaTime;
        transform.Translate(direction * distThisFrame);

        timer -= Time.deltaTime;
        if (timer <= 0) {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.layer == LayerMask.NameToLayer("Shield") && collision.gameObject.tag == gameObject.tag)
            return;

        if (collision.gameObject.tag == gameObject.tag)
            return;

        speed = 0;
        if (collision.gameObject.layer != gameObject.layer)
            Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("OnCollision");
        Destroy(gameObject);
    }


}

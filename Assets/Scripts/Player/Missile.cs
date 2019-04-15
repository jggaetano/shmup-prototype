using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {

    public float speed;
    public Vector2 direction;

    float rotation;

    void Start() {
    }

    void Update() {

        float distThisFrame = speed * Time.deltaTime;
        transform.Translate(direction * distThisFrame);
      
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag != gameObject.tag)
            Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision) {

        

        direction = Vector2.right;
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground")) {
            Vector3 contactPoint = collision.contacts[0].point;
            contactPoint.y = Mathf.Round(contactPoint.y);
            transform.position = contactPoint;
            return;
        }

        if (collision.gameObject.tag != gameObject.tag)
            Destroy(gameObject);

        if (collision.gameObject.layer == LayerMask.NameToLayer("Border"))
            Destroy(gameObject);
    }

}

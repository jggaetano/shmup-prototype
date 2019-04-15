using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {

    public int totalHitPoints;
    int hitPoints;
    
    void OnEnable() {
        hitPoints = totalHitPoints;
    }

    void TakeDamage() {
        hitPoints--;
        if (hitPoints <= 0) {
            FindObjectOfType<GameManager>().ShieldDown();
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.tag == "Enemy" || collision.gameObject.name == "Enemy_Blaster") {
            TakeDamage();
        }

    }

}

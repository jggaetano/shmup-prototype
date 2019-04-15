using UnityEngine;
using System.Collections;

public enum PowerUPType {
    SPEEDUP, MISSLES, SHIELD, EXTRABLASTERS
    //RAPIDFIRE, 
}

public class PowerUp : MonoBehaviour {

    const float flashCooldown = 0.1f;

    public float speed;
    public Sprite flashSprite;

    SpriteRenderer spriteRenderer;
    Sprite originalSprite;
    bool flashCheck;
    float timer;
    Vector2 direction;

    void Start() {
        flashCheck = false;
        timer = 0;
        direction = Vector2.left;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSprite = spriteRenderer.sprite;
    }

    void Update() {

        float distThisFrame = speed * Time.deltaTime;
        transform.Translate(direction * distThisFrame);

        timer += Time.deltaTime;
        if (timer > flashCooldown) {
            Flash();
            timer = 0;
        }
        
    }

    void Flash() {
        if (flashCheck)
            spriteRenderer.sprite = originalSprite;
        else
            spriteRenderer.sprite = flashSprite;

        flashCheck = !flashCheck;      
    }

    void PickedUp() {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.layer == LayerMask.NameToLayer("Border"))
            Destroy(gameObject);

        if (collision.gameObject.tag == "Player") 
            PickedUp();
  
    }
}

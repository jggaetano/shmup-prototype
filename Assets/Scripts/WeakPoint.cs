using UnityEngine;
using System.Collections;

public class WeakPoint : MonoBehaviour {

    public GameObject whoToDestroy;
    public int hitPoints;
    public Sprite flashSprite;
    public float flashTime;

    SpriteRenderer spriteRenderer;
    Sprite originalSprite;
    bool flashing;
    bool state;
    float timer;

    void Start() {
        flashing = false;
        state = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSprite = spriteRenderer.sprite;
    }

    void FixedUpdate() {

        if (flashing) {
            state = !state;
            Flash();
            timer += Time.deltaTime;
            if (timer > flashTime) {
                flashing = false;
                state = true;
                Flash();
            }
        }

    }

    void Flash() {
        if (state)
            spriteRenderer.sprite = originalSprite;
        else
            spriteRenderer.sprite = flashSprite;
    }

    void TakeDamage() {
        hitPoints--;
        if (hitPoints <= 0) {
            if (whoToDestroy == transform.parent.gameObject)
                FindObjectOfType<GameManager>().AddPoints(whoToDestroy.GetComponent<EnemyController>().points);
            Destroy(whoToDestroy);
        }

        timer = 0;
        flashing = true;
    }

    void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.tag != gameObject.tag) {
            TakeDamage();
        }

    }
}

using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    const float rapidFire = 0.2f;
    const float missileCoolDown = rapidFire * 4;

    public GameObject blasterShotPrefab;
    public GameObject missilePrefab;
    public GameObject explosionPrefab;
    public float speed;
    public float blasterCoolDown;

    Vector2 direction;
    float blasterTimer, missileTimer;
    int missileCount;

	void Start () {
        direction = Vector2.zero;
        blasterTimer = 0;
        missileTimer = 0;
        missileCount = 0;
	}

    void Update()
    {

        if (Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 0)
            direction = Vector2.zero;

        if (Input.GetAxisRaw("Vertical") == 1)
            direction = Vector2.up;
        if (Input.GetAxisRaw("Vertical") == -1)
            direction = Vector2.down;
        if (Input.GetAxisRaw("Horizontal") == 1)
            direction = Vector2.right;
        if (Input.GetAxisRaw("Horizontal") == -1)
            direction = Vector2.left;
        if (Input.GetAxisRaw("Vertical") == 1 && Input.GetAxisRaw("Horizontal") == 1)
            direction = Vector2.right + Vector2.up;
        if (Input.GetAxisRaw("Vertical") == -1 && Input.GetAxisRaw("Horizontal") == 1)
            direction = Vector2.right + Vector2.down;
        if (Input.GetAxisRaw("Vertical") == 1 && Input.GetAxisRaw("Horizontal") == -1)
            direction = Vector2.left + Vector2.up;
        if (Input.GetAxisRaw("Vertical") == -1 && Input.GetAxisRaw("Horizontal") == -1)
            direction = Vector2.left + Vector2.down;

        if (blasterTimer <= 0) { 
            if (Input.GetButtonDown("Fire") || Input.GetButton("Fire")) {
                Fire();
                blasterTimer = blasterCoolDown;
            }
        }
        if (missileTimer <= 0) {
            if (Input.GetButtonDown("Fire") || Input.GetButton("Fire")) {
                Launch();
                missileTimer = missileCoolDown;
            }
        }
        
        blasterTimer -= Time.deltaTime;
        missileTimer -= Time.deltaTime;

	}

    void FixedUpdate() {

        float distThisFrame = speed * Time.deltaTime;
        transform.Translate(direction.normalized * distThisFrame);

    }

    void Die() {
        Instantiate(explosionPrefab, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.tag == "PowerUP") {
            FindObjectOfType<GameManager>().AdvancePowerUpChoice();
            return;
        }

        if (collision.gameObject.tag != gameObject.tag) {
            Die();
        }

    }

    void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.tag == gameObject.tag)
            return;

        if (collision.gameObject.layer != LayerMask.NameToLayer("Border")) {
            Die();
        }
    }

    void Fire() {

        GameObject[] blasters = GameObject.FindGameObjectsWithTag("Blaster");
        foreach (var blaster in blasters) {
            GameObject blasterShotGameObject = (GameObject)Instantiate(blasterShotPrefab, blaster.transform.position, Quaternion.identity);
            blasterShotGameObject.GetComponent<BlasterShot>().direction = Vector2.right;
            blasterShotGameObject.name = "Player_Blaster";
            blasterShotGameObject.tag = "Player";
            blasterShotGameObject.GetComponent<BlasterShot>().speed += speed;
        }
        
    }

    void Launch() {

        GameObject[] launchers = GameObject.FindGameObjectsWithTag("Launcher");
        foreach (var launcher in launchers) {
            GameObject missileGameObject = (GameObject)Instantiate(missilePrefab, launcher.transform.position, missilePrefab.transform.rotation);
            missileGameObject.name = "Player_Missile";
            if (launcher.GetComponent<Launcher>().position == LauncherPosition.TOP) {
                missileGameObject.transform.Rotate(0, 0, 65);
                missileGameObject.GetComponent<Missile>().direction.y *= -1;
            }
        }

    }

    public void AddLauncher() {

        for (int i = 0; i < transform.childCount; i++) {
            if (transform.GetChild(i).name == "Launcher" && transform.GetChild(i).gameObject.activeSelf == false) {
                transform.GetChild(i).gameObject.SetActive(true);
                missileCount++;
                CheckBlasters();
                return;
            }
        }

    }

    void CheckBlasters() {

        for (int i = 0; i < transform.childCount; i++) {
            if (transform.GetChild(i).name == "ExtraBlaster" && transform.GetChild(i).gameObject.activeSelf == true) {
                if (missileCount > 0 && transform.GetChild(i).GetChild(0).gameObject.GetComponent<Launcher>().position == LauncherPosition.BOTTOM)
                    transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
                if (missileCount > 1 && transform.GetChild(i).GetChild(0).gameObject.GetComponent<Launcher>().position == LauncherPosition.TOP)
                    transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
            }
        }

    }

    public void AddBlaster() {

        for (int i = 0; i < transform.childCount; i++) {
            if (transform.GetChild(i).name == "ExtraBlaster" && transform.GetChild(i).gameObject.activeSelf == false) {
                transform.GetChild(i).gameObject.SetActive(true);
                if (missileCount > 0 && transform.GetChild(i).GetChild(0).gameObject.GetComponent<Launcher>().position == LauncherPosition.BOTTOM)
                    transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
                if (missileCount > 1 && transform.GetChild(i).GetChild(0).gameObject.GetComponent<Launcher>().position == LauncherPosition.TOP)
                    transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
                return;
            }
        }

    }

    public void ShieldsUp() {

        for (int i = 0; i < transform.childCount; i++) {
            if (transform.GetChild(i).name == "Shield" && transform.GetChild(i).gameObject.activeSelf == false) {
                transform.GetChild(i).gameObject.SetActive(true);
                return;
            }
        }

    }

}

using UnityEngine;
using System.Collections;

public enum EnemyPath {
    ATTACK,
    STRAIGHT, WAVE, LOOP, SCURVE,
    HOVER_EXIT_TOP, HOVER_EXIT_MIDDLE, HOVER_EXIT_BOTTOM,
    HOVER, HOVER_EXIT,
    STRAIFE_EXIT_BOTTOM
}

public class Spawner : MonoBehaviour {

    public GameObject enemyPrefab;
    public GameObject bossPrefab;
    public GameObject attackPrefab;
    public GameObject straightPathPrefab;
    public GameObject wavePathPrefab;
    public GameObject loopPathPrefab;
    public GameObject sCurvePathPrefab;
    public GameObject hoverPrefab;
    public GameObject hoverExit;
    public GameObject hoverExitTopPrefab;
    public GameObject hoverExitMiddlePrefab;
    public GameObject hoverExitBottomPrefab;
    public GameObject straifeExitBottom;

    public float spawnDuration;
    public int groupSize;
    public EnemyPath path;
    public EnemyType type;
    public bool dropLootSetting;
    float timer;
    int activeCount;
    int spawnCounter;
    bool dropLoot;

    GameObject pathGameObject;

    void OnEnable() {
        activeCount = groupSize;
        timer = 0;
        spawnCounter = groupSize;
        dropLoot = dropLootSetting;

        switch (path) {
            case EnemyPath.STRAIGHT:
                pathGameObject = (GameObject)Instantiate(straightPathPrefab, gameObject.transform.position, Quaternion.identity);
                break;
            case EnemyPath.WAVE:
                pathGameObject = (GameObject)Instantiate(wavePathPrefab, gameObject.transform.position, Quaternion.identity);
                break;
            case EnemyPath.LOOP:
                pathGameObject = (GameObject)Instantiate(loopPathPrefab, gameObject.transform.position, Quaternion.identity);
                break;
            case EnemyPath.SCURVE:
                pathGameObject = (GameObject)Instantiate(sCurvePathPrefab, gameObject.transform.position, Quaternion.identity);
                break;
            case EnemyPath.HOVER:
                pathGameObject = (GameObject)Instantiate(hoverPrefab, gameObject.transform.position, Quaternion.identity);
                break;
            case EnemyPath.HOVER_EXIT_TOP:
                pathGameObject = (GameObject)Instantiate(hoverExitTopPrefab, gameObject.transform.position, Quaternion.identity);
                break;
            case EnemyPath.HOVER_EXIT_MIDDLE:
                pathGameObject = (GameObject)Instantiate(hoverExitMiddlePrefab, gameObject.transform.position, Quaternion.identity);
                break;
            case EnemyPath.HOVER_EXIT_BOTTOM:
                pathGameObject = (GameObject)Instantiate(hoverExitBottomPrefab, gameObject.transform.position, Quaternion.identity);
                break;
            case EnemyPath.STRAIFE_EXIT_BOTTOM:
                pathGameObject = (GameObject)Instantiate(straifeExitBottom, gameObject.transform.position, Quaternion.identity);
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update() {

        if (spawnCounter == 0)
            return;

        timer -= Time.deltaTime;
        if (timer <= 0) {
            GameObject go;
            switch (type) {
                case EnemyType.BOSS:
                    go = (GameObject)Instantiate(bossPrefab, gameObject.transform.position, Quaternion.identity);
                    go.name = "BossShip";
                    break;
                default:
                    go = (GameObject)Instantiate(enemyPrefab, gameObject.transform.position, Quaternion.identity);
                    go.name = "EnemyShip " + spawnCounter;
                    go.GetComponent<EnemyController>().type = type;
                    break;
            }
   
            go.GetComponent<EnemyController>().path = pathGameObject;
            go.GetComponent<EnemyController>().spawnerOrigin = gameObject.name;

            timer = spawnDuration;
            spawnCounter--;
        }

    }

    void CleanUp() {
        Destroy(pathGameObject);
        gameObject.SetActive(false);
    }

    public void Survived() {
        activeCount--;
        dropLoot = false;
        if (activeCount == 0)
            CleanUp();
    }

    public bool Killed() {
        activeCount--;
        if (activeCount == 0) {
            CleanUp();
            return dropLoot;
        }

        return false;
    }
}

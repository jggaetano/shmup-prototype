using UnityEngine;
using System.Collections.Generic;


public class SpawnManager : MonoBehaviour {

    public int totalSpawns;

    int counter;
    Spawner top, middle, bottom;
    List<Spawner> spawners;
    int current = 0;

	void Start () {
        counter = 0;
        spawners = new List<Spawner>();

        top = transform.FindChild("Top").gameObject.GetComponent<Spawner>();
        middle = transform.FindChild("Middle").gameObject.GetComponent<Spawner>();
        bottom = transform.FindChild("Bottom").gameObject.GetComponent<Spawner>();
        top.groupSize = 8;
        top.path = EnemyPath.LOOP;
        top.type = EnemyType.FLIER;
        top.dropLootSetting = true;
        top.spawnDuration = .2f;
        middle.groupSize = 1;
        middle.path = EnemyPath.STRAIGHT;
        middle.type = EnemyType.SHOOTER;
        middle.dropLootSetting = true;
        middle.enemyPrefab.GetComponent<EnemyController>().waitCooldown = .8f;
        bottom.groupSize = 7;
        bottom.path = EnemyPath.STRAIGHT;
        bottom.type = EnemyType.FLIER;
        bottom.dropLootSetting = true;
        bottom.spawnDuration = .2f;

        spawners.Add(top);
        spawners.Add(middle);
        spawners.Add(bottom);

        //SpawnBoss();
        //spawners[current].gameObject.SetActive(true);
        spawners[0].gameObject.SetActive(true);
    }

    void Update () {

        if (spawners[current].gameObject.activeSelf == false) {
            if (counter < totalSpawns) {
                current = Random.Range(0, spawners.Count);
                current = 1;
                spawners[current].gameObject.SetActive(true);
                counter++;
            }
            else {
                SpawnBoss();
            }
        }
    }

    void SpawnBoss() {
        middle.groupSize = 1;
        middle.path = EnemyPath.HOVER;
        middle.type = EnemyType.BOSS;
        middle.dropLootSetting = false;
        middle.gameObject.SetActive(true);
    }
}

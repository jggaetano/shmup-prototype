using UnityEngine;
using System;
using System.Collections.Generic;

public enum EnemyType {
    FLIER, SHOOTER, BOSS, ATTACKER
}

public class EnemyController : MonoBehaviour {

    public float speed;
    public GameObject blasterShotPrefab;
    public GameObject powerUpPrefab;
    public GameObject explosionPrefab;
    public float shotCooldown;
    public float waitCooldown;
    public int points;
    public Sprite originalSprite;
    public Sprite flashSprite;

    //[HideInInspector]
    public EnemyType type;
    //[HideInInspector]
    public GameObject path;
    //[HideInInspector]
    public string spawnerOrigin;

    Enemy enemy;

    GameObject go;
//    Transform targetPathNode;
//    int pathNodeIndex = 0;
//    float waitTimer;
    bool waited;
 
    void Start () {
//        waitTimer = 0f;
        waited = false;
    }

    void Update() {

        if (enemy == null) {
            SetType();
            return;
        }

        DoState(enemy.PassiveState);
        DoState(enemy.CycleState);
        
        /*switch (enemy.Action) {
            case State.WAITING:
                Waiting(Time.deltaTime);
                break;
            case State.MOVING:
                if (targetPathNode == null) {
                    GetNextPathNode();
                    if (targetPathNode == null) {
                        // We've run out of path!
                        ReachedGoal();
                        return;
                    }
                }
                Move(Time.deltaTime);
                break;
            case State.COOLDOWN:
                CoolDown(Time.deltaTime);
                break;
            case State.MUZZLEFLASH:
                MuzzleFlash(Time.deltaTime);
                break;
            case State.SHOOT:
                Shoot(Time.deltaTime);
                break;
            case State.ATTACK:
                Attack(Time.deltaTime);
                break;
            default:
                 break;
        } */
    }

    void DoState(IEnemyState state) {

        if (state == null)
            return;

        state.UpdateState(Time.deltaTime);
    
    }

    void SetType() {
        switch (type) {
            case EnemyType.ATTACKER:
                enemy = new Attacker();
                break;
            case EnemyType.FLIER:
                enemy = new Flier(this);
                break;
            case EnemyType.SHOOTER:
                enemy = new Shooter(this);
                break;
            case EnemyType.BOSS:
                enemy = new Boss(this);
                break;
        }
    }

    //void Move(float deltaTime) {

    //    if (targetPathNode.tag == "Wait") {
    //        waitTimer += Time.deltaTime;
    //        if (waitTimer < waitCooldown)
    //            return;
    //        waitTimer = 0;
    //        waited = true;
    //    }

    //    if (targetPathNode.tag == "Next") {
    //        //enemy.NextState();
    //        return;
    //    }

    //    Vector3 dir = targetPathNode.position - this.transform.localPosition;

    //    float distThisFrame = speed * Time.deltaTime;

    //    if (dir.magnitude <= distThisFrame) {
    //        // We reached the node
    //        targetPathNode = null;
    //    }
    //    else {
    //        // TODO: Consider ways to smooth this motion.

    //        // Move towards node
    //        transform.Translate(dir.normalized * distThisFrame, Space.World);
    //        Quaternion targetRotation = Quaternion.LookRotation(dir);
    //        if (enemy.CanTurn) {
    //            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    //            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    //        }
    //    }
    //}

    //void Waiting(float deltaTime) {
    //    if (waited == false)
    //        return;

    //    //enemy.RemoveState();
    //}

   // void CoolDown(float deltaTime) {
   //     enemy.Timer += deltaTime;
   //     if (enemy.Timer >= enemy.ShotCooldown) {
   //       //  enemy.NextState();
   //     }
   // }

   //public void MuzzleFlash(float deltaTime) {

   //     enemy.Timer += deltaTime;
   //     if (enemy.Timer >= Enemy.flashTimer) {
   //         for (int i = 0; i < transform.childCount; i++) {
   //             if (transform.GetChild(i).name == "Blaster") {
   //                 if (transform.GetChild(i).childCount != 0) {
   //                     transform.GetChild(i).GetChild(1).gameObject.SetActive(false);
   //                 }
   //             }
   //         }
   //         return;
   //     }

   //     for (int i = 0; i < transform.childCount; i++) {
   //         if (transform.GetChild(i).name == "Blaster") {
   //             if (transform.GetChild(i).childCount != 0) {
   //                 transform.GetChild(i).GetChild(1).gameObject.SetActive(!transform.GetChild(i).GetChild(1).gameObject.activeSelf);
   //             }
   //         }
   //     }
   // } 

    public void Shoot() {
        for (int i = 0; i < transform.childCount; i++) {
            if (transform.GetChild(i).name == "Blaster") {
                go = (GameObject)Instantiate(blasterShotPrefab, transform.GetChild(i).position, Quaternion.identity);
                go.GetComponent<BlasterShot>().direction = Vector2.left;
                go.GetComponent<BlasterShot>().speed = 14f;
                go.name = "Enemy_Blaster";
                go.tag = "Enemy";
            }
        }
    }

    //void Attack(float deltaTime) {

    //    targetPathNode = GameObject.Find("PlayerShip").transform;
    //    Move(deltaTime);

    //}

    //void GetNextPathNode() {
    //    if (pathNodeIndex < path.transform.childCount) {
    //        targetPathNode = path.transform.GetChild(pathNodeIndex);
    //        pathNodeIndex++;
    //        if (targetPathNode.tag == "Goto") {
    //            pathNodeIndex = int.Parse(targetPathNode.name);
    //            targetPathNode = path.transform.GetChild(pathNodeIndex);
    //        }
    //    }
    //    else {
    //        targetPathNode = null;
    //    }
    //}

    public void ReachedGoal() {
        GameObject.Find(spawnerOrigin).GetComponent<Spawner>().Survived();
        Destroy(gameObject);
    }

    void Die() {
        if (GameObject.Find(spawnerOrigin).GetComponent<Spawner>().Killed())
            Instantiate(powerUpPrefab, gameObject.transform.position, Quaternion.identity);

        Instantiate(explosionPrefab, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    
    void OnTriggerEnter2D(Collider2D collision) {

      if (collision.gameObject.tag != gameObject.tag) {
            FindObjectOfType<GameManager>().AddPoints(points);
            Die();
        }

    }

    void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.tag != gameObject.tag) {
            FindObjectOfType<GameManager>().AddPoints(points);
            Die();
        }

    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : IEnemyState {

    private readonly EnemyController ec;
    private readonly Enemy enemy;

    private Transform targetPathNode;
    private int pathNodeIndex;

    public bool Passive { get; private set; }
    public bool OneTime { get; private set; }
    
    public MoveState(EnemyController enemyController, Enemy enemy, bool passive = false, bool oneTime = false) {
        ec = enemyController;
        this.enemy = enemy;
        pathNodeIndex = 0;
        Passive = passive;
        OneTime = oneTime;
    }

    public void UpdateState(float deltaTime) {

        if (targetPathNode == null) {
            if (Passive == true)
                GetNextPathNode();
            else
                enemy.NextState();
            
            if (targetPathNode == null) {
                // We've run out of path!
                ec.ReachedGoal();
                return;
            }
        }

        Vector3 dir = targetPathNode.position - ec.transform.localPosition;

        float distThisFrame = ec.speed * deltaTime;

        if (dir.magnitude <= distThisFrame) {
            // We reached the node
            targetPathNode = null;
        }
        else {
            // TODO: Consider ways to smooth this motion.

            // Move towards node
            ec.transform.Translate(dir.normalized * distThisFrame, Space.World);
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            if (enemy.CanTurn) {
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                ec.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
           
    }
    
    void GetNextPathNode() {

        if (pathNodeIndex < ec.path.transform.childCount) {
            targetPathNode = ec.path.transform.GetChild(pathNodeIndex);
            pathNodeIndex++;
            if (targetPathNode.tag == "Goto") {
                pathNodeIndex = int.Parse(targetPathNode.name);
                targetPathNode = ec.path.transform.GetChild(pathNodeIndex);
            }
        }
        else {
            targetPathNode = null;
        }

    }

}

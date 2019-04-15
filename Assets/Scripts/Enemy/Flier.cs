using UnityEngine;
using System.Collections;

public class Flier : Enemy {

    public Flier(EnemyController enemyController) : base() {
        PassiveState = new MoveState(enemyController, this, true);
    }

}

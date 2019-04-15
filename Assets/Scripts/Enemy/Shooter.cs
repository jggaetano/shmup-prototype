using UnityEngine;

public class Shooter : Enemy {

    public Shooter(EnemyController enemyController) : base() {
        PassiveState = new MoveState(enemyController, this, true);
        states.Add(new WaitState(enemyController, this, enemyController.waitCooldown));
        states.Add(new ShootState(enemyController, this, enemyController.shotCooldown));
    }

}

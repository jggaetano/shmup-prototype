using UnityEngine;
using System.Collections.Generic;

public class Boss : Enemy {

   	public Boss(EnemyController enemyController) : base() {
        CanTurn = false;
        PassiveState = new MoveState(enemyController, this, true);
        states.Add(new WaitState(enemyController, this, 3.0f, true));
        states.Add(new MuzzleFlashState(enemyController, this, Enemy.flashTimer));
        states.Add(new ShootState(enemyController, this, enemyController.shotCooldown));
        states.Add(new WaitState(enemyController, this, enemyController.waitCooldown));
    }
	
}

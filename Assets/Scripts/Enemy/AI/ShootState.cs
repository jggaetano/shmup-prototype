using System;

public class ShootState : IEnemyState {

    private readonly EnemyController enemyController;
    private readonly Enemy enemy;

    private float timer;
    private float coolDown;

    public bool Passive {
        get; private set;
    }

    public bool OneTime {
        get; private set;
    }

    public ShootState(EnemyController enemyController, Enemy enemy, float shotCoolDown, bool passive = false, bool oneTime = false) {
        this.enemyController = enemyController;
        this.enemy = enemy;
        timer = 0;
        coolDown = shotCoolDown;
        Passive = passive;
        OneTime = oneTime;
    }

    public void UpdateState(float deltaTime) {
        timer -= deltaTime;
        if (timer <= 0) {
            enemyController.Shoot();
            if (Passive == false)
                enemy.NextState();
            else
                timer = coolDown;
        }
    }

}

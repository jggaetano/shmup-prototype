using System;

public class ChaseState : IEnemyState {

    private readonly EnemyController enemyController;
    private readonly Enemy enemy;

    public bool Passive {
        get; private set;
    }

    public bool OneTime {
        get; private set;
    }

    public ChaseState(EnemyController enemyController, Enemy enemy, bool passive = false, bool oneTime = false) {
        this.enemyController = enemyController;
        this.enemy = enemy;
        Passive = passive;
        OneTime = oneTime;
    }

    public void UpdateState(float deltaTime) {
    }
}

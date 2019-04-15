using System;

public class WaitState : IEnemyState {

    private readonly EnemyController enemyController;
    private readonly Enemy enemy;

    private float timer;
    private float coolDown;

    public bool Passive { get { return false; }  }
    public bool OneTime { get; private set; }

    public WaitState(EnemyController enemyController, Enemy enemy, float waitCoolDown, bool oneTime = false) {
        this.enemyController = enemyController;
        this.enemy = enemy;
        timer = 0;
        OneTime = oneTime;
        coolDown = waitCoolDown;
    }

    public void UpdateState(float deltaTime) {
        timer += deltaTime;
        if (timer >= coolDown) {
            timer = 0;
            enemy.NextState();                        
        }
    }

}
